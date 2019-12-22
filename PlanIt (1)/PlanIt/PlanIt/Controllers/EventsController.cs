using PlanIt.Controllers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace PlanIt.Models
{
    public class EventsController : Controller
    {
        public static int event_id = 0;
        private Database1Entities db = new Database1Entities();

        // GET: Events
        [Authorize(Roles = "Club")]
        public ActionResult Index()
        {
            if (db.Events.FirstOrDefault(c => c.Club_idClub == AccountController.user_id) != null)
            {
                event_id = db.Events.FirstOrDefault(c => c.Club_idClub == AccountController.user_id).idEvents;
            }
            var events = db.Events.Include(c => c.Club).Where(c => c.Club_idClub == AccountController.user_id);

            //return new JsonResult { Data = events, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            var seat = Events_has_Member.seats(50);
            ViewBag.seats = seat;
            return View(events.ToList());
        }

        // GET: Events/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Event @event = db.Events.Find(id);
            if (@event == null)
            {
                return HttpNotFound();
            }
            return View(@event);
        }

        // GET: Events/Create
        [Authorize(Roles = "Club")]
        public ActionResult Create()
        {
            ViewBag.Club_idClub = db.Clubs.FirstOrDefault(x => x.idClub == AccountController.user_id).Name;
            return View();
        }

        // POST: Events/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "idEvents,Club_idClub,Name,Date,Location,description, seats")] Event @event)
        {
            if (ModelState.IsValid)
            {
                @event.Club_idClub = AccountController.user_id;
                @event.idEvents = db.Events.Max(u => u.idEvents) + 1;
                db.Events.Add(@event);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Club_idClub = db.Clubs.FirstOrDefault(x => x.idClub == AccountController.user_id).Name;
            return View(@event);
        }

        // GET: Events/Edit/5

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Event @event = db.Events.Find(id);
            if (@event == null)
            {
                return HttpNotFound();
            }
            ViewBag.Club_idClub = db.Clubs.FirstOrDefault(x => x.idClub == AccountController.user_id).Name;
            return View(@event);
        }

        // POST: Events/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idEvents,Club_idClub,Name,Date,Location,description,seats")] Event @event)
        {
            if (ModelState.IsValid)
            {
                @event.Club_idClub = AccountController.user_id;
                db.Entry(@event).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Club_idClub = new SelectList(db.Clubs, "idClub", "Name", @event.Club_idClub);
            return View(@event);
        }

        // GET: Events/Delete/5
        [Authorize(Roles = "Club")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Event @event = db.Events.Find(id);
            if (@event == null)
            {
                return HttpNotFound();
            }
            return View(@event);
        }

        // POST: Events/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Event @event = db.Events.Find(id);
            db.Events.Remove(@event);
            db.SaveChanges();
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
