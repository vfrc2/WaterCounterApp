using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WaterCounterApp.Controllers
{
    public class WebController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ListHomes()
        {
            return View();
        }
        
        public ActionResult EditHome()
        {
            return View();
        }

    }
}