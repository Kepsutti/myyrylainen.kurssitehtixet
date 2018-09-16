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

namespace vk2.Models
{
    public class Player
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int Level { get; set; }
        public int Score { get; set; }
        public bool IsBanned { get; set; }
        public DateTime CreationTime { get; set; }
    }

    public class NewPlayer
    {
        public string Name { get; set; }
    }

    public class ModifiedPlayer
    {
        public int Score { get; set; }
        public int Level { get; set; }
    }

    public class Item
    {
        public Guid Id { get; set; }
        public int Level { get; set; }
        public string Type { get; set; }
        public DateTime CreationDate { get; set; }
        public Guid PlayerId { get; set; }
    }

    public class NewItem
    {
        [Required]
        [Range(1, 99)]
        public int Level { get; set; }
        [Required]
        [TypeCheck()]
        public string Type { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateTime CreationDate { get; set; }
        [Required]
        public Guid PlayerId { get; set; }
    }

    public class ModifiedItem
    {
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