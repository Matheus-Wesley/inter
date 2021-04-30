using System;
using System.ComponentModel.DataAnnotations;

namespace Commander.Models{
    public class Command{
        [Key]
        public int Id{get;set;}
        [Required]
        [MaxLength(250)]
        public string firstName{get;set;}
        [Required]
        //..^^
        public string surname{get;set;}
        [Required]

        public int age {get;set;}
        [Required]
        public DateTime creationDate { get; set; }
    }

}