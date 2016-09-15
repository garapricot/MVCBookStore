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
            return GetBookResult(books);
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
        public List<BookViewModel> Search(string searchString)
        {
            var books = new List<Book>();
            if (!string.IsNullOrEmpty(searchString))
            {
                books.AddRange(db.Books.Where(x => x.Title == searchString));
                books.AddRange(db.Books.Where(x => x.Author.FirstName == searchString));
                books.AddRange(db.Books.Where(x => x.Author.LastName == searchString));
                books.AddRange(db.Books.Where(x => x.Author.LastName + " " + x.Author.FirstName == searchString));
                books.AddRange(db.Books.Where(x => x.Author.FirstName + " " + x.Author.LastName == searchString));
                books.AddRange(db.Books.Where(x => x.Country.Name == searchString));
            }
            return GetBookResult(books);
        }
        public List<BookViewModel> GetBookResult(IEnumerable<Book> books)
        {
            var result = new List<BookViewModel>();
            foreach (var item in books)
            {
                result.Add(new BookViewModel
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
        public void Dispose()
        {
            db.Dispose();
        }
    }
}


