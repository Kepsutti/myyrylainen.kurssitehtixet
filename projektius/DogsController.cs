using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using vprokkis.Models;
using vprokkis.Processors;
using MongoDB.Bson;

namespace vprokkis.Controllers
{
    [Route("api/dogs")]
    [ApiController]
    public class DogsController : ControllerBase
    {
        private readonly DogsController dogPro;

        public DogsController(
            DogsProcessor myDogPro
        )
        {
            dogPro = myDogPro;
        }

        [HttpGet]
        [Route("{name}")]
        public Task<List<Player>> GetDogByName(string name)
        {
            return GetDogByName(name);
        }

        [HttpGet]
        public Task<List<Player>> GetAllBySex(int sex)
        {
            var value = dogPro.GetAllBySex(sex);
            return value;
        }

        [HttpGet]
        [Route("lead")]
        public Task<List<Player>> GetTopDog()
        {
            var value = dogPro.GetTopDog();
            return value;
        }
        public Task<List<Dog>> GetDog(string id)
        {
            if(id==null){
                return GetAllDogs();
            }
            var value = dogPro.GetDog(id);
            if (value == null)
            {
                return null;
            }
            return value;
        }

        
        public Task<List<Dog>> GetAllDogs()
        {
            var value = dogPro.GetAllDogs();
            if (value == null)
            {
                return null;
            }
            return value;
        }

        [HttpPost]
        public Task<Dog> CreateDog([FromBody] NewDog dog)
        {
            var value = dogPro.Create(Dog);
            if (value == null)
            {
                return null;
            }
            return value;
        }

        [HttpPut("{id}")]
        public Task<Dog> UpdateDog(string id, UpdatedDog dog)
        {
            var value = dogPro.UpdateDog(id, dog);
            if (value == null)
            {
                return null;
            }
            return value;
        }

        [HttpDelete("{id}")]
        public bool DeleteDog(string id)
        {
            bool value = dogPro.DeleteDog(id);
            if (value != true)
            {
                return "Deletion success";
            }
            return "Deletion failed";
        }
    }
}