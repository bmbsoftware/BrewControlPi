using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using BrewControlPi.Service.Sensors.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.FileProviders;

namespace BrewControlPi.WebApi.Controllers
{
	[Route("api/[controller]")]
	public class SensorsController : Controller
	{
		private readonly ISensorService _sensorService;

		public SensorsController(ISensorService sensorService)
		{
			_sensorService = sensorService;
		}

		[HttpGet]
		public IActionResult Get()
		{
			var result = _sensorService.GetAll();
			return Ok(result);
		}

		[HttpGet("{id}")]
		public IActionResult Get(string id)
		{
			var result = _sensorService.GetTemperatureSensor(id);
			return Ok(result);
		}
	}
}
