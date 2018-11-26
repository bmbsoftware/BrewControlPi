using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using BrewControlPi.Service.Sensors.Models;

namespace BrewControlPi.Service.Sensors.Services
{
	public class SensorService : ISensorService
	{
		public IEnumerable<Sensor> GetAll()
		{
			var contents = Directory.EnumerateDirectories("/sys/bus/w1/devices").Select(dir => dir.Substring(dir.LastIndexOf("/") + 1));
			var result = contents
				.Where(x => x.StartsWith("28") || x.StartsWith("10"))
				.Select(x => new TemperatureSensor { Id = x });
			return result;
		}

		public TemperatureSensor GetTemperatureSensor(string id)
		{
			double? currentTemp = null;
			using (var stream = File.OpenRead($"/sys/bus/w1/devices/{id}/w1_slave"))
			using (var reader = new StreamReader(stream))
			{
				var content = reader.ReadToEnd();
				if (content.Split('\n')[0].Split(' ')[11] == "YES")
				{
					currentTemp = double.Parse(content.Split("=")[content.Split("=").Length - 1]) / 1000;
					currentTemp = Math.Round(currentTemp.Value, 2);
				}
			}

			if (!currentTemp.HasValue)
			{
				return null;
			}

			var fTemp = Math.Round(currentTemp.Value * 9 / 5 + 32, 2);
			var result = new TemperatureSensor
			{
				Id = id,
				CelsiusValue = currentTemp,
				FahrenheitValue = fTemp
			};
			return result;
		}
	}
}
