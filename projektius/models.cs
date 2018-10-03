using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace vprokkis.Models
{
    public class Dog
    {
        [BsonId]
        public ObjectId _id { get; set; }
        [BsonElement("id")]
        public ObjectId id { get; set; }
        [BsonElement("name")]
        public string Name { get; set; }
        [BsonElement("age")]
        public int Age { get; set; }
        [BsonElement("sex")]
        public int Sex { get; set; }
        [BsonElement("isbanned")]
        public bool ValidHealthCheck { get; set; }
        [BsonElement("trophyhistory")]
        public List<Trophy> TrophyHistory { get; set; }
    }

    public class NewDog
    {
        [BsonElement("name")]
        public string Name { get; set; }
    }

    public class UpdatedDog
    {
        [BsonElement("age")]
        public int Age { get; set; }
        [BsonElement("sex")]
        [SexCheck()]
        public int Sex { get; set; }
        [BsonElement("validhealthcheck")]
        public int ValidHealthCheck { get; set; }
        [BsonElement("trophyhistory")]
        public List<Trophy> TrophyHistory { get; set; }
    }

    public class Trophy
    {
        [BsonId]
        public ObjectId id { get; set; }
        [BsonElement("rank")]
        public sting Rank { get; set; }
        [BsonElement("event")]
        public sting Event { get; set; }
        [BsonElement("year")]
        public int Year { get; set; }
        [BsonId]
        public ObjectId DogId { get; set; }
    }

    public class NewTrophy
    {
        [Required]
        [BsonElement("rank")]
        [RankCheck()]
        public string Rank { get; set; }
        [Required]
        [BsonElement("event")]
        public string Event { get; set; }
        [Required]
        [BsonElement("year")]
        [Range(1800, 2018)]
        public int Year { get; set; }
        [Required]
        [BsonId]
        public ObjectId DogId { get; set; }
    }

    public class UpdatedTrophy
    {
        [Required]
        [BsonId]
        public ObjectId DogId { get; set; }
    }

    public class RankCheckAttribute : ValidationAttribute//, IClientModelValidator
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationcontext)
        {
            NewTrophy newTrophy = (NewTrophy)validationcontext.ObjectInstance;

            if (newTrophy.Rank == "Gold" || newItem.Type == "Silver" || newItem.Type == "Bronze")
            {
                return ValidationResult.Success;
            }

            return new ValidationResult($"Invalid rank");
        }
    }

    public class SexCheckAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationcontext)
        {
            UpdatedDog updatedDog = (UpdatedDog)validationcontext.ObjectInstance;

            if (updatedDog.Sex == 0 || updatedDog.Sex == 1)
            {
                return ValidationResult.Success;
            }

            return new ValidationResult($"Invalid sex");
        }
    }
}