using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DAL.Entities.Base;
using Dal;

namespace MyMVCBookStore.Controllers
{
    public class AttributesController : Controller
    {
        private ApplicationDBContext db = new ApplicationDBContext();

        // GET: Attributes
        public async Task<ActionResult> Index()
        {
            var attributes = db.Attributes.Include(a => a.AttributeType).Include(a => a.Books);
            return View(await attributes.ToListAsync());
        }

        // GET: Attributes/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Attributes attributes = await db.Attributes.FindAsync(id);
            if (attributes == null)
            {
                return HttpNotFound();
            }
            return View(attributes);
        }

        // GET: Attributes/Create
        [Authorize]
        public ActionResult Create()
        {
            ViewBag.AttributeTypeId = new SelectList(db.AttributeTypes, "Id", "Name");
            ViewBag.BookID = new SelectList(db.Books, "Id", "Title");
            return View();
        }

        // POST: Attributes/Create
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Name,AttributeTypeId,BookID,Value")] Attributes attributes)
        {
            if (ModelState.IsValid)
            {
                db.Attributes.Add(attributes);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.AttributeTypeId = new SelectList(db.AttributeTypes, "Id", "Name", attributes.AttributeTypeId);
            ViewBag.BookID = new SelectList(db.Books, "Id", "Title", attributes.BookID);
            return View(attributes);
        }

        // GET: Attributes/Edit/5
        [Authorize]
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Attributes attributes = await db.Attributes.FindAsync(id);
            if (attributes == null)
            {
                return HttpNotFound();
            }
            ViewBag.AttributeTypeId = new SelectList(db.AttributeTypes, "Id", "Name", attributes.AttributeTypeId);
            ViewBag.BookID = new SelectList(db.Books, "Id", "Title", attributes.BookID);
            return View(attributes);
        }

        // POST: Attributes/Edit/5
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Name,AttributeTypeId,BookID,Value")] Attributes attributes)
        {
            if (ModelState.IsValid)
            {
                db.Entry(attributes).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.AttributeTypeId = new SelectList(db.AttributeTypes, "Id", "Name", attributes.AttributeTypeId);
            ViewBag.BookID = new SelectList(db.Books, "Id", "Title", attributes.BookID);
            return View(attributes);
        }

        // GET: Attributes/Delete/5
        [Authorize]
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Attributes attributes = await db.Attributes.FindAsync(id);
            if (attributes == null)
            {
                return HttpNotFound();
            }
            return View(attributes);
        }

        // POST: Attributes/Delete/5
        [Authorize]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Attributes attributes = await db.Attributes.FindAsync(id);
            db.Attributes.Remove(attributes);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
