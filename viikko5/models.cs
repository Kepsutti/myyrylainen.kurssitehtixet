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

namespace vk2.Models
{
    public class Player
    {
        [BsonId]
        public ObjectId _id { get; set; }
        [BsonElement("id")]
        public ObjectId id { get; set; }
        [BsonElement("name")]
        public string Name { get; set; }
        [BsonElement("level")]
        public int Level { get; set; }
        [BsonElement("score")]
        public int Score { get; set; }
        [BsonElement("isbanned")]
        public bool IsBanned { get; set; }
        [BsonElement("creationtime")]
        public DateTime CreationTime { get; set; }
        [BsonElement("itemlist")]
        public List<Item> ItemList { get; set; }
    }

    public class NewPlayer
    {
        [BsonElement("name")]
        public string Name { get; set; }
    }

    public class ModifiedPlayer
    {
        [BsonElement("score")]
        public int Score { get; set; }
        [BsonElement("level")]
        public int Level { get; set; }
        [BsonElement("itemlist")]
        public List<Item> ItemList { get; set; }
    }

    public class Item
    {
        [BsonId]
        public ObjectId id { get; set; }
        [BsonElement("level")]
        public int Level { get; set; }
        [BsonElement("type")]
        public string Type { get; set; }
        [BsonElement("creationdate")]
        public DateTime CreationDate { get; set; }
        [BsonId]
        public ObjectId PlayerId { get; set; }
    }

    public class NewItem
    {
        [Required]
        [BsonElement("level")]
        [Range(1, 99)]
        public int Level { get; set; }
        [Required]
        [BsonElement("type")]
        [TypeCheck()]
        public string Type { get; set; }
        [Required]
        [BsonElement("creationtime")]
        [DataType(DataType.Date)]
        public DateTime CreationDate { get; set; }
        [Required]
        [BsonId]
        public ObjectId PlayerId { get; set; }
    }

    public class ModifiedItem
    {
        [BsonElement("level")]
        [Range(1, 99)]
        public int Level { get; set; }
    }

    public class TypeCheckAttribute : ValidationAttribute//, IClientModelValidator
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationcontext)
        {
            NewItem newItem = (NewItem)validationcontext.ObjectInstance;

            if (newItem.Type == "Sword" || newItem.Type == "Potion" || newItem.Type == "Shield")
            {
                return ValidationResult.Success;
            }

            return new ValidationResult($"Invalid item type");
        }
    }
}