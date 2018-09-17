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

        Task<Item> GetItem(Guid id);
        Task<Item[]> GetAllItems();
        Task<Item> CreateItem(Item item, Guid playerId);
        Task<Item> ModifyItem(Guid id, ModifiedItem item);
        Task<Item> DeleteItem(Guid id);  
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
        List<Item> itemList = new List<Item>();

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
                    playerList[i].Level = player.Level;
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
        
        public async Task<Item> GetItem(Guid id)
        {
            for (int i = 0; i < itemList.Count; i++)
            {
                if (itemList[i].Id == id)
                    return itemList[i];
            }
            return null;
        }

        public async Task<Item[]> GetAllItems()
        {
            Item[] arr1 = new Item[itemList.Count];
            for (int i = 0; i < itemList.Count; i++)
            {
                arr1[i] = itemList[i];
            }
            return arr1;
        }

        public async Task<Item> CreateItem(Item item, Guid playerId)
        {
            itemList.Add(item);
            if (item.Type == "Sword")
            {
                for (int i = 0; i < playerList.Count; i++)
                {
                    if (playerList[i].Id == playerId)
                    {
                        if (playerList[i].Level < 3)
                        {
                        GameRuleValidation argEx = new GameRuleValidation("Game Rule Validation: Player must be at least level 3 to hold this item");
                        throw argEx;
                        }
                    }
                }
            }
            return item;
        }

        public async Task<Item> ModifyItem(Guid id, ModifiedItem item)
        {
            for (int i = 0; i < itemList.Count; i++)
            {
                if (itemList[i].Id == id)
                {
                    itemList[i].Level = item.Level;
                    return itemList[i];
                }
            }
            return null;
        }
        public async Task<Item> DeleteItem(Guid id)
        {
            for (int i = 0; i < itemList.Count; i++)
            {
                if (itemList[i].Id == id)
                    itemList.RemoveAt(i);
            }
            return null;
        }
    }

    [Serializable()]
    public class GameRuleValidation : System.Exception
    {
        public GameRuleValidation() : base() { }
        public GameRuleValidation(string message) : base(message) { }
        public GameRuleValidation(string message, System.Exception inner) : base(message, inner) { }

        // A constructor is needed for serialization when an
        // exception propagates from a remoting server to the client. 
        protected GameRuleValidation(System.Runtime.Serialization.SerializationInfo info,
            System.Runtime.Serialization.StreamingContext context) { }
    }

}
