using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TICRM.BuisnessLayer;
using TICRM.DTOs;
using TICRM.Helper;

namespace TICRM.CRMFilters
{
    public class AccountActionFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            filterContext.Controller.ViewBag.CustomActionMessage1 = "Custom Action Filter: Message from OnActionExecuting method.";
            Utility.SendEmail("pre Account Creation");
        }

        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            //filterContext.Controller.ViewBag.CustomActionMessage2 = "Custom Action Filter: Message from OnActionExecuted method.";
        }

        public override void OnResultExecuting(ResultExecutingContext filterContext)
        {
            //filterContext.Controller.ViewBag.CustomActionMessage3 = "Custom Action Filter: Message from OnResultExecuting method.";
        }

        public override void OnResultExecuted(ResultExecutedContext filterContext)
        {
            //Utility.SendEmail("post Account Creation");

            // executes after the action method executes
            //WorkFlowActivityManager obj = new WorkFlowActivityManager(WorkFlowTypes.Account, false);
            //obj.ExecuteWorkFlow();
        }
    }
}