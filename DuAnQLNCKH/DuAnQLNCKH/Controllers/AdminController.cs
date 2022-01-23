 using DuAnQLNCKH.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DuAnQLNCKH.Controllers
{
    public class AdminController : Controller
    {
        DHTDTTDNEntities1 qLNCKHDHTDTD = new DHTDTTDNEntities1();
        // GET: Admin
        //[Authorize(Roles = "0")]
        public ActionResult Index(string seach, int page = 1, int pagesize = 5)
        {

            
            var model = new AccountSQL().getListAcc(seach, page, pagesize);
            ViewBag.seaching = seach;
             return View(model);
             
        }
        public void Setlist()
        {

            ViewBag.Access = new List<SelectListItem>{
                       new SelectListItem { Value = "0" , Text = "Quản trị viên" },
                       new SelectListItem { Value = "1" , Text = "Phòng Nghiên Cứu Khoa Học" },
                        new SelectListItem { Value = "2" , Text = "Giảng Viên" },
                         new SelectListItem { Value = "3" , Text = "Sinh Viên" }
            };
            ViewBag.Status = new List<SelectListItem> {
                       new SelectListItem { Value = "0" , Text = "Hoat động" },
                       new SelectListItem { Value = "1" , Text = "Vô hiệu Hóa" }
            };

        }
        public ActionResult FormInsert()
        {
            List<Position> positions = qLNCKHDHTDTD.Positions.ToList();
            ViewBag.listPosition = new SelectList(positions, "IdPo", "NamePo");
            
            List<Faculty> faculties = qLNCKHDHTDTD.Faculties.ToList();
            ViewBag.listFaculty = new SelectList(faculties, "IdFa", "Name");
             
            Setlist();
            return View();
        }
         
        public ActionResult FormEdit(string Email)
        {
            Setlist();
            var model = new AccountSQL().DetailbyUser(Email);
            ViewBag.User = Email;
            
            return View(model);
        }
        [HttpPost]
        public ActionResult Create(Account model, int Position, string IdLe, string IdFa)
        {
            if (ModelState.IsValid)
            {
                var res = new AccountSQL();
                if (res.FindByUser(model.Email) == false)
                {
                    res.InsertAcc(model.Email, HashMD5.MD5Hash(model.PassWord), model.Access, Position, IdLe, IdFa);
                    ModelState.AddModelError("", "Thêm Tài Khoản Thành Công");
                }
                else
                    ModelState.AddModelError("", "Tài Khoản Đã Tồn Tại");

            }
            return RedirectToAction("FormInsert");
        }
        public ActionResult Update(Account model)
        {
            var update = new AccountSQL().UpdateAccount(model.Email, model.Access, model.Status);
            if (update == true)
                ModelState.AddModelError("", "Cập Nhật Thành Công");
            else ModelState.AddModelError("", "Cập Nhật Lỗi");
            return RedirectToAction("FormEdit");
        }
       
        public ActionResult Edit(string UserName)
        {
 
            var model = qLNCKHDHTDTD.Accounts.Where(x=>x.Email==UserName).SingleOrDefault();
            
            return View(model);

        }
        [HttpPost]
        public ActionResult EditAccount(Account account)
        {
 
            return View();

        }
         
        public JsonResult DeleteAccount(string UserName)
        {
            bool a = qLNCKHDHTDTD.Database.ExecuteSqlCommand("delete from Account where UserName ='" + UserName + "'") > 0;

            return Json(new
            {
                UserName = UserName,
                a = a
            }, JsonRequestBehavior.AllowGet);
        }
    }
}