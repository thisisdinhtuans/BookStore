﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Models
{
    public class Book
    {
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        [MaxLength(length: 100)]
        public string Description { get; set; }
        public string Language { get; set; }

        [Required,
        MaxLength(length: 13)]
        public string ISBN { get; set; }
        [Required,
         DataType(DataType.Date),
         Display(Name ="Date Published")]
        public DateTime DatePublished { get; set; }
        [Required,
         DataType(DataType.Currency)]
        public int Price { get; set; }
        [Required]
        public string Author { get; set; }
        [Display(Name ="Image URL")]
        public string ImageUrl { get; set; }
        public List<Review> Reviews { get; set; } = new List<Review>();
    }
}
