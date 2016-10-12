using Dal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyMVCBookStore.Controllers
{
    public class HomeController : Controller
    {
        private readonly BookService _service = new BookService();
        public ActionResult Index()
        {
            List<BookViewModel> result = null;
            try
            {
               
            }
            catch
            {
                return View("Error");
            }
            return View(result);
        }
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