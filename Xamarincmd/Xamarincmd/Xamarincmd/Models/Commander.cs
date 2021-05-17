using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Xamarincmd.Models
{
    class Commander
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(250)]
        public string firstName { get; set; }
        [Required]
        public string surname { get; set; }
        [Required]
        public int age { get; set; }
        [Required]
        public DateTime creationDate { get; set; }
    }
}
