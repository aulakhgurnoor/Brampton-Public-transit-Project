using PagedList;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using transitProject.DAL;
using transitProject.Models;

namespace transitProject.Controllers
{
    //[Authorize]

    public class OwnerController : Controller
    {
        private TransitContext db = new TransitContext();
        //------------------------------------------------------------------------------------------
        // Owner Login

        [AllowAnonymous]
        [HttpGet]
        public ActionResult OwnerLogIn()
        {

            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult OwnerLogIn(Owner ownerObj)
        {
            if (ModelState.IsValid)
            {
                using (db)
                {
                    var obj = db.Owners.Where(a => a.userName.Equals(ownerObj.userName) && a.password.Equals(ownerObj.password)).FirstOrDefault();
                    if (obj != null)
                    {

                        Session["UserName"] = obj.userName.ToString();
                        FormsAuthentication.SetAuthCookie(ownerObj.userName, false);

                        return RedirectToAction("OwnerHome");

                    }
                    else
                    {
                        ViewBag.Message = "Invalid Account";
                        return View("OwnerLogin");
                    }
                }

            }
            return View("OwnerLogin");
        }
        //------------------------------------------------------------------------------------------

        //------------------------------------------------------------------------------------------
        //Owner Logout
        [HttpGet]
        public ActionResult Logout()
        {
            Session.Remove("UserName");

            FormsAuthentication.SignOut();


            return RedirectToAction("Logout", "Home");
        }
        //------------------------------------------------------------------------------------------

        //------------------------------------------------------------------------------------------
        //Display List of All Admins for Owner
        public ViewResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            if (Session["UserName"] != null)
            {
                ViewBag.CurrentSort = sortOrder;
                 ViewBag.UserNameSortParm = String.IsNullOrEmpty(sortOrder) ? "UserName_desc" : "";
                ViewBag.AdminIdSortParm = sortOrder == "AdminId" ? "AdminId_desc" : "AdminId";

                if (searchString != null)
                {
                    page = 1;
                }
                else
                {
                    searchString = currentFilter;
                }

                ViewBag.CurrentFilter = searchString;
                
                var admins = from a in db.Admins
                            select a;
                if (!String.IsNullOrEmpty(searchString))
                {
                    admins = admins.Where(a => a.userName.Contains(searchString));

                }
                switch (sortOrder)
                {
                    case "UserName_desc":
                        admins = admins.OrderByDescending(a => a.userName);
                        break;
                    case "AdminId":
                        admins = admins.OrderBy(a => a.adminId);
                        break;
                    case "AdminId_desc":
                        admins = admins.OrderByDescending(a => a.adminId);
                        break;
                    

                    default:
                        admins = admins.OrderBy(a => a.adminId);
                        break;
                }
                int pageSize = 3;
                int pageNumber = (page ?? 1);
                return View(admins.ToPagedList(pageNumber, pageSize));
            }
            else
            {
                return View("AdminLogin");
            }
        }
        //------------------------------------------------------------------------------------------


        //------------------------------------------------------------------------------------------
        //Display Owner Home Page
        public ActionResult OwnerHome()
        {
            if (Session["UserName"] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("OwnerLogin");
            }
           
        }
        //------------------------------------------------------------------------------------------

        //------------------------------------------------------------------------------------------
        //Display the user feedback to Owner
        public ActionResult ViewFeedback()
        {
            if (Session["UserName"] != null)
            {
                return View(db.Feedbacks.ToList());
            }
            else
            {
                return View("OwnerLogin");
            }
            
        }
        //------------------------------------------------------------------------------------------

        //------------------------------------------------------------------------------------------
        //Display the details of each user feedback to Owner
        public ActionResult DetailsFeedback(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Feedback feedback = db.Feedbacks.Find(id);
            if (feedback == null)
            {
                ViewBag.Message = "Not Found";
                return View("Error");
            }
            return View(feedback);
        }
        //------------------------------------------------------------------------------------------

        //------------------------------------------------------------------------------------------
        // Display details of each admin to Owner
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Admin admin = db.Admins.Find(id);
            if (admin == null)
            {
                ViewBag.Message = "Not Found";
                return View("Error");
            }
            return View(admin);
        }
        //------------------------------------------------------------------------------------------

        //------------------------------------------------------------------------------------------
        // Owner can Insert a new Admin
        public ActionResult Create()

        {

            if (Session["UserName"] != null)
            {
                return View();
            }
            else
            {
                return View("OwnerLogin");
            }
            
        }

        // POST: Owner/Create
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "adminId,userName,password")] Admin admin)
        {
            if (ModelState.IsValid)
            {
                db.Admins.Add(admin);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(admin);
        }
        //------------------------------------------------------------------------------------------

        //------------------------------------------------------------------------------------------
        // Owner can Edit an existing Admin
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Admin admin = db.Admins.Find(id);
            if (admin == null)
            {
                ViewBag.Message = "Not Found";
                return View("Error");
            }
            return View(admin);
        }

        // POST: Owner/Edit/5
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "adminId,userName,password")] Admin admin)
        {
            if (ModelState.IsValid)
            {
                db.Entry(admin).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(admin);
        }
        //------------------------------------------------------------------------------------------

        //------------------------------------------------------------------------------------------
        // Owner can Delete an existing Admin
        public ActionResult Delete(int? id, bool? saveChangesError = false)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //
            if (saveChangesError.GetValueOrDefault())
            {
                ViewBag.ErrorMessage = "Delete failed.Try again.";
            }
            //
            Admin admin = db.Admins.Find(id);

            if (admin == null)
            {
                ViewBag.Message = "Not Found";
                return View("Error");
            }
            return View(admin);
        }


        // POST: Admin/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            try
            {

                Admin admin = db.Admins.Find(id);
                db.Admins.Remove(admin);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (DataException)
            {

                return RedirectToAction("Delete", new { id = id, saveChangesError = true });
            }
            return RedirectToAction("Index");
        }
        //------------------------------------------------------------------------------------------

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
