using MyMVCBookStore.Data;
using MyMVCBookStore.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyMVCBookStore.Controllers
{
    public class HomeController : Controller
    {
        private const string GRID_PARTIAL_PATH = "~/Views/Home/_BookGrid.cshtml";

        private IGridMvcHelper gridMvcHelper;
        private IDemoData data;

        public HomeController()
        {
            this.gridMvcHelper = new GridMvcHelper();
            this.data = new BookData();
        }

        public ActionResult Index()
        {
            return View();
        }

        [ChildActionOnly]
        public ActionResult GetGrid()
        {
            var items = this.data.GetBooks().OrderBy(f => f.Price);
            var grid = this.gridMvcHelper.GetAjaxGrid(items);

            return PartialView(GRID_PARTIAL_PATH, grid);
        }

        [HttpGet]
        public ActionResult GridPager(int? page)
        {
            var items = this.data.GetBooks().OrderBy(f => f.Price);
            var grid = this.gridMvcHelper.GetAjaxGrid(items, page);
            object jsonData = this.gridMvcHelper.GetGridJsonData(grid, GRID_PARTIAL_PATH, this);

            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }
    }
}