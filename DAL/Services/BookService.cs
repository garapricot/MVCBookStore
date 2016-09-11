using DAL.Context;
using DAL.Entities;
using DAL.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DAL.Services
{
    public class ViewModelService : IDisposable
    {
        private ApplicationDbContext db;
        public ViewModelService(ApplicationDbContext dbContext)
        {
            db = dbContext;
        }
        public List<BookViewModel> CreateBook()
        {
            var books = db.Books.ToList();
            List<BookViewModel> result = new List<BookViewModel>();
            foreach (var item in books)
            {
                result.Add(new BookViewModel()
                {
                    Id = item.Id,
                    Title = item.Title,
                    PageCount = item.PageCount,
                    Description = item.Description,
                    PublishedDay = item.PublishedDay,
                    Author = item.Author,
                    Image = item.Image,
                    Price = item.Price + item.Country.CountryCode,
                    Country = item.Country
                });
            }
            return result;
        }
        public BookViewModel EditDetalisDelObject(Book book)
        {
            var result = new BookViewModel()
            {
                Id = book.Id,
                Title = book.Title,
                PageCount = book.PageCount,
                Description = book.Description,
                PublishedDay = book.PublishedDay,
                Author = book.Author,
                Image = book.Image,
                Price = book.Price,
                Country = book.Country
            };
            return result;
        }
        public void Dispose()
        {
            db.Dispose();
        }
    }
}


