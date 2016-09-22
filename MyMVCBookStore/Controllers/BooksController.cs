using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Threading.Tasks;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using DAL.Context;
using DAL.Entities;
using DAL.Services;
using DAL.ViewModel;
using Microsoft.AspNet.Identity.Owin;

namespace MyMVCBookStore.Controllers
{
    public class BooksController : Controller
    {
        private readonly ApplicationDbContext _db = new ApplicationDbContext();
        private readonly BookService _service = new BookService();
        // GET: Books
        public ActionResult Index()
        {
            List<BookViewModel> result;
            try
            {
                result = _service.GetAllBook();
            }
            catch 
            {
                return Content("Siktir");
            }
            return View(result);
        }

        public ActionResult Search(string searchString)
        {
            List<BookViewModel> result;
            try
            {
                result = _service.GetSearchResult(searchString);
            }
            catch
            {
                return Content("Siktir");
            }
            return View("Index", result);
        }
        // GET: Books/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            BookViewModel result;
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            try
            {
                result = await _service.GetBookById(id);
            }
            catch
            {
                return Content("siktir");
            }
            return PartialView("_Details", result);
        }

        // GET: Books/Create
        public ActionResult Create()
        {
            ViewBag.AuthorId = new SelectList(_db.Authors, "Id", "FullName");
            ViewBag.CountryId = new SelectList(_db.Countires, "Id", "Name");
            return PartialView("_Create");
        }

        // POST: Books/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,CountryId,AuthorId,Title,Price,PublishedDay,Description,PageCount,Image")] Book book, BookViewModel result, HttpPostedFileBase upimage)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _service.SaveCreatedBook(book, upimage);
                    return Json(new { success = true });
                }
                ViewBag.AuthorId = new SelectList(_db.Authors, "Id", "FullName", book.AuthorId);
                ViewBag.CountryId = new SelectList(_db.Countires, "Id", "Name", book.CountryId);

            }
            catch 
            {
                return Content("Siktir");
            }
            return PartialView("_Create", result);
        }

        // GET: Books/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Book book = await _db.Books.FindAsync(id);
            if (book == null)
            {
                return HttpNotFound();
            }
            ViewBag.AuthorId = new SelectList(_db.Authors, "Id", "FullName", book.AuthorId);
            ViewBag.CountryId = new SelectList(_db.Countires, "Id", "Name", book.CountryId);
            return PartialView("_Edit", book);
        }

        // POST: Books/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,CountryId,AuthorId,Title,Price,PublishedDay,Description,PageCount,Image")] Book book, HttpPostedFileBase upimage)
        {
            if (ModelState.IsValid)
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
                return Json(new { success = true });

            }
            ViewBag.AuthorId = new SelectList(_db.Authors, "Id", "FirstName", book.AuthorId);
            ViewBag.CountryId = new SelectList(_db.Countires, "Id", "Name", book.CountryId);
            return PartialView("_Edit", book);
        }

        // GET: Books/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Book book = await _db.Books.FindAsync(id);
            if (book == null)
            {
                return HttpNotFound();
            }
            return PartialView("_Delete", book);
        }

        // POST: Books/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Book book = await _db.Books.FindAsync(id);
            _db.Books.Remove(book);
            await _db.SaveChangesAsync();
            return Json(new { success = true });
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}