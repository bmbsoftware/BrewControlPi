namespace BrewControlPi.Service.Sensors.Models
{
	public class TemperatureSensor : Sensor
	{
		public double? CelsiusValue { get; set; }

		public double? FahrenheitValue { get; set; }
	}
}
