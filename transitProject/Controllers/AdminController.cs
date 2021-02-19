using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using transitProject.DAL;
using transitProject.Models;
using PagedList;
using System.Web.Security;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;

namespace transitProject.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        private TransitContext db = new TransitContext();

        //Login
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;

        public AdminController()
        {
        }

        public AdminController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }
        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }



        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Home");
        }




        //------------------------------------------------------------------------------------------
        // 1st step of authentication in Admin Login
        [AllowAnonymous]
        [HttpGet]
        public ActionResult AdminLogIn()
        {

            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AdminLogin(Admin adminObj, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                using (db)
                {
                    var obj = db.Admins.Where(a => a.userName.Equals(adminObj.userName) && a.password.Equals(adminObj.password)).FirstOrDefault();
                    if (obj != null)
                    {

                       Session["UserName"] = obj.userName.ToString();
                      //  FormsAuthentication.SetAuthCookie(adminObj.userName, false);
                   

                        //return RedirectToAction("AdminHome");
                        return RedirectToAction("Login","Account");

                    }
                    else
                    {
                        ViewBag.Message = "Invalid Account";
                        return View("AdminLogin");
                    }
                }

            }
            return View("AdminLogin");
        }
        //------------------------------------------------------------------------------------------



        //------------------------------------------------------------------------------------------
        // Displays the list of all Routes and stops to Admin
        public ViewResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            if (Session["UserName"] != null)
            {
                ViewBag.CurrentSort = sortOrder;
                ViewBag.StopNameSortParm = String.IsNullOrEmpty(sortOrder) ? "stopname_asc" : "";
                ViewBag.EtaSortParm = sortOrder == "Eta" ? "Eta_desc" : "Eta";
                if (searchString != null)
                {
                    page = 1;
                }
                else
                {
                    searchString = currentFilter;
                }

                ViewBag.CurrentFilter = searchString;
                var stops = from s in db.Stops
                            select s;
                if (!String.IsNullOrEmpty(searchString))
                {
                    stops = stops.Where(s => s.stopName.Contains(searchString)
                                           || s.source.Contains(searchString)
                                           || s.destination.Contains(searchString));

                }
                switch (sortOrder)
                {
                    case "stopname_asc":
                        stops = stops.OrderBy(s => s.stopName);
                        break;
                    case "Eta":
                        stops = stops.OrderBy(s => s.eta);
                        break;
                    case "Eta_desc":
                        stops = stops.OrderByDescending(s => s.eta);
                        break;
                    default:
                        stops = stops.OrderBy(s => s.stopId);
                        break;
                }
                int pageSize = 6;
                int pageNumber = (page ?? 1);
                return View(stops.ToPagedList(pageNumber, pageSize));
            }
            else
            {
                return View("AdminLogin");
            }
        }
        //------------------------------------------------------------------------------------------


        //------------------------------------------------------------------------------------------
        // Displays Admin Home Page
        public ActionResult AdminHome()
        {
            //if (Session["UserName"] != null)
            //{ 
            //    return View(); 
            //}
            //else
            //{
            //    return View("AdminLogin");
            //}


            return View();
        }
        //------------------------------------------------------------------------------------------

        //------------------------------------------------------------------------------------------
        // Displays Details of each stop to Admin
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Stop stop = db.Stops.Find(id);
            if (stop == null)
            {
                ViewBag.Message = "Not Found";
                return View("Error");
            }
            return View(stop);
        }
        //------------------------------------------------------------------------------------------


        //------------------------------------------------------------------------------------------
        //Admin can add a new Stop

        public ActionResult Create()
        {
            if (Session["UserName"] != null)
            {
                return View();
            }
            else
            {
                return View("AdminLogin");
            }

            
        }

        // POST: Admin/Create
       
        [HttpPost]
        [ValidateAntiForgeryToken]
        
        public ActionResult Create([Bind(Include = "stopId,stopName,source,destination,eta")] Stop stop)
        {
            if (ModelState.IsValid)
            {
                if (stop.source.Equals(stop.destination))
                {
                    ViewBag.Message = "Source & Destination cannot be same";
                    return View("Error");
                }
                else
                {
                    db.Stops.Add(stop);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

            }

            return View(stop);
        }
        //------------------------------------------------------------------------------------------


        //------------------------------------------------------------------------------------------
        //Admin can edit a existing Stop

        public ActionResult Edit(int? id)
        {

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Stop stop = db.Stops.Find(id);
            if (stop == null)
            {
                ViewBag.Message = "Not Found";
                return View("Error");
            }
            return View(stop);
        }

        // POST: Admin/Edit/5
 
        [HttpPost]
        [ValidateAntiForgeryToken]
      
        public ActionResult Edit([Bind(Include = "stopId,stopName,source,destination,eta")] Stop stop)
        {
            if (ModelState.IsValid)
            {
                if (stop.source.Equals(stop.destination))
                {
                    ViewBag.Message = "Source & Destination cannot be same";
                    return View("Error");
                }
                else
                {
                    db.Entry(stop).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            return View(stop);
        }
        //------------------------------------------------------------------------------------------


        //------------------------------------------------------------------------------------------
        //Admin can delete a existing Stop
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
            Stop stop = db.Stops.Find(id);
            if (stop == null)
            {
                ViewBag.Message = "Not Found";
                return View("Error");
            }
            return View(stop);
        }


        // POST: Admin/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
     
        public ActionResult Delete(int id)
        {
            try
            {
                Stop stop = db.Stops.Find(id);
                db.Stops.Remove(stop);
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
                //
                if (_userManager != null)
                {
                    _userManager.Dispose();
                    _userManager = null;
                }
                if (_signInManager != null)
                {
                    _signInManager.Dispose();
                    _signInManager = null;
                }
                //
            }
            base.Dispose(disposing);
        }
       
    }
}
