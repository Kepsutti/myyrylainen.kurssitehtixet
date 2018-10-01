using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using vk2.Models;
using MongoDB.Bson;

namespace vk2.Processors
{
    public class PlayersProcessor
    {
        //IRepository repo;
        private readonly IRepository repo;

        public PlayersProcessor(
            IRepository myRepo
        )
        {
            repo = myRepo;
        }
        public async Task<List<Player>> Get(string id)
        {
            var value = await repo.Get(id);
            return value;
        }
        public async Task<List<Player>> GetByMinScore(int minScore)
        {
            var value = await repo.GetByMinScore(minScore);
            return value;
        }
        public async Task<List<Player>> GetByItemAmount(int itemAmount)
        {
            var value = await repo.GetByItemAmount(itemAmount);
            return value;
        }
        public async Task<List<Player>> GetMostCommonLevel()
        {
            var value = await repo.GetMostCommonLevel();
            return value;
        }
        public async Task</*Player[]*/List<Player>> GetAll()
        {
            var value = await repo.GetAll();
            return value;
        }
        public async Task<Player> Create(NewPlayer player)
        {   
            Player player2 = new Player();
            player2.Name = player.Name;
            //player2.id = ObjectId.NewGuid();
            player2.Level = 1;
            var value = await repo.Create(player2);
            return value;
        }

        public async Task<Player> Modify(string id, ModifiedPlayer player)
        {
            var value = await repo.Modify(id, player);
            return value;
        }
        public async Task<Player> Delete(string id)
        {
            var value = await repo.Delete(id);
            return value;
        }
    }
}