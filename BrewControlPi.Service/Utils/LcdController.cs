using System.Threading;
using WiringPi;

namespace BrewControlPi.Service.Utils
{
	public class LcdController : ILcdController
	{
		private const int LCDAddr = 0x27;
		private int BLEN = 1;
		private int fd;

		public LcdController()
		{
			fd = I2C.wiringPiI2CSetup(LCDAddr);
			init();
		}

		public void Clear()
		{
			send_command(0x01); //clear Screen
		}

		public void Write(int x, int y, char[] data)
		{
			int addr, i;
			int tmp;
			if (x < 0) x = 0;
			if (x > 15) x = 15;
			if (y < 0) y = 0;
			if (y > 1) y = 1;

			// Move cursor
			addr = 0x80 + 0x40 * y + x;
			send_command(addr);

			tmp = data.Length;
			for (i = 0; i < tmp; i++)
			{
				send_data(data[i]);
			}
		}

		private void write_word(int data)
		{
			int temp = data;
			if (BLEN == 1)
				temp |= 0x08;
			else
				temp &= 0xF7;
			I2C.wiringPiI2CWrite(fd, temp);
		}

		private void send_command(int comm)
		{
			int buf;
			// Send bit7-4 firstly
			buf = comm & 0xF0;
			buf |= 0x04;      // RS = 0, RW = 0, EN = 1
			write_word(buf);
			Thread.Sleep(2);
			buf &= 0xFB;      // Make EN = 0
			write_word(buf);

			// Send bit3-0 secondly
			buf = (comm & 0x0F) << 4;
			buf |= 0x04;      // RS = 0, RW = 0, EN = 1
			write_word(buf);
			Thread.Sleep(2);
			buf &= 0xFB;      // Make EN = 0
			write_word(buf);
		}

		private void send_data(int data)
		{
			int buf;
			// Send bit7-4 firstly
			buf = data & 0xF0;
			buf |= 0x05;      // RS = 1, RW = 0, EN = 1
			write_word(buf);
			Thread.Sleep(2);
			buf &= 0xFB;      // Make EN = 0
			write_word(buf);

			// Send bit3-0 secondly
			buf = (data & 0x0F) << 4;
			buf |= 0x05;      // RS = 1, RW = 0, EN = 1
			write_word(buf);
			Thread.Sleep(2);
			buf &= 0xFB;      // Make EN = 0
			write_word(buf);
		}

		private void init()
		{
			send_command(0x33); // Must initialize to 8-line mode at first
			Thread.Sleep(5);
			send_command(0x32); // Then initialize to 4-line mode
			Thread.Sleep(5);
			send_command(0x28); // 2 Lines & 5*7 dots
			Thread.Sleep(5);
			send_command(0x0C); // Enable display without cursor
			Thread.Sleep(5);
			send_command(0x01); // Clear Screen
			I2C.wiringPiI2CWrite(fd, 0x08);
		}
	}
}
