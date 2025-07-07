using System;
using System.Net;
using System.Text;
using System.Web.Mvc;

namespace TICRM.Controllers
{
    public class CloudConfigurationController : Controller
    {
        // GET: CloudConfiguration
        public ActionResult Index()
        {
            return View();
        }
        private static string orgId = "";
        private static string appId = "";
        private static string apiKey = "";
        private static string authToken = "";

        public JsonResult ConfigureItToCloud(string UserName, string Password, string OrganizationId, string APIKey, string AuthToken, string DeviceType, string DeviceId)
        {
            try
            {


            }
            catch (Exception)
            {
                // ignore
            }

            return Json("", JsonRequestBehavior.AllowGet);
        }




        public ActionResult IBMCloudBrowse()
        {
            return View();
        }



        public ActionResult SubmitIBMCloudBrowse(string Option, string OrganizationId, string APIKey, string AuthToken, string DeviceType, string DeviceId)
        {
            if (Option == "GetAllDevices")
            {

            }
            else if (Option == "RegisterDevice")
            {

            }
            else if (Option == "RegisterMultipleDevices")
            {

            }
            return View();
        }


















    }

}