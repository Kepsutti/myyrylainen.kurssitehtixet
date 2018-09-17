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
        public async Task<Item> GetItem(Guid id)
        {
            var value = await repo.GetItem(id);
            return value;
        }
        public async Task<Item[]> GetAllItems()
        {
            var value = await repo.GetAllItems();
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

        public async Task<Item> ModifyItem(Guid id, ModifiedItem item)
        {
            var value = await repo.ModifyItem(id, item);
            return value;
        }
        public async Task<Item> DeleteItem(Guid id)
        {
            var value = await repo.DeleteItem(id);
            return value;
        }
    }
}