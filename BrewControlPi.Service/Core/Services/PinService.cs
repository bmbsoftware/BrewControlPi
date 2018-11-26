using System;
using Unosquare.RaspberryIO;
using Unosquare.RaspberryIO.Gpio;

namespace BrewControlPi.Service.Core.Services
{
	public class PinService : IPinService
	{
		public bool GetStatus(int id)
		{
			var pin = Pi.Gpio[id];
			pin.PinMode = GpioPinDriveMode.Output;
			var pinStatus = pin.Read();
			return pinStatus;
		}

		public void UpdateStatus(int id, int status)
		{
			var pin = Pi.Gpio[id];
			pin.PinMode = GpioPinDriveMode.Output;

			if (status == 1)
			{
				pin.Write(GpioPinValue.High);
			}
			else
			{
				pin.Write(GpioPinValue.Low);
			}
		}
	}
}
