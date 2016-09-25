using Dal.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;

namespace Dal
{
    public class BookService : IDisposable
    {
        private readonly ApplicationDBContext _db = new ApplicationDBContext();
        private bool disposed = false;
        public enum BookResult
        {
            None=0,
            Succeeded = 1,
            InValidModel,
            IdNotFound,
        }
        public static List<BookViewModel> GetEnumerableBooks(IEnumerable<Book> books)
        {
            var result = new List<BookViewModel>();
            if (books != null)
            {
                result.AddRange(books.Select(item => new BookViewModel
                {
                    Id = item.Id, Title = item.Title, PageCount = item.PageCount, Description = item.Description, PublishedDay = item.PublishedDay, Author = item.Author, Image = item.Image, Price = item.Price + item.Country.CountryCode, Country = item.Country
                }));
            }
            
            return result;
        }
        public static BookViewModel GetBooks(Book book)
        {
            BookViewModel result=null;
            if (book != null)
            {
                result = new BookViewModel()
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
            }

            
            return result;
        }
        public async Task<BookResult> GetStatusCode(int? id)
        {
            var book = await _db.Books.FindAsync(id);
            if (id != null && book != null)
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

            }
            _db.Books.Attach(book);
            _db.Entry(book).State = EntityState.Modified;
            if (upimage == null)
            {
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
            //int numberOfObjectsPerPage = 4;
            //var books = _db.Books
            //  .Skip(numberOfObjectsPerPage * (Convert.ToInt32(grid_page)-1))
            //  .Take(numberOfObjectsPerPage).ToList();
            var books = _db.Books.ToList();
            return GetEnumerableBooks(books);
        }

        public List<BookViewModel> GetSearchResult(string searchValue)
        {
            var books = new List<Book>();
            if (!string.IsNullOrEmpty(searchValue))
            {
                books.AddRange(_db.Books.Where(x => x.Title == searchValue));
                books.AddRange(_db.Books.Where(x => x.Author.FirstName == searchValue));
                books.AddRange(_db.Books.Where(x => x.Author.LastName == searchValue));
                books.AddRange(_db.Books.Where(x => x.Author.LastName + " " + x.Author.FirstName == searchValue));
                books.AddRange(_db.Books.Where(x => x.Author.FirstName + " " + x.Author.LastName == searchValue));
                books.AddRange(_db.Books.Where(x => x.Country.Name == searchValue));
                return GetEnumerableBooks(books);
            }
            return GetEnumerableBooks(books);
        }


        public DbSet<Author> GetAuthors => _db.Authors;

        public DbSet<Country> GetCountries => _db.Countires;

        protected virtual void Dispose(bool disposing)
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


