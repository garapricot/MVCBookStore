using DAL.Context;
using DAL.Entities;
using DAL.ViewModel;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.ModelBinding;

namespace DAL.Services
{
    public class BookService : IDisposable
    {
        private readonly ApplicationDbContext _db=new ApplicationDbContext();
        public List<BookViewModel> GetEnumerableBooks(IEnumerable<Book> books)
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
        public BookViewModel GetBooks(Book book)
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
        public async Task<BookViewModel> GetBookById(int? id)
        {
            var  book = await _db.Books.FindAsync(id);
            return GetBooks(book);
        }
        public async Task<BookViewModel> SaveCreatedBook(Book book, HttpPostedFileBase upimage)
        {
                if (upimage != null)
                {
                    book.Image = new byte[upimage.ContentLength];
                    upimage.InputStream.Read(book.Image, 0, upimage.ContentLength);
                }
                _db.Books.Add(book);
                await _db.SaveChangesAsync();
            return GetBooks(book);
        }
        public List<BookViewModel> GetAllBook()
        {
            var books = _db.Books.ToList();
            return GetEnumerableBooks(books);
        }
       
        public List<BookViewModel> GetSearchResult(string searchString)
        {
            var books = new List<Book>();
            if (!string.IsNullOrEmpty(searchString))
            {
                books.AddRange(_db.Books.Where(x => x.Title == searchString));
                books.AddRange(_db.Books.Where(x => x.Author.FirstName == searchString));
                books.AddRange(_db.Books.Where(x => x.Author.LastName == searchString));
                books.AddRange(_db.Books.Where(x => x.Author.LastName + " " + x.Author.FirstName == searchString));
                books.AddRange(_db.Books.Where(x => x.Author.FirstName + " " + x.Author.LastName == searchString));
                books.AddRange(_db.Books.Where(x => x.Country.Name == searchString));
            }
            return GetEnumerableBooks(books);
        }
      
        public void Dispose()
        {
            _db.Dispose();
        }
    }
}


