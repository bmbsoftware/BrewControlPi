using System;
using BrewControlPi.Service.Core.Services;
using Microsoft.AspNetCore.Mvc;

namespace BrewControlPi.WebApi.Controllers
{
	[Route("api/[controller]")]
	public class PinsController : Controller
	{
		private readonly IPinService _pinService;

		public PinsController(IPinService pinService)
		{
			_pinService = pinService;
		}

		[HttpGet]
		public IActionResult Get()
		{
			Console.WriteLine("About to list pin statuses.");
			var status = _pinService.GetStatus(1);
			return Ok(status);
		}

		[HttpGet("{pinId}")]
		public IActionResult Get(int pinId)
		{
			Console.WriteLine($"About to get pin status for pin {pinId}.");

			var pinStatus = _pinService.GetStatus(pinId);
			Console.WriteLine($"Return pin status of pin {pinId}: {pinStatus}");
			return Ok(pinStatus ? "On" : "Off");
		}

		[HttpPost]
		public void SwitchPin(int pinId, int status)
		{
			Console.WriteLine($"About to change pin status for pin {pinId}.");
			_pinService.UpdateStatus(pinId, status);
		}
	}
}
