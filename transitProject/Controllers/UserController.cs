using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using transitProject.DAL;
using transitProject.Models;

namespace transitProject.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        private TransitContext db = new TransitContext();
       
        //Display Search Route Page
        public ActionResult Index()
        {
            return View();
        }

        //Display the Searched Routes 
        public ActionResult Display(FormCollection coll)
        {
            string src = coll["SelectSource"].ToString();
            string dest = coll["SelectDestination"].ToString();
            var stopList = new List<Stop>();
            if (src.Equals("") || dest.Equals(""))
            {
                ViewBag.Message = "Please select Source & Destination";
                return View("Error");
            }
            else
            {
                if (src.Equals(dest))
                {
                    ViewBag.Message = "Source & Destination cannot be same";
                    return View("Error");
                }
                else
                {
                    if (!src.Equals(dest))
                    {
                        using (db)
                        {
                            stopList = db.Stops.Where(x => x.source.Equals(src) && x.destination.Equals(dest)).ToList();
                        }
                    }
                }
            }

            if (stopList.Count.Equals(0))
            {
                ViewBag.Message = "No Stops added for this route yet.";
            }
            return View(stopList);
        }

        //Contact and Feedback page
        public ActionResult Contact()
        {


            return View();
        }


        [HttpPost]

        public ActionResult Contact([Bind(Include = "Id,FullName,Email,Message,FeedbackDate")] Feedback feedback)
        {
            if (ModelState.IsValid)
            {
                feedback.FeedbackDate = DateTime.Now;
                db.Feedbacks.Add(feedback);
                db.SaveChanges();
                return RedirectToAction("Contact");
            }

            else
            {
                return View();
            }

        }

        //Display FAQ page
        public ActionResult Faq()
        {
            return View();
        }

        //Display location and map
        public ActionResult Map()
        {
            //var citys = new List<City>();
            //using(db)
            //{
            //    citys = db.Cities.Where(x =>x.Title.Equals("Toronto") || x.Title.Equals("Mississauga") || x.Title.Equals("Brampton")|| x.Title.Equals("Etobicoke")).ToList();

            //}

            //return View(citys);
            return View(db.Cities.ToList());

        }

        //Display Fares Policy
        public ActionResult Fares()
        {
            return View();
        }


        //Display Events Calender
        public ActionResult GetCalendar()
        {
            return View("Calendar");
        }

        public JsonResult GetEvents()
        {
            using (db)
            {
                var events = db.Events.ToList();
                return new JsonResult { Data = events, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }


        }
    }
}