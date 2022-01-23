using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DuAnQLNCKH.Models;
using System.Web.Security;

namespace DuAnQLNCKH.Controllers
{
    
    public class LoginController : Controller
    {
        DHTDTTDNEntities1 qLNCKHDHTDTD = new DHTDTTDNEntities1();
        // GET: Login
        public ActionResult RequestLogin()
        {
            
            return View();
        }
        [HttpGet]
        public ActionResult Index(string id)
        {
            Session.Clear();
            Session.RemoveAll();
            FormsAuthentication.SignOut();
            Session["Acess"] = "null";
            Session["UserName"] = "null";
            Session["ID"] = id;
             return View();  
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Login1(Account account, string returnURL = "")
        {
            //string pass = HashMD5.MD5Hash(account.PassWord);
            
            var obj = qLNCKHDHTDTD.Accounts.Where(a => a.Email==account.Email && a.PassWord==account.PassWord && a.Access==account.Access).FirstOrDefault();
            if (obj != null)
            {
                 Session["Acess"] = obj.Access.ToString();
                Session["UserName"] =  obj.Email.ToString();
                ConstantSession.USER = obj.Email;
                Session.Add(ConstantSession.USER, obj);
                FormsAuthentication.SetAuthCookie(account.Email, false);
                if (Url.IsLocalUrl(returnURL) && returnURL.Length > 1 && returnURL.StartsWith("/") && returnURL.StartsWith("//") && returnURL.StartsWith("/\\"))
                {
                    return Redirect(returnURL);
                }
                if (Session["Acess"].Equals("1"))
                {
                    return RedirectToAction("Index","TopicOfLecture"); 
                }
                else if (Session["Acess"].Equals("0"))
                {
                    return RedirectToAction("Index", "Admin");
                }
                else if (Session["Acess"].Equals("2"))
                {
                    return RedirectToAction("myTopicLecture", "TopicOfLecture");
                }    
                        
             }
                   
              
            ModelState.AddModelError("SessionLogin", "Tên đăng nhập hoặc mật khẩu không đúng");
            return View("Index");
        }

         
    }
}