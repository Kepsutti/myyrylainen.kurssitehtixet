using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using vprokkis.Models;

namespace vprokkis.Processors
{
    public class TrophiesProcessor
    {
        private readonly IRepository repo;

        public TrophiesProcessor(
            IRepository myRepo
        )
        {
            repo = myRepo;
        }
        public async Task<Trophy> GetTrophy(ObjectId id, ObjectId dogId)
        {
            var value = await repo.GetTrophy(id, dogId);
            return value;
        }
        public async Task<List<Trophy>> GetAllTrophies(ObjectId dogId)
        {
            var value = await repo.GetAllTrophies(dogId);
            return value;
        }
        public async Task<Trophy> CreateTrophy(NewTrophy trophy, ObjectId dogId)
        {   
            Trophy trophy2 = new Trophy();
            trophy2.Rank = trophy.Rank;
            trophy2.Event = trophy.Event;
            trophy2.Year = trophy.Year;
            trophy2.id = ObjectId.GenerateNewId();
            trophy2.DogId = dogId;
            var value = await repo.CreateTrophy(trophy2, dogId);
            return value;
        }

        public async Task<Trophy> UpdateTrophy(ObjectId id, UpdatedTrophy trophy, ObjectId dogId)
        {
            var value = await repo.UpdateTrophy(id, trophy, dogId);
            return value;
        }
        public async Task<bool> DeleteTrophy(ObjectId id, ObjectId dogId)
        {
            var value = await repo.DeleteTrophy(id, dogId);
            return value;
        }
    }
}