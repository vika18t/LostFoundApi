using LostFoundApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LostFoundApi.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class LostItemsController : ControllerBase
	{
		private static readonly List<LostItem> items = new()
		{
			new() { Id = 1, Name = "Ключи", Place = "Аудитория 205" },
			new() { Id = 2, Name = "Флешка", Place = "Библиотека" },
			new() { Id = 3, Name = "Наушники", Place = "Столовая" },
			new() { Id = 4, Name = "Студенческий билет", Place = "Холл" }
		};

		[HttpGet]
		public IActionResult GetAll()
		{
			return Ok(items);
		}

		[HttpGet("{id}")]
		public IActionResult GetById(int id)
		{
			var item = items.FirstOrDefault(x => x.Id == id);
			if (item == null) return NotFound();
			return Ok(item);
		}

		[HttpPost]
		public IActionResult Add([FromBody] LostItem item)
		{
			item.Id = items.Count == 0 ? 1 : items.Max(x => x.Id) + 1;
			items.Add(item);
			return StatusCode(201, item);
		}

		[HttpDelete("{id}")]
		public IActionResult Delete(int id)
		{
			var item = items.FirstOrDefault(x => x.Id == id);
			if (item == null) return NotFound();

			items.Remove(item);
			return NoContent();
		}

		[HttpGet("search")]
		public IActionResult SearchByPlace([FromQuery] string place)
		{
			if (string.IsNullOrWhiteSpace(place))
				return Ok(new List<LostItem>());

			var found = items.Where(x => x.Place.Contains(place, StringComparison.OrdinalIgnoreCase)).ToList();
			return Ok(found);
		}
	}
}