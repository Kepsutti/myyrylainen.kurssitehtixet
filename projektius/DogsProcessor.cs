using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using vprokkis.Models;
using MongoDB.Bson;

namespace vprokkis.Processors
{
    public class DogsProcessor
    {
        private readonly IRepository repo;

        public DogsProcessor(
            IRepository myRepo
        )
        {
            repo = myRepo;
        }
        public async Task<List<Dog>> GetDog(string id)
        {
            var value = await repo.GetDog(id);
            return value;
        }
        public async Task<List<Dog>> GetDogByName(string name)
        {
            var value = await repo.GetDogByName(name);
            return value;
        }
        public async Task<List<Dog>> GetAllBySex(int sex)
        {
            var value = await repo.GetAllBySex(sex);
            return value;
        }
        public async Task<List<Dog>> GetTopDog()
        {
            var value = await repo.GetTopDog();
            return value;
        }
        public async Task<List<Dog>> GetAllDogs()
        {
            var value = await repo.GetAllDogs();
            return value;
        }
        public async Task<Dog> CreateDog(NewDog dog)
        {   
            Dog dog2 = new Dog();
            dog2.Name = dog.Name;
            dog2.id = ObjectId.GenerateNewId();
            var value = await repo.Create(dog2);
            return value;
        }

        public async Task<Dog> UpdateDog(string id, UpdatedDog dog)
        {
            var value = await repo.UpdateDog(id, dog);
            return value;
        }
        public async Task<Dog> DeleteDog(string id)
        {
            var value = await repo.DeleteDog(id);
            return value;
        }
    }
}