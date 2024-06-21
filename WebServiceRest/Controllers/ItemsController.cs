using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using WebServiceRest.Models;

namespace WebServiceRest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemsController : Controller
    {
        private static readonly List<Item> Items = new List<Item>
        {
            new Item { Id = 1, Name = "Item1", Description = "Description1" },
            new Item { Id = 2, Name = "Item2", Description = "Description2" }
        };

        [HttpGet]
        public ActionResult<IEnumerable<Item>> GetItems()
        {
            return Items;
        }

        [HttpGet("{id}")]
        public ActionResult<Item> GetItem(int id)
        {
            var item = Items.FirstOrDefault(i => i.Id == id);
            if (item == null)
            {
                return NotFound();
            }
            return item;
        }

        [HttpPost]
        public ActionResult<Item> PostItem(Item item)
        {
            item.Id = Items.Count + 1;
            Items.Add(item);
            return CreatedAtAction(nameof(GetItem), new { id = item.Id }, item);
        }

        [HttpPut("{id}")]
        public IActionResult PutItem(int id, Item item)
        {
            var existingItem = Items.FirstOrDefault(i => i.Id == id);
            if (existingItem == null)
            {
                return NotFound();
            }
            existingItem.Name = item.Name;
            existingItem.Description = item.Description;
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteItem(int id)
        {
            var item = Items.FirstOrDefault(i => i.Id == id);
            if (item == null)
            {
                return NotFound();
            }
            Items.Remove(item);
            return NoContent();
        }
    }
}
