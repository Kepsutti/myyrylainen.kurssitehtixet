using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using vk2.Models;
using vk2.Repositories;

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
/*        public async Task<Item> GetItem(ObjectId id, ObjectId playerId)
        {
            var value = await repo.GetItem(id, playerId);
            return value;
        }
        public async Task</*Item[]*//*List<Item>> GetAllItems(ObjectId playerId)
        {
            var value = await repo.GetAllItems(playerId);
            return value;
        }
        public async Task<Item> CreateItem(NewItem item, ObjectId playerId)
        {   
            Item item2 = new Item();
            item2.Type = item.Type;
            item2.CreationDate = item.CreationDate;
            item2.id = ObjectId.NewGuid();
            item2.PlayerId = playerId;
            var value = await repo.CreateItem(item2, playerId);
            return value;
        }

        public async Task<Item> ModifyItem(ObjectId id, ModifiedItem item, ObjectId playerId)
        {
            var value = await repo.ModifyItem(id, item, playerId);
            return value;
        }
        public async Task<Item> DeleteItem(ObjectId id, ObjectId playerId)
        {
            var value = await repo.DeleteItem(id, playerId);
            return value;
        }*/
    }
}