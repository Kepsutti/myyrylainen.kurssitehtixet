using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using vk2.Models;

namespace vk2.Controllers
{
    [Route("api/players/{playerId}/items")]
    [ApiController]
    public class ItemsController : ControllerBase
    {
        private readonly ItemsProcessor itemPro;

        public ItemsController(
            ItemsProcessor myItemPro
        )
        {
            itemPro = myItemPro;
        }

        [HttpGet("{id}")]
        public Task<Item> GetItem(Guid id)
        {
            var value = itemPro.GetItem(id);
            return value;
        }

        [HttpGet]
        public Task<Item[]> GetAllItems()
        {
            var value = itemPro.GetAllItems();
            return value;
        }

        [HttpPost]
        public Task<Item> CreateItem([FromBody] NewItem item, Guid playerId)
        {
            var value = itemPro.CreateItem(item, playerId);
            return value;
        }

        [HttpPut("{id}")]
        public Task<Item> ModifyItem(Guid id, ModifiedItem item)
        {
            var value = itemPro.ModifyItem(id, item);
            return value;
        }

        [HttpDelete("{id}")]
        public Task<Item> DeleteItem(Guid id)
        {
            var value = itemPro.DeleteItem(id);
            return value;
        }
    }
}