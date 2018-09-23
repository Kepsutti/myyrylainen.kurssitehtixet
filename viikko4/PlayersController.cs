using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using vk2.Models;
using vk2.Processors;

namespace vk2.Controllers
{
    [Route("api/[controller]")]
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

        [HttpGet("{id}")]
        public Task<Player> Get(Guid id)
        {
            var value = plaPro.Get(id);
            if (value == null)
            {
                return null;
            }
            return value;
        }

        [HttpGet]
        public Task</*Player[]*/List<Player>> GetAll()
        {
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
        public Task<Player> Modify(Guid id, ModifiedPlayer player)
        {
            var value = plaPro.Modify(id, player);
            if (value == null)
            {
                return null;
            }
            return value;
        }

        [HttpDelete("{id}")]
        public Task<Player> Delete(Guid id)
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
