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
            var statuscode = await _service.GetStatusCode(id);
            statuscode.ToString();
            try
            {               
                var result = await _service.GetBookListById(id);
                return PartialView("_Details", result);
            }
            catch
            {
                return Content("siktir "+  statuscode);
            }
            
        }

        // GET: Books/Create
        public ActionResult Create()
        {
            ViewBag.AuthorId = new SelectList(_service.GetAuthors(), "Id", "FullName");
            ViewBag.CountryId = new SelectList(_service.GetCountries(), "Id", "Name");
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
                ViewBag.AuthorId = new SelectList(_service.GetAuthors(), "Id", "FullName", book.AuthorId);
                ViewBag.CountryId = new SelectList(_service.GetCountries(), "Id", "Name", book.CountryId);

            }
            catch 
            {
                return Content("Siktir");
            }
            return PartialView("_Create", result);
        }

        // GET: Books/Edit/5
        public async Task<ActionResult> Edit(int? id,Book book)
        {
            var statuscode = await _service.GetStatusCode(id);
            statuscode.ToString();
            try
            {
                ViewBag.AuthorId = new SelectList(_service.GetAuthors(), "Id", "FullName", book.AuthorId);
                ViewBag.CountryId = new SelectList(_service.GetCountries(), "Id", "Name", book.CountryId);               
                var result = await _service.GetBookListById(id);
                return PartialView("_Edit", result);
            }
            catch
            {

                return Content("siktir "+statuscode);
            }
            
        }

        // POST: Books/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,CountryId,AuthorId,Title,Price,PublishedDay,Description,PageCount,Image")] Book book, HttpPostedFileBase upimage)
        {
            BookViewModel result=null;
            try
            {
                if (ModelState.IsValid)
                {
                   result= await _service.EditBook(book, upimage);
                    return Json(new { success = true });

                }
                ViewBag.AuthorId = new SelectList(_service.GetAuthors(), "Id", "FirstName", book.AuthorId);
                ViewBag.CountryId = new SelectList(_service.GetCountries(), "Id", "Name", book.CountryId);
            }
            catch
            {
                return Content("Siktir");
            }
            return PartialView("_Edit", result);
        }

        // GET: Books/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            var statuscode = await _service.GetStatusCode(id);
            statuscode.ToString();
            try
            {               
              var result=  await _service.GetBookListById(id);
                return PartialView("_Delete", result);
            }
            catch
            {
                return Content("siktir "+statuscode);
            }
            
        }

        // POST: Books/Delete/5
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