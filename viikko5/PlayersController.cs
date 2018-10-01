using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using vk2.Models;
using vk2.Processors;
using MongoDB.Bson;

namespace vk2.Controllers
{
    [Route("api/players")]
    [ApiController]
    public class PlayersController : ControllerBase
    {
        private readonly PlayersProcessor plaPro;

        public PlayersController(
            PlayersProcessor myPlaPro
        )
        {
            plaPro = myPlaPro;
        }

        [HttpGet]
        [Route("{name}")]
        public Task<List<Player>> GetName(string name)
        {
            return Get(name);
        }

        [HttpGet]
        [Route("{id}")]
        public Task<List<Player>> GetId(string id)
        {
            return Get(id);
        }

        [HttpGet]
        [Route("level")]
        public Task<List<Player>> GetMostCommonLevel()
        {
            return GetMostCommonLevel();
        }

        [HttpGet]
        public Task<List<Player>> GetByMinScore(int minScore=-1, int itemAmount=-1)
        {
            Console.WriteLine("minscore: " + minScore + " itemamount: " + itemAmount);
            if(minScore==-1 && itemAmount==-1){
                return GetAll();
            }
            if(minScore!=-1)
                return plaPro.GetByMinScore(minScore);
            if(itemAmount!=-1)
                return plaPro.GetByItemAmount(itemAmount);
            return null;
            
        }
        public Task<List<Player>> Get(string id)
        {
            if(id==null){
                return GetAll();
            }
            Console.WriteLine("kkkkkkkkkkkkkkkkk");
            var value = plaPro.Get(id);
            if (value == null)
            {
                return null;
            }
            return value;
        }

        
        public Task</*Player[]*/List<Player>> GetAll()
        {
            Console.WriteLine("getalllllllllllllllllll");
            var value = plaPro.GetAll();
            if (value == null)
            {
                return null;
            }
            return value;
        }

        [HttpPost]
        public Task<Player> Create([FromBody] NewPlayer player)
        {
            var value = plaPro.Create(player);
            if (value == null)
            {
                return null;
            }
            return value;
        }

        [HttpPut("{id}")]
        public Task<Player> Modify(string id, ModifiedPlayer player)
        {
            var value = plaPro.Modify(id, player);
            if (value == null)
            {
                return null;
            }
            return value;
        }

        [HttpDelete("{id}")]
        public Task<Player> Delete(string id)
        {
            var value = plaPro.Delete(id);
            if (value == null)
            {
                return null;
            }
            return value;
        }
    }
}