﻿using DAL.Context;
using DAL.Entities;
using DAL.ViewModel;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;

namespace DAL.Services
{
    public class BookService : IDisposable
    {
        private readonly ApplicationDbContext _db=new ApplicationDbContext();
        private bool disposed = false;
        public enum BookResult
        {
            Succeeded = 1,
            InValidModel,
            IdNotFound,
        }
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
        public async Task<BookResult> GetStatusCode(int? id)
        {
            var book = await _db.Books.FindAsync(id);
             if (id != null&& book!= null)
                {
                    return BookResult.Succeeded;
                }
            else
            {
                return BookResult.IdNotFound;
            }

        }
        public async Task<BookViewModel> GetBookListById(int? id)
        {
            Book book;           
            if (id != null)
            {
                book = await _db.Books.FindAsync(id);
            }
            else
            {
                book = null;
            }

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

        public async Task<BookViewModel> EditBook(Book book, HttpPostedFileBase upimage)
        {

            if (upimage != null)
            {
                book.Image = new byte[upimage.ContentLength];
                upimage.InputStream.Read(book.Image, 0, upimage.ContentLength);
                _db.Books.Attach(book);
                _db.Entry(book).State = EntityState.Modified;
            }
            else
            {
                _db.Books.Attach(book);
                _db.Entry(book).Property(m => m.Image).IsModified = false;
            }
            await _db.SaveChangesAsync();
            return GetBooks(book);
        }

        public async Task<BookViewModel> DeleteBook(int id)
        {
            Book book = await _db.Books.FindAsync(id);
            _db.Books.Remove(book);
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

        public DbSet<Author> GetAuthors()
        {
            return _db.Authors;
        }
        public DbSet<Country> GetCountries()
        {
            return _db.Countires;
        }
        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _db.Dispose();
                }
            }

            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}


