﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FinalProject.DATA;
using Microsoft.AspNet.Identity;

namespace FinalProject.UI.Controllers
{
    [Authorize]
    public class ApplicationsController : Controller
    {
        private JobBoardEntities db = new JobBoardEntities();

        // GET: Applications
        public ActionResult Index()
        {
            var appUserID = User.Identity.GetUserId();
            var applications = db.Applications.Include(a => a.ApplicationStatu).Include(a => a.OpenPosition).Include(a => a.UserDetail);
            if (User.IsInRole("Seeker"))
            {

                var sortedApps = (from u in applications
                                  where u.UserID == appUserID
                                  select u);
                return View(sortedApps.ToList());
            }


            if (User.IsInRole("Manager"))
            {
                var sortedApps = (from u in applications
                                  where u.OpenPosition.Location.ManagerID == appUserID
                                  select u);
                return View(sortedApps.ToList());
            }
            else
            {
                return View(applications.ToList());
            }

        }

        // GET: Applications/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Application application = db.Applications.Find(id);
            if (application == null)
            {
                return HttpNotFound();
            }
            return View(application);
        }

        // GET: Applications/Create
        public ActionResult Create()
        {
            ViewBag.ApplicationStatusID = new SelectList(db.ApplicationStatus, "ApplicationStatusID", "Statusname");
            ViewBag.OpenPositionID = new SelectList(db.OpenPositions, "OpenPositionID", "OpenPositionID");
            ViewBag.UserID = new SelectList(db.UserDetails, "UserID", "FirstName");
            return View();
        }

        // POST: Applications/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ApplicationID,UserID,OpenPositionID,ApplicationDate,ManagerNotes,ApplicationStatusID,ResumeFilename")] Application application)
        {
            if (ModelState.IsValid)
            {
                db.Applications.Add(application);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ApplicationStatusID = new SelectList(db.ApplicationStatus, "ApplicationStatusID", "Statusname", application.ApplicationStatusID);
            ViewBag.OpenPositionID = new SelectList(db.OpenPositions, "OpenPositionID", "OpenPositionID", application.OpenPositionID);
            ViewBag.UserID = new SelectList(db.UserDetails, "UserID", "FirstName", application.UserID);
            return View(application);
        }

        // GET: Applications/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Application application = db.Applications.Find(id);
            if (application == null)
            {
                return HttpNotFound();
            }
            ViewBag.ApplicationStatusID = new SelectList(db.ApplicationStatus, "ApplicationStatusID", "Statusname", application.ApplicationStatusID);
            ViewBag.OpenPositionID = new SelectList(db.OpenPositions, "OpenPositionID", "OpenPositionID", application.OpenPositionID);
            ViewBag.UserID = new SelectList(db.UserDetails, "UserID", "FirstName", application.UserID);
            return View(application);
        }

        // POST: Applications/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ApplicationID,UserID,OpenPositionID,ApplicationDate,ManagerNotes,ApplicationStatusID,ResumeFilename")] Application application)
        {
            if (ModelState.IsValid)
            {
                db.Entry(application).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ApplicationStatusID = new SelectList(db.ApplicationStatus, "ApplicationStatusID", "Statusname", application.ApplicationStatusID);
            ViewBag.OpenPositionID = new SelectList(db.OpenPositions, "OpenPositionID", "OpenPositionID", application.OpenPositionID);
            ViewBag.UserID = new SelectList(db.UserDetails, "UserID", "FirstName", application.UserID);
            return View(application);
        }

        // GET: Applications/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Application application = db.Applications.Find(id);
            if (application == null)
            {
                return HttpNotFound();
            }
            return View(application);
        }

        // POST: Applications/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Application application = db.Applications.Find(id);
            db.Applications.Remove(application);
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
