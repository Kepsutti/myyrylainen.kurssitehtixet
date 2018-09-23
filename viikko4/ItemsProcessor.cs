using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using vk2.Models;

namespace vk2.Processors
{
    public class ItemsProcessor
    {
        private readonly IRepository repo;

        public ItemsProcessor(
            IRepository myRepo
        )
        {
            repo = myRepo;
        }
        public async Task<Item> GetItem(Guid id, Guid playerId)
        {
            var value = await repo.GetItem(id, playerId);
            return value;
        }
        public async Task</*Item[]*/List<Item>> GetAllItems(Guid playerId)
        {
            var value = await repo.GetAllItems(playerId);
            return value;
        }
        public async Task<Item> CreateItem(NewItem item, Guid playerId)
        {   
            Item item2 = new Item();
            item2.Type = item.Type;
            item2.CreationDate = item.CreationDate;
            item2.Id = Guid.NewGuid();
            item2.PlayerId = playerId;
            var value = await repo.CreateItem(item2, playerId);
            return value;
        }

        public async Task<Item> ModifyItem(Guid id, ModifiedItem item, Guid playerId)
        {
            var value = await repo.ModifyItem(id, item, playerId);
            return value;
        }
        public async Task<Item> DeleteItem(Guid id, Guid playerId)
        {
            var value = await repo.DeleteItem(id, playerId);
            return value;
        }
    }
}