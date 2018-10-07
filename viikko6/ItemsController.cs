using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using vk2.Models;
using vk2.Processors;

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
/*
        [HttpGet("{id}")]
        public Task<Item> GetItem(ObjectId id, ObjectId playerId)
        {
            var value = itemPro.GetItem(id, playerId);
            return value;
        }

        [HttpGet]
        public Task</*Item[]*//*List<Item>> GetAllItems(ObjectId playerId)
        {
            var value = itemPro.GetAllItems(playerId);
            return value;
        }

        [HttpPost]
        public Task<Item> CreateItem([FromBody] NewItem item, ObjectId playerId)
        {
            var value = itemPro.CreateItem(item, playerId);
            return value;
        }

        [HttpPut("{id}")]
        public Task<Item> ModifyItem(ObjectId id, ModifiedItem item, ObjectId playerId)
        {
            var value = itemPro.ModifyItem(id, item, playerId);
            return value;
        }

        [HttpDelete("{id}")]
        public Task<Item> DeleteItem(ObjectId id, ObjectId playerId)
        {
            var value = itemPro.DeleteItem(id, playerId);
            return value;
        }*/
    }
}