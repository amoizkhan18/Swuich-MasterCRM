using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TICRM.Controllers
{
    //[Authorize(Roles = "Administrator")]
    [Authorize]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }


        public ActionResult Contactdemo()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }


        public ActionResult Metronic()
        {
            ViewBag.Message = "Your Metronic page.";

            return View();
        }
        public ActionResult MetronicMaster()
        {
            ViewBag.Message = "Your MetronicMaster page.";

            return View();
        }

        public ActionResult MetronicDatatable()
        {
            ViewBag.Message = "Your MetronicDatatable page.";

            return View();
        }

        public ActionResult MetronicMaxlength()
        {
            ViewBag.Message = "Your MetronicMaxlength page.";
            return View();
        }
        

        // not reomve this this is in used

        public ActionResult SetLayout(string value)
        {
            Session["DynamicLayout"] = value;
            return Content("success");
        }

        public ActionResult demo()
        {
            return View();
        }

        public ActionResult autocompleteDemo()
        {
            return View();
        }

        public JsonResult autocompletedata()
        {
            string result = "[{ 'name': 'Afghanistan', 'code': 'AF'}, { 'name': 'Albania', 'code': 'AL'},{ 'name': 'Algeria', 'code': 'DZ'}]";
            return Json(result);
        }

    }
}