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
    public class Events_has_MemberController : Controller
    {
        private Database1Entities db = new Database1Entities();

        [Authorize(Roles = "Club")]
        // GET: Events_has_Member
        public ActionResult Index()
        {
            var events_has_Member = db.Events_has_Member.Include(e => e.Club_member).Include(e => e.Event).Where(e=> e.Events_idEvents == EventsController.event_id);
            return View(events_has_Member.ToList());
        }

        // GET: Events_has_Member/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Events_has_Member events_has_Member = db.Events_has_Member.Find(id);
            if (events_has_Member == null)
            {
                return HttpNotFound();
            }
            return View(events_has_Member);
        }

        // GET: Events_has_Member/Create
        [Authorize(Roles = "Student")]
        public ActionResult Create()
        {
            ViewBag.Club_member_idClub_members = db.Students.FirstOrDefault(x => x.idStudent == AccountController.user_id).Name;
            ViewBag.Events_idEvents = new SelectList(db.Events, "idEvents", "Name");
            return View();
        }

        // POST: Events_has_Member/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "idEvents_has_Member,Club_member_idClub_members,Events_idEvents, Why")] Events_has_Member events_has_Member)
        {
            if (ModelState.IsValid)
            {
                events_has_Member.idEvents_has_Member = db.Events_has_Member.Max(u => u.idEvents_has_Member) + 1;
                events_has_Member.Club_member_idClub_members = db.Club_member.FirstOrDefault(u => u.Student_idStudent
                == AccountController.user_id).idClub_members;
                db.Events_has_Member.Add(events_has_Member);
                db.SaveChanges();
                return RedirectToAction("Index","Students");
            }


            ViewBag.Club_member_idClub_members = db.Students.FirstOrDefault(x => x.idStudent == AccountController.user_id).Name;
            ViewBag.Events_idEvents = new SelectList(db.Events, "idEvents", "Name"); 
            return View(events_has_Member);
        }

        // GET: Events_has_Member/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Events_has_Member events_has_Member = db.Events_has_Member.Find(id);
            if (events_has_Member == null)
            {
                return HttpNotFound();
            }
            ViewBag.Club_member_idClub_members = new SelectList(db.Club_member, "idClub_members", "Student_idStudent", events_has_Member.Club_member_idClub_members);
            ViewBag.Events_idEvents = new SelectList(db.Events, "idEvents", "Club_idClub", events_has_Member.Events_idEvents);
            return View(events_has_Member);
        }

        // POST: Events_has_Member/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idEvents_has_Member,Club_member_idClub_members,Events_idEvents")] Events_has_Member events_has_Member)
        {
            if (ModelState.IsValid)
            {
                db.Entry(events_has_Member).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Club_member_idClub_members = new SelectList(db.Club_member, "idClub_members", "Student_idStudent", events_has_Member.Club_member_idClub_members);
            ViewBag.Events_idEvents = new SelectList(db.Events, "idEvents", "Club_idClub", events_has_Member.Events_idEvents);
            return View(events_has_Member);
        }

        // GET: Events_has_Member/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Events_has_Member events_has_Member = db.Events_has_Member.Find(id);
            if (events_has_Member == null)
            {
                return HttpNotFound();
            }
            return View(events_has_Member);
        }

        // POST: Events_has_Member/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Events_has_Member events_has_Member = db.Events_has_Member.Find(id);
            db.Events_has_Member.Remove(events_has_Member);
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
