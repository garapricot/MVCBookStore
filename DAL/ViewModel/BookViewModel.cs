using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using DAL.Entities;
using System.Collections.Generic;

namespace DAL.ViewModel
{
    public class BookViewModel
    {
        public int Id { get; set; }
        [Required]
        public int CountryId { get; set; }
        [Required]
        public int AuthorId { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public decimal Price { get; set; }
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        [DisplayName("Published Day")]
        [Required]
        public DateTime? PublishedDay { get; set; }
        public string Description { get; set; }
        public int PageCount { get; set; }
        public byte[] Image { get; set; }
        public Author Author { get; set; }
        public Country Country { get; set; }

       
    }
}