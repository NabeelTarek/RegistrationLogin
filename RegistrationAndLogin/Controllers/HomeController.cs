using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RegistrationAndLogin.Models;

namespace RegistrationAndLogin.Controllers
{
    public class HomeController : Controller
    {
        MyDatabaseEntities db = new MyDatabaseEntities();
        // GET: Home
        [Authorize]
        public ActionResult Index()
        {
            if (Session["Token"] != null)
            {
                Guid g = Guid.NewGuid();
                Session["Token"] = Convert.ToString(g);
                string UserID = Session["UserID"].ToString();
                var User = db.Users.Where(p => p.EmailID == UserID || p.IdCode == UserID).FirstOrDefault();
                return View(User);
            }
            else
            {
                return RedirectToAction("Login", "User");
            }            
        }
        public ActionResult TokenExpire()
        {
            Session.Remove("Token");            
            return View();
        }
    }
}