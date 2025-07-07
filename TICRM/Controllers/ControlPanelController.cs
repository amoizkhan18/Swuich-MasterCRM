using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Web.Mvc;
using TICRM.BuisnessLayer;
using TICRM.DTOs;

namespace TICRM.Controllers
{
    [Authorize]
    public class ControlPanelController : Controller
    {


        // GET: ControlPanel
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Index1()
        {
            return View();
        }
        public ActionResult WorkFlowGojs()
        {
            return View();
        }

        public ActionResult demo()
        {
            return View();
        }



        public ActionResult testresult()
        {



            return View();
        }




    }


    
    public class student
    {

        // is a early binding example
        string _name;
        public string Name
        {
            set
            {
                _name = value;
            }
            get { return _name; }
        }
        // late binding 
        public object _obj = "name";

        public string GetObj
        {
            set { _obj = value; }
            get { return _obj.GetType().ToString(); }
        }
    }




}