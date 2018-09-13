using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Web.Http;
using vk2.Models;

namespace vk2
{
    public interface IRepository
    {
        Task<Player> Get(Guid id);
        Task<Player[]> GetAll();
        Task<Player> Create(Player player);
        Task<Player> Modify(Guid id, ModifiedPlayer player);
        Task<Player> Delete(Guid id);
    }

    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }

    public class InMemoryRepository : IRepository
    {
        List<Player> playerList = new List<Player>();

        public async Task<Player> Get(Guid id)
        {
            for (int i = 0; i < playerList.Count; i++)
            {
                if (playerList[i].Id == id)
                    return playerList[i];
            }
            return null;
        }

        public async Task<Player[]> GetAll()
        {
            Player[] arr1 = new Player[playerList.Count];
            for (int i = 0; i < playerList.Count; i++)
            {
                arr1[i] = playerList[i];
            }
            return arr1;
        }

        public async Task<Player> Create(Player player)
        {
            playerList.Add(player);
            return player;
        }

        public async Task<Player> Modify(Guid id, ModifiedPlayer player)
        {
            for (int i = 0; i < playerList.Count; i++)
            {
                if (playerList[i].Id == id)
                {
                    playerList[i].Score = player.Score;
                    return playerList[i];
                }
            }
            return null;
        }
        public async Task<Player> Delete(Guid id)
        {
            for (int i = 0; i < playerList.Count; i++)
            {
                if (playerList[i].Id == id)
                    playerList.RemoveAt(i);
            }
            return null;
        }
    }
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
        public async Task<Player> Get(Guid id)
        {
            var value = await repo.Get(id);
            return value;
        }
        public async Task<Player[]> GetAll()
        {
            var value = await repo.GetAll();
            return value;
        }
        public async Task<Player> Create(NewPlayer player)
        {   
            Player player2 = new Player();
            player2.Name = player.Name;
            player2.Id = Guid.NewGuid();
            var value = await repo.Create(player2);
            return value;
        }

        public async Task<Player> Modify(Guid id, ModifiedPlayer player)
        {
            var value = await repo.Modify(id, player);
            return value;
        }
        public async Task<Player> Delete(Guid id)
        {
            var value = await repo.Delete(id);
            return value;
        }
    }

}
