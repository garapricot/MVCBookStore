using DAL.Context;
using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Services
{
    public class BookService
    {
        private ApplicationDbContext db;        
        public BookService(ApplicationDbContext dbContext)
        {
            db = dbContext;            
        }

        public void CreateBook(string title, int pageCount, string description, DateTime? publishedDay, Author author, byte[] image, decimal price)
        {
            var book = new Book { Title = title, PageCount = pageCount, Description = description, PublishedDay = publishedDay, Author = author, Image = image, Price = price };
            db.Books.Add(book);
            db.SaveChanges();
        }
    }
}
