using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BrewControlPi.Service.Utils;

namespace BrewControlPi.Service.Core.Services
{
	public class DisplayService : IDisplayService
	{
		private readonly ILcdController _lcd;

		public DisplayService(ILcdController lcd)
		{
			_lcd = lcd;
		}

		public void Show(string text)
		{
			var bytes = text.ToCharArray();
			_lcd.Write(0, 0, bytes);
		}
	}
}
