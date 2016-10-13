using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Threading.Tasks;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using Dal.Entities;
using Microsoft.AspNet.Identity.Owin;
using Dal;
using DAL.Entities.Base;

namespace MyMVCBookStore.Controllers
{
    [HandleError]
    public class BooksController : Controller
    {
        private readonly BookService _service = new BookService();
        // GET: Books
        public ActionResult Index()
        {
            List<BookViewModel> result = null;
            try
            {
                result = _service.GetAllBook();
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
                return View("Error");
            }
            return View(result);
        }
        // GET: Books
        public ActionResult Search(string searchString)
        {
            List<BookViewModel> result = null;
            try
            {
                result = _service.GetSearchResult(searchString);
            }
            catch
            {
                return View("Error");
            }
            return View("Index", result);
        }
        // GET:
        public async Task<ActionResult> Details(int? id)
        {
            var statuscode = await _service.GetStatusCode(id);
            ViewBag.statuscode = statuscode.ToString();
            try
            {
                var result = await _service.GetBookListById(id);
                return PartialView("_Details", result);
            }
            catch
            {
                return View("HttpNotFound");
            }

        }

        // GET: 
        [Authorize]
        public ActionResult Create()
        {
            ViewBag.AuthorId = new SelectList(_service.GetAuthors(), "Id", "FullName");
            ViewBag.CountryId = new SelectList(_service.GetCountries(), "Id", "Name");
            return PartialView("_Create");
        }

        // POST:
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,CountryId,AuthorId,Title,Price,PublishedDay,Description,PageCount,Image")] Book book, BookViewModel result, HttpPostedFileBase upimage)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _service.SaveCreatedBook(book, upimage,result);
                    return Json(new { success = true });
                }
                ViewBag.AuthorId = new SelectList(_service.GetAuthors(), "Id", "FullName", book.AuthorId);
                ViewBag.CountryId = new SelectList(_service.GetCountries(), "Id", "Name", book.CountryId);

            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);   
                return View("Error");
            }
            return PartialView("_Create", result);
        }
        // GET: 
        [Authorize]
        public async Task<ActionResult> Edit(int? id, Book book)
        {
            var statuscode = await _service.GetStatusCode(id);
            ViewBag.statuscode = statuscode.ToString();
            try
            {
                ViewBag.AuthorId = new SelectList(_service.GetAuthors(), "Id", "FullName", book.AuthorId);
                ViewBag.CountryId = new SelectList(_service.GetCountries(), "Id", "Name", book.CountryId);
                var result = await _service.GetBookListById(id);
                return PartialView("_Edit", result);
            }
            catch
            {
                return View("HttpNotFound");
            }

        }

        // POST: 
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,CountryId,AuthorId,Title,Price,PublishedDay,Description,PageCount,Image")] Book book, HttpPostedFileBase upimage)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _service.EditBook(book, upimage);
                    return Json(new { success = true });

                }
                ViewBag.AuthorId = new SelectList(_service.GetAuthors(), "Id", "FullName", book.AuthorId);
                ViewBag.CountryId = new SelectList(_service.GetCountries(), "Id", "Name", book.CountryId);
            }
            catch
            {
                return View("Error");
            }
            return PartialView("_Edit");
        }

        // GET: 
        [Authorize]
        public async Task<ActionResult> Delete(int? id)
        {
            var statuscode = await _service.GetStatusCode(id);
            ViewBag.statuscode = statuscode.ToString();
            try
            {
               
                var result = await _service.GetBookListById(id);
                return PartialView("_Delete", result);
            }
            catch
            {
                return View("HttpNotFound");
            }

        }

        // POST:
        [Authorize]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            await _service.DeleteBook(id);
            return Json(new { success = true });
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _service.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}