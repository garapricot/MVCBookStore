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
        public int CountryId { get; set; }
        public int AuthorId { get; set; }
        public string Title { get; set; }
        public decimal Price { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime PublishedDay { get; set; }
        public string Description { get; set; }
        public int PageCount { get; set; }
        public byte[] Image { get; set; }
        public Author Author { get; set; }
        public Country Country { get; set; }

       
    }
}