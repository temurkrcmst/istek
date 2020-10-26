using istek.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace istek.Areas.Admin.Controllers
{
    public class HomeController : Controller
    {
        // GET: Admin/Home
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult AddMovie()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddActors()
        {
            return View();
        }
        public ActionResult AddScenario()
        {
            return View();
        }
        public ActionResult AddCompany()
        {
            return View();
        }
        public ActionResult ListActors()
        {
            return View();
        }
        public ActionResult ListCompany()
        {
            return View();
        }
        public ActionResult ListMovie()
        {
            return View();
        }
        public ActionResult ListScenario()
        {
            return View();
        }
        public ActionResult EditCompany()
        {
            return View();
        }
        public ActionResult EditScenario()
        {
            return View();
        }
        public ActionResult EditActors()
        {
            return View();
        }
        public ActionResult EditMovie()
        {
            return View();
        }
    }
}