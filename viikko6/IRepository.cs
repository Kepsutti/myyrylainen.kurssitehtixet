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
        Task<List<Player>> Get(string id);
        Task<List<Player>> GetByMinScore(int minScore);
        Task<List<Player>> GetByItemAmount(int itemAmount);
        Task<List<Player>> GetMostCommonLevel();
        Task</*Player[]*/List<Player>> GetAll();
        Task<Player> Create(Player player);
        Task<Player> Modify(string id, ModifiedPlayer player);
        Task<Player> Delete(string id);

        Task<Item> GetItem(ObjectId id, ObjectId playerId);
        Task</*Item[]*/List<Item>> GetAllItems(ObjectId playerId);
        Task<Item> CreateItem(Item item, ObjectId playerId);
        Task<Item> ModifyItem(ObjectId id, ModifiedItem item, ObjectId playerId);
        Task<Item> DeleteItem(ObjectId id, ObjectId playerId);  

        Task WriteToLog(LogEntry logEntry);
        Task<LogEntry[]> GetLog();
    }
}