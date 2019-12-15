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
    public class Club_memberController : Controller
    {
        private Database1Entities db = new Database1Entities();

        // GET: Club_member
        public ActionResult Index()
        {
            var club_member = db.Club_member.Include(c => c.Club).Include(c => c.Position).Include(c => c.Student).Where(c =>c.Club_idClub == AccountController.user_id);
            return View(club_member.ToList());
        }

        // GET: Club_member/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Club_member club_member = db.Club_member.Find(id);
            if (club_member == null)
            {
                return HttpNotFound();
            }
            return View(club_member);
        }

        // GET: Club_member/Create
        public ActionResult Create()
        {
            ViewBag.Club_idClub = new SelectList(db.Clubs, "idClub", "Name");  
            ViewBag.Positions_idPositions = "Member";
            ViewBag.Student_idStudent = db.Students.FirstOrDefault(x => x.idStudent == AccountController.user_id).Name;
            return View();
        }

        // POST: Club_member/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "idClub_members,Positions_idPositions,Student_idStudent,Club_idClub,Why,How")] Club_member club_member)
        {
            if (ModelState.IsValid)
            {
                club_member.Student_idStudent = AccountController.user_id;
                club_member.Positions_idPositions = 1;
                club_member.idClub_members = db.Club_member.Max(u => u.idClub_members) + 1;
                db.Club_member.Add(club_member);
                db.SaveChanges();
                return RedirectToAction("Index", "Students");
            }

            ViewBag.Club_idClub = new SelectList(db.Clubs, "idClub", "Name");
            ViewBag.Positions_idPositions = "Member";
            ViewBag.Student_idStudent = db.Students.FirstOrDefault(x => x.idStudent == AccountController.user_id).Name;
            return View(club_member);
        }

        // GET: Club_member/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Club_member club_member = db.Club_member.Find(id);
            if (club_member == null)
            {
                return HttpNotFound();
            }
            ViewBag.Club_idClub = new SelectList(db.Clubs, "idClub", "Name", club_member.Club_idClub);
            ViewBag.Positions_idPositions = new SelectList(db.Positions, "idPositions", "Name", club_member.Positions_idPositions);
            ViewBag.Student_idStudent = new SelectList(db.Students, "idStudent", "Name", club_member.Student_idStudent);
            return View(club_member);
        }

        // POST: Club_member1/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idClub_members,Positions_idPositions,Student_idStudent,Club_idClub,Why,How")] Club_member club_member)
        {
            if (ModelState.IsValid)
            {
                db.Entry(club_member).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Club_idClub = new SelectList(db.Clubs, "idClub", "Name", club_member.Club_idClub);
            ViewBag.Positions_idPositions = new SelectList(db.Positions, "idPositions", "Name", club_member.Positions_idPositions);
            ViewBag.Student_idStudent = new SelectList(db.Students, "idStudent", "Name", club_member.Student_idStudent);
            return View(club_member);
        }

        // GET: Club_member1/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Club_member club_member = db.Club_member.Find(id);
            if (club_member == null)
            {
                return HttpNotFound();
            }
            return View(club_member);
        }

        // POST: Club_member1/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Club_member club_member = db.Club_member.Find(id);
            db.Club_member.Remove(club_member);
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
