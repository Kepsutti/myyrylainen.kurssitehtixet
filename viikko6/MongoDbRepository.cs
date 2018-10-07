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

namespace vk2.Repositories
{
    public class MongoDbRepository : IRepository
    {
        List<Player> playerList = new List<Player>();
        List<Item> itemList = new List<Item>();
        MongoClient dbClient = new MongoClient("mongodb://localhost:27017"); 

        public async Task<List<Player>> Get(string id)
        {
            var db = dbClient.GetDatabase("game");
            var players = db.GetCollection<Player>("players");

            bool validId = true;
            try {
            var playerId = new ObjectId(id);
            } catch {
                validId = false;
            }
            
            if (validId) {
            var playerId = new ObjectId(id);
                return players.AsQueryable<Player>().Where(p => p.id == playerId).ToList();
            }
            else {
                return players.AsQueryable<Player>().Where(p => p.Name == id).ToList();
            }
            /*var filter = Builders<Player>.Filter.Eq("id", id);
            var result = players.Find(filter).SingleOrDefault();*/
            //for (int i = 0; i < playerList.Count; i++)
            //{
            //    if (playerList[i].Id == id)
            //        return playerList[i];
            //}
            //return null;
        }

        public async Task<List<Player>> GetByMinScore(int minScore)
        {
            var db = dbClient.GetDatabase("game");
            var players = db.GetCollection<Player>("players");
            return players.AsQueryable<Player>().Where(p => p.Score >= minScore).ToList();
        }

        public async Task<List<Player>> GetByItemAmount(int itemAmount)
        {
            var db = dbClient.GetDatabase("game");
            var players = db.GetCollection<Player>("players");
            return players.AsQueryable<Player>().Where(p => p.ItemList.Count == itemAmount).ToList();
        }
        public async Task<List<Player>> GetMostCommonLevel()
        {
            var db = dbClient.GetDatabase("game");
            var players = db.GetCollection<Player>("players");

            var project = new BsonDocument{ 
                    {"$project", new BsonDocument{
                                {"Level", 1}
                            } 
                    } 
                };
            var group = new BsonDocument{ 
                    {"$group", new BsonDocument{
                                {"_id", new BsonDocument{
                                    {
                                        "myLevel","$Level"
                                    }
                                }
                                },
                                {
                                    "Count", new BsonDocument{
                                        {
                                            "$sum", 1
                                        }
                                    }
                                }
                            } 
                    } 
                };
            var sort = new BsonDocument{ 
                    {"$sort", new BsonDocument{
                                {"Count", -1}
                            } 
                    } 
                };
            var limit = new BsonDocument{ 
                    {"$limit", 3
                    } 
                };

            var pipeline = new[] { project, group, sort, limit };
            var result = players.Aggregate<Player>(pipeline);

            /*players.Aggregate(
                {"$project" : {"Level" : 1}},
                {"$group" : {"_id" : "$Level", "Count" : {"$sum" : 1 }}},
                {"$sort" : {"Count" :  -1}},
                {"$limit" :  3}
            )*/
            return result.ToList();
        }

        public async Task</*Player[]*/List<Player>> GetAll()
        {
            var db = dbClient.GetDatabase("game");
            var players = db.GetCollection<Player>("players");

            var result = players.AsQueryable<Player>();
            var resultlist = result.ToList();
            foreach(var player in resultlist){
                Console.WriteLine("id: " + player.id);
            }
            return resultlist;

            /*var db = dbClient.GetDatabase("game");
            var players = db.GetCollection<Player>("Players");
            var filter = Builders<Player>.Filter.Empty;
            var cursor = players.Find(filter);
            var result = cursor.ToList();
            return result;*/

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

            //var document = new BsonDocument
            Player p = new Player{
                id = ObjectId.GenerateNewId(),
                Name = player.Name,
                Level = 1,
                Score = 0,
                IsBanned = false,
                CreationTime = DateTime.Now,
                ItemList = new List<Item>()
            };
            /*{
                { "id", player.id },
                { "Name", player.Name },
                { "Level", 1 },
                { "Score", 0 },
                { "IsBanned", false },
                { "CreationTime", player.CreationTime },
            };*/
            var players = db.GetCollection<BsonDocument>("players");
            players.InsertOne(p.ToBsonDocument());
            playerList.Add(player);
            return player;
        }

