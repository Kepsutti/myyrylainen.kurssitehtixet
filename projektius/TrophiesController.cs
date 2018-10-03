using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using vprokkis.Models;
using vprokkis.Processors;

namespace vprokkis.Controllers
{
    [Route("api/dogs/{dogId}/trophies")]
    [ApiController]
    public class TrophiesController : ControllerBase
    {
        private readonly TrophiesProcessor troPro;

        public TrophiesController(
            TrophiesProcessor myTroPro
        )
        {
            troPro = myTroPro;
        }

        [HttpGet("{id}")]
        public Task<Trophy> GetTrophy(ObjectId id, ObjectId dogId)
        {
            var value = troPro.GetTrophy(id, dogId);
            return value;
        }

        [HttpGet]
        public Task<List<Trophy>> GetAllTrophies(ObjectId dogId)
        {
            var value = troPro.GetAllTrophies(dogId);
            return value;
        }

        [HttpPost]
        public Task<Trophy> CreateTrophy([FromBody] NewTrophy trophy, ObjectId dogId)
        {
            var value = troPro.CreateTrophy(trophy, dogId);
            return value;
        }

        [HttpPut("{id}")]
        public Task<Trophy> UpdateTrophy(ObjectId id, UpdatedTrophy trophy, ObjectId dogId)
        {
            var value = troPro.UpdateTrophy(id, trophy, dogId);
            return value;
        }

        [HttpDelete("{id}")]
        public Task<bool> DeleteTrophy(ObjectId id, ObjectId dogId)
        {
            var value = troPro.DeleteTrophy(id, dogId);
            return value;
        }
    }
}