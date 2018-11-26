using System.Collections.Generic;
using System.Threading.Tasks;
using BrewControlPi.Service.Sensors.Models;

namespace BrewControlPi.Service.Sensors.Services
{
	public interface ISensorService
	{
		IEnumerable<Sensor> GetAll();

		TemperatureSensor GetTemperatureSensor(string id);
	}
}
