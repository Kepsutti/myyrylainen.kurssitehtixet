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
using MongoDB.Driver;
using MongoDB.Bson;

namespace vk2
{
    public interface IRepository
    {
        Task<Player> Get(Guid id);
        Task</*Player[]*/List<Player>> GetAll();
        Task<Player> Create(Player player);
        Task<Player> Modify(Guid id, ModifiedPlayer player);
        Task<Player> Delete(Guid id);

        Task<Item> GetItem(Guid id, Guid playerId);
        Task</*Item[]*/List<Item>> GetAllItems(Guid playerId);
        Task<Item> CreateItem(Item item, Guid playerId);
        Task<Item> ModifyItem(Guid id, ModifiedItem item, Guid playerId);
        Task<Item> DeleteItem(Guid id, Guid playerId);  
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

    public class MongoDbRepository : IRepository
    {
        List<Player> playerList = new List<Player>();
        List<Item> itemList = new List<Item>();
        MongoClient dbClient = new MongoClient("mongodb://localhost:27017"); 
        

        public async Task<Player> Get(Guid id)
        {
            var db = dbClient.GetDatabase("game");
            var players = db.GetCollection<Player>("players");
            var filter = Builders<Player>.Filter.Eq("Id", id);
            var result = players.Find(filter).SingleOrDefault();
            return result;

            //for (int i = 0; i < playerList.Count; i++)
            //{
            //    if (playerList[i].Id == id)
            //        return playerList[i];
            //}
            //return null;
        }

        public async Task</*Player[]*/List<Player>> GetAll()
        {
            var db = dbClient.GetDatabase("game");
            var players = db.GetCollection<Player>("players");
            var filter = Builders<Player>.Filter.Empty;
            var result = players.Find(filter).ToList();
            return result;

            //Player[] arr1 = new Player[playerList.Count];
            //for (int i = 0; i < playerList.Count; i++)
            //{
            //    arr1[i] = playerList[i];
            //}
            //return arr1;
        }

        public async Task<Player> Create(Player player)
        {
            var db = dbClient.GetDatabase("game");

            var document = new BsonDocument
            {
                { "Id", player.Id },
                { "Name", player.Name },
                { "Level", 1 },
                { "Score", 0 },
                { "IsBanned", false },
                { "CreationTime", player.CreationTime },
            };
            var players = db.GetCollection<BsonDocument>("players");
            players.InsertOne(document);
            playerList.Add(player);
            return player;
        }

        public async Task<Player> Modify(Guid id, ModifiedPlayer player)
        {
            var db = dbClient.GetDatabase("game");
            var players = db.GetCollection<Player>("players");
            var filter = Builders<Player>.Filter.Eq("Id", id);
            var update = Builders<Player>.Update.Set("Score", player.Score).Set("Level", player.Level).CurrentDate("lastModified");
            var result = players.UpdateOne(filter, update);

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
            var db = dbClient.GetDatabase("game");
            var players = db.GetCollection<Player>("players");
            var filter = Builders<Player>.Filter.Eq("Id", id);
            var result = players.DeleteOne(filter);

            for (int i = 0; i < playerList.Count; i++)
            {
                if (playerList[i].Id == id)
                    playerList.RemoveAt(i);
            }
            return null;
        }
        
        public async Task<Item> GetItem(Guid id, Guid playerId)
        {
            var db = dbClient.GetDatabase("game");
            var players = db.GetCollection<Player>("players");
            var filter = Builders<Player>.Filter.Eq("Id", playerId);
            var result = players.Find(filter).SingleOrDefault();

            for (int i = 0; i < result.ItemList.Count; i++)
            {
                if (result.ItemList[i].Id == id)
                    return result.ItemList[i];
            }
            return null;
        }

        public async Task</*Item[]*/List<Item>> GetAllItems(Guid playerId)
        {
            var db = dbClient.GetDatabase("game");
            var players = db.GetCollection<Player>("players");
            var filter = Builders<Player>.Filter.Empty;
            var result = players.Find(filter).SingleOrDefault();
            return result.ItemList;

            //Item[] arr1 = new Item[itemList.Count];
            //for (int i = 0; i < itemList.Count; i++)
            //{
            //    arr1[i] = itemList[i];
            //}
            //return arr1;
        }

        public async Task<Item> CreateItem(Item item, Guid playerId)
        {
            var db = dbClient.GetDatabase("game");
            var players = db.GetCollection<Player>("players");
            var filter = Builders<Player>.Filter.Eq("Id", playerId);
            var result = players.Find(filter).SingleOrDefault();
            result.ItemList.Add(item);

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

        public async Task<Item> ModifyItem(Guid id, ModifiedItem item, Guid playerId)
        {
            var db = dbClient.GetDatabase("game");
            var players = db.GetCollection<Player>("players");
            var filter = Builders<Player>.Filter.Eq("Id", playerId);
            var result = players.Find(filter).SingleOrDefault();

            for (int i = 0; i < result.ItemList.Count; i++)
            {
                if (result.ItemList[i].Id == id)
                {
                    result.ItemList[i].Level = item.Level;
                    return result.ItemList[i];
                }
            }
            return null;
        }
        public async Task<Item> DeleteItem(Guid id, Guid playerId)
        {
            var db = dbClient.GetDatabase("game");
            var players = db.GetCollection<Player>("players");
            var filter = Builders<Player>.Filter.Eq("Id", playerId);
            var result = players.Find(filter).SingleOrDefault();

            for (int i = 0; i < result.ItemList.Count; i++)
            {
                if (result.ItemList[i].Id == id)
                    result.ItemList.RemoveAt(i);
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
