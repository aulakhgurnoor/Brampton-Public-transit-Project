using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using transitProject.DAL;

namespace transitProject.Controllers
{
    public class HomeController : Controller
    {
        private TransitContext db = new TransitContext();
        public ActionResult Index()
        {
            return View();
        }

        //public ActionResult About()
        //{
        //    ViewBag.Message = "Your application description page.";

        //    return View();
        //}
        public ActionResult SafetyRules()
        {

            return View();
        }
        public ActionResult Logout()
        {

            return View();
        }

      


       
        //public ActionResult Contact()
        //{
        //    ViewBag.Message = "Your contact page.";

        //    return View();
        //}
    }
}