using DuAnQLNCKH.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DuAnQLNCKH.Controllers
{
    public class UserLectureController : Controller
    {
        // GET: UserLecture
        DHTDTTDNEntities1 dHTDTTDNEntities1 = new DHTDTTDNEntities1();
        List<Faculty> faculties = new DHTDTTDNEntities1().Faculties.ToList();
        List<Information> informations = new DHTDTTDNEntities1().Information.ToList();
         List<Author> authors = new DHTDTTDNEntities1().Authors.ToList();
        List<Position> positions = new DHTDTTDNEntities1().Positions.ToList();
        public ActionResult Index()
        {
            string s = Session["UserName"].ToString();      
            var info = (from i in informations                                               
                        join f in faculties on i.IdKhoa equals f.IdFa
                        join p in positions on i.IdPo equals p.IdPo
                        where i.Email == s
                        select new TopicOfLectureView
                        {
                            information=i,
                            faculty=f,                             
                            position=p
                           
                        }).ToList();
               
            ViewBag.listInfo = info;
                
            return View();
            

        }
        [HttpPost]
        public ActionResult editInfo(Information model)
        {
            using (var context = new DHTDTTDNEntities1())
            {
                string s = Session["UserName"].ToString();
                // Use of lambda expression to access
                // particular record from a database
                var data = context.Information.FirstOrDefault(x => x.Email == s);

                // Checking if any such record exist 
                if (data != null)
                {
                    data.NameLe = model.NameLe;
                    data.Phone = model.Phone;
                    data.Email = model.Email;
                    data.DateOfBirth = model.DateOfBirth;
                    data.Address = model.Address;
                    data.CMND = model.CMND;
                    context.SaveChanges();

                    // It will redirect to 
                    // the Read method
                    return RedirectToAction("Index");
                }
                else
                    return View("Index");
            }

        }
        [HttpPost]
        public ActionResult changePassWord1(string OldPassword, string NewPassword, string ConfirmPassword)
        {
            string p = Session["UserName"].ToString();

            var passWord = (from a in dHTDTTDNEntities1.Accounts where a.Email == p select a.PassWord).First();

            ViewBag.passWord = passWord;
            if (OldPassword.Equals(passWord)) ViewBag.OldPassword = "";
            else ModelState.AddModelError("OldPassword", "Mật khẩu cũ không đúng");
            if (ConfirmPassword.Equals(NewPassword)) ViewBag.ConfirmPassword = "";
            else ModelState.AddModelError("ConfirmPassword", "Xác nhận lại mật khẩu không đúng");
            
            if (ViewBag.OldPassword == "" && ViewBag.ConfirmPassword == "")
            {
                ModelState.AddModelError("result", "Đổi mật khẩu thành công");
                using (DHTDTTDNEntities1 entities = new DHTDTTDNEntities1())
                {
                    entities.Database.ExecuteSqlCommand("update Account set PassWord='" + NewPassword + "' where UserName='" + Session["UserName"].ToString() + "'");
                    entities.SaveChanges();
                }


            }
            return View("changePassWord");
        }
        public ActionResult changePassWord()
        {

            Session["UserName"] = "lecture";
            Session["Acess"] = "2";

            string p = Session["UserName"].ToString();
             
            var accounts = dHTDTTDNEntities1.Accounts.ToList().Where(a => a.Email == Session["UserName"].ToString());
            //var accounts = dHTDTTDNEntities1.Accounts.Where(a => a.UserName == Session["UserName"].ToString()).ToList();
            //                ViewBag.passWord = dHTDTTDNEntities1.Accounts.Where(a => a.UserName == Session["UserName"].ToString());
             
            return View();
             


        }

    }
}