        public async Task<Player> Modify(string id, ModifiedPlayer player)
        {
            var db = dbClient.GetDatabase("game");
            var players = db.GetCollection<Player>("players");

            var playerId = new ObjectId(id);
            var result = players.AsQueryable<Player>().SingleOrDefault(p => p.id == playerId);
            result.Score += 100;
            return result;

            /*var filter = Builders<Player>.Filter.Eq("id", id);
            var update = Builders<Player>.Update.Set("Score", player.Score).Set("Level", player.Level).CurrentDate("lastModified");
            var result = players.UpdateOne(filter, update);

            for (int i = 0; i < playerList.Count; i++)
            {
                if (playerList[i].id == id)
                {
                    playerList[i].Score = player.Score;
                    playerList[i].Level = player.Level;
                    return playerList[i];
                }
            }
            return null;*/
        }
        public async Task<Player> Delete(string id)
        {
            var db = dbClient.GetDatabase("game");
            var players = db.GetCollection<Player>("players");

            var filter = Builders<Player>.Filter.Eq("id", id);
            var result = players.DeleteOne(filter);
            return null;

            /*for (int i = 0; i < playerList.Count; i++)
            {
                if (playerList[i].id == id)
                    playerList.RemoveAt(i);
            }
            return null;*/
        }
        
        public async Task<Item> GetItem(ObjectId id, ObjectId playerId)
        {
            var db = dbClient.GetDatabase("game");
            var players = db.GetCollection<Player>("players");
            var filter = Builders<Player>.Filter.Eq("id", playerId);
            var result = players.Find(filter).SingleOrDefault();

            for (int i = 0; i < result.ItemList.Count; i++)
            {
                if (result.ItemList[i].id == id)
                    return result.ItemList[i];
            }
            return null;
        }

        public async Task</*Item[]*/List<Item>> GetAllItems(ObjectId playerId)
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

        public async Task<Item> CreateItem(Item item, ObjectId playerId)
        {
            var db = dbClient.GetDatabase("game");
            var players = db.GetCollection<Player>("players");
            var filter = Builders<Player>.Filter.Eq("id", playerId);
            var result = players.Find(filter).SingleOrDefault();
            result.ItemList.Add(item);

            itemList.Add(item);
            if (item.Type == "Sword")
            {
                for (int i = 0; i < playerList.Count; i++)
                {
                    if (playerList[i].id == playerId)
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

        public async Task<Item> ModifyItem(ObjectId id, ModifiedItem item, ObjectId playerId)
        {
            var db = dbClient.GetDatabase("game");
            var players = db.GetCollection<Player>("players");
            var filter = Builders<Player>.Filter.Eq("id", playerId);
            var result = players.Find(filter).SingleOrDefault();

            for (int i = 0; i < result.ItemList.Count; i++)
            {
                if (result.ItemList[i].id == id)
                {
                    result.ItemList[i].Level = item.Level;
                    return result.ItemList[i];
                }
            }
            return null;
        }
        public async Task<Item> DeleteItem(ObjectId id, ObjectId playerId)
        {
            var db = dbClient.GetDatabase("game");
            var players = db.GetCollection<Player>("players");
            var filter = Builders<Player>.Filter.Eq("id", playerId);
            var result = players.Find(filter).SingleOrDefault();

            for (int i = 0; i < result.ItemList.Count; i++)
            {
                if (result.ItemList[i].id == id)
                    result.ItemList.RemoveAt(i);
            }
            return null;
        }
        
        public async Task WriteToLog(LogEntry logEntry)
        {
            var db = dbClient.GetDatabase("game");
            var logCollection = db.GetCollection<LogEntry>("log");

            await logCollection.InsertOneAsync(logEntry);
            return;
        }
        public async Task<LogEntry[]> GetLog()
        {
            var db = dbClient.GetDatabase("game");
            var logCollection = db.GetCollection<LogEntry>("log");

            var filter = Builders<LogEntry>.Filter.Empty;
            List<LogEntry> logList = await logCollection.Find(filter).ToListAsync();
            return logList.ToArray();
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