using System;
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
    public class OpenPositionsController : Controller
    {
        private JobBoardEntities db = new JobBoardEntities();

        // GET: OpenPositions
        public ActionResult Index(string searchFilter)
        {
            var appUserID = User.Identity.GetUserId();
            var userApps = db.Applications.Where(x => x.UserID == appUserID);

            var openPositions = db.OpenPositions.Include(o => o.Location).Include(o => o.Position);

            //only shows manager open positions at their location
            if (User.IsInRole("Manager"))
            {
                var sortedPositions = (from u in openPositions
                                       where u.Location.ManagerID == appUserID
                                       select u);
                return View(sortedPositions.ToList());
            }

            foreach (var op in openPositions)
            {
                foreach (var a in userApps)
                {
                    if (op.OpenPositionID == a.OpenPositionID)
                    {
                        op.HasApplied = true;
                    }
                }
            }

            if (string.IsNullOrEmpty(searchFilter))
            {
                return View(openPositions.ToList());
            }
            else
            {
                var searchResults = db.OpenPositions.Where(o => o.Position.Title.ToLower().Contains(searchFilter.ToLower()) || o.Location.City.ToLower().Contains(searchFilter.ToLower()) || o.Location.State.ToLower().Contains(searchFilter.ToLower())).OrderBy(o => o.Position.Title).ThenBy(o => o.Location.City) ;
                return View(searchResults);
            }
            ;
        }

        // GET: OpenPositions/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OpenPosition openPosition = db.OpenPositions.Find(id);
            if (openPosition == null)
            {
                return HttpNotFound();
            }
            return View(openPosition);
        }

        // GET: OpenPositions/Create
        [Authorize(Roles = "Manager, Admin")]
        public ActionResult Create()
        {
            ViewBag.LocationID = new SelectList(db.Locations, "LocationID", "FullLocation");
            ViewBag.PositionID = new SelectList(db.Positions, "PositionID", "Title");
            return View();
        }

        // POST: OpenPositions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Manager, Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "OpenPositionID,LocationID,PositionID")] OpenPosition openPosition)
        {
            if (ModelState.IsValid)
            {
                var appUserID = User.Identity.GetUserId();

                if (User.IsInRole("Manager"))
                {
                    openPosition.LocationID = (from l in db.Locations
                                               where l.ManagerID == appUserID
                                               select l.LocationID).FirstOrDefault();
                }

                db.OpenPositions.Add(openPosition);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.LocationID = new SelectList(db.Locations, "LocationID", "FullLocation", openPosition.LocationID);
            ViewBag.PositionID = new SelectList(db.Positions, "PositionID", "Title", openPosition.PositionID);
            return View(openPosition);
        }

        [Authorize(Roles = "Seeker")]
        public ActionResult Apply(int id)
        {
            Application userApp = new Application();//get application info
            var appUserID = User.Identity.GetUserId();// Get ID applicant

            var appUserDets = (from u in db.UserDetails
                               where u.UserID == appUserID
                               select u)
                               .FirstOrDefault();//get User info

            //Fill out application info 
            userApp.OpenPositionID = id;
            userApp.UserID = User.Identity.GetUserId();
            userApp.ApplicationStatusID = 2;//Applied status ID
            userApp.ResumeFilename = appUserDets.ResumeFilename;
            userApp.ApplicationDate = DateTime.Now;

            if (ModelState.IsValid)
            {
                //add application and save changes to DB
                db.Applications.Add(userApp);
                db.SaveChanges();
                return RedirectToAction("Index", "OpenPositions");

            }
            return RedirectToAction("Index", "OpenPositions");
        }



        // GET: OpenPositions/Edit/5
        [Authorize(Roles = "Manager, Admin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OpenPosition openPosition = db.OpenPositions.Find(id);
            if (openPosition == null)
            {
                return HttpNotFound();
            }
            ViewBag.LocationID = new SelectList(db.Locations, "LocationID", "FullLocation", openPosition.LocationID);
            ViewBag.PositionID = new SelectList(db.Positions, "PositionID", "Title", openPosition.PositionID);
            return View(openPosition);
        }

        // POST: OpenPositions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Manager, Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "OpenPositionID,LocationID,PositionID")] OpenPosition openPosition)
        {
            if (ModelState.IsValid)
            {
                db.Entry(openPosition).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.LocationID = new SelectList(db.Locations, "LocationID", "FullLocation", openPosition.LocationID);
            ViewBag.PositionID = new SelectList(db.Positions, "PositionID", "Title", openPosition.PositionID);
            return View(openPosition);
        }

        // GET: OpenPositions/Delete/5
        [Authorize(Roles = "Manager, Admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OpenPosition openPosition = db.OpenPositions.Find(id);
            if (openPosition == null)
            {
                return HttpNotFound();
            }
            return View(openPosition);
        }

        // POST: OpenPositions/Delete/5
        [Authorize(Roles = "Manager, Admin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            OpenPosition openPosition = db.OpenPositions.Find(id);
            db.OpenPositions.Remove(openPosition);
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
