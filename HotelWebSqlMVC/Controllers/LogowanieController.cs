using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace HotelWebSqlMVC.Controllers
{
    public class LogowanieController : Controller
    {
        // GET: Logowanie/Index
        public ActionResult Login()
        {
            try
            {
                Session["UserID"] = null;
            }
            catch { }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(Models.User objUser)
        {
            if (ModelState.IsValid)
            {
                if (ModelState.IsValid)
                {
                    var temp = objUser.IsEmpoyee(objUser.UserName, objUser.Password);
                    if (!temp.Equals("-1"))
                    {
                        Session["UserID"] = Convert.ToInt32(temp);
                        return RedirectToAction("Index","Pracownik");
                    }
                    else
                    {
                        temp = objUser.IsCustomer(objUser.UserName, objUser.Password);
                        if(!temp.Equals("-1"))
                        {
                            Session["UserID"] = Convert.ToInt32(temp);
                            return RedirectToAction("Index","Klient");
                        }
                        else
                        ModelState.AddModelError("", "Login data is incorrect!");
                    }
                }
            }
            return View(objUser);   
        }
        public ActionResult UserDashBoard()
        {
            if (Session["UserID"] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Login");
            }
        }
    }
}