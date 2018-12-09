using System;
using BrewControlPi.Service.Core.Services;
using Microsoft.AspNetCore.Mvc;

namespace BrewControlPi.WebApi.Controllers
{
	[Route("api/[controller]")]
	public class DisplayController : Controller
	{
		private readonly IDisplayService _displayService;

		public DisplayController(IDisplayService displayService)
		{
			_displayService = displayService;
		}

		[HttpPost]
		public IActionResult Show(string text)
		{
			_displayService.Show(text);
			return Ok();
		}
	}
}
