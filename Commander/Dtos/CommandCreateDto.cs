using System;
using System.ComponentModel.DataAnnotations;

namespace Commander.Dtos{
    public class CommandCreateDto{
        
        [Required]
        [MaxLength(250)]
        public string firstName{get;set;}
        [Required]
        
        public string surname{get;set;}
        
        [Required]
        
        public int age {get;set;}
        
        
    }

}