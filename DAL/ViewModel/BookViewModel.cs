﻿using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Dal.Entities;
using System.Collections.Generic;
using DAL.Entities.Base;

namespace Dal
{
    public class BookViewModel
    {
        public int Id { get; set; }
        public int CountryId { get; set; }
        public int AuthorId { get; set; }
        public string Title { get; set; }
        [DisplayFormat(DataFormatString = "{0:C0}")]
        public decimal Price { get; set; }
        public string Description { get; set; }
        public int PageCount { get; set; }
        public byte[] Image { get; set; }
        public Author Author { get; set; }
        public Country Country { get; set; }
       
        public List<Attributes> Attribute { get; set; }


    }
}