using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http;
using Microsoft.AspNetCore.Routing;

namespace CudJobUI.Services
{
    //public class SessionTimeoutAttribute : ActionFilterAttribute
    //{
    //    public override void OnActionExecuting(ActionExecutingContext filterContext)
    //    {
    //        //base.OnActionExecuting(filterContext);

    //        if (filterContext.HttpContext == null || filterContext.HttpContext.Session.GetString("EmailID") == null)
    //        {
    //            filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new
    //            {
    //                controller = "Account",
    //                action = "Login"
    //            }));
    //        }
    //    }

    //}

    public class SessionTimeoutAttribute : IActionFilter
    {
        public IHttpContextAccessor Httpaccessor { get; }

        public SessionTimeoutAttribute(IHttpContextAccessor httpaccessor)
        {
            Httpaccessor = httpaccessor;
        }
        public void OnActionExecuting(ActionExecutingContext context)
        {
            var cnt = context.ActionDescriptor.RouteValues.Count();            
            string Requested_action = string.Empty;
            string requested_Controller = string.Empty;
            if(cnt>0)
            {
                Requested_action = context.ActionDescriptor.RouteValues.FirstOrDefault().Value;
                requested_Controller = context.ActionDescriptor.RouteValues.LastOrDefault().Value;
            }
            if(Requested_action != "Login" && Requested_action != "Register" && Requested_action != "PasswordReset" && Requested_action != "PasswordConfirmation")
            {
                if(Requested_action == "Details")
                {
                    if(requested_Controller != "Job")
                    {
                        var sess = Httpaccessor.HttpContext.Session.Get("EmailID");
                        if (sess == null)
                        {
                            context.Result = new RedirectToRouteResult(new RouteValueDictionary(new
                            {
                                controller = "Account",
                                action = "Login"
                            }));
                        }
                    }                   
                }
                else
                {
                    var sess = Httpaccessor.HttpContext.Session.Get("EmailID");
                    if (sess == null)
                    {
                        context.Result = new RedirectToRouteResult(new RouteValueDictionary(new
                        {
                            controller = "Account",
                            action = "Login"
                        }));
                    }
                }
               
            }            
            // Do something before the action executes.
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            //throw new NotImplementedException();
        }        

    }

}
