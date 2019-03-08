using System;
using System.Web;
using System.Web.Mvc;

namespace STO.Presentation.Web.data
{
    public class ControlLogin : ActionFilterAttribute, IActionFilter
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            //Response.RedirectPermanent("url")
            try
            {
                if (string.IsNullOrEmpty(HttpContext.Current.Session["KullaniciId"].ToString()))
                {
                    HttpContext.Current.Response.Redirect("/Giris/Login");//false
                }
                else
                {
                    base.OnActionExecuting(filterContext);
                    //HttpContext.Current.Response.Redirect("/Genel/Durum", false);
                }
            }
            catch (Exception)
            {
                if (!HttpContext.Current.Response.IsRequestBeingRedirected)
                    HttpContext.Current.Response.Redirect("/Giris/Login");
                //HttpContext.Current.Response.Redirect("/Giris/Islem", false);
            }
        }
        
    }
}