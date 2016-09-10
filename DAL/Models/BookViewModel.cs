using DAL.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DAL.Models
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
        [DisplayName("Published Day")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? PublishedDay { get; set; }
        public string Description { get; set; }
        public int PageCount { get; set; }
        public byte[] Image { get; set; }
        public Author Author { get; set; }
        public Country Country { get; set; }

    }
}