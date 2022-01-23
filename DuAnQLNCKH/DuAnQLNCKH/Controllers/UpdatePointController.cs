using DuAnQLNCKH.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DuAnQLNCKH.Controllers
{
    public class UpdatePointController : Controller
    {
        // GET: UpdatePoint
        DHTDTTDNEntities1 qLNCKHDHTDTD = new DHTDTTDNEntities1();
       // [Authorize(Roles = "1")]
        public ActionResult Index()
        {
            viewbag();
            return View();
        }
        public void viewbag()
        {
             List<PointTable> pointTables = qLNCKHDHTDTD.PointTables.ToList();
            var listPoi = (from  p in pointTables  
                           select new TopicOfLectureView
                           {
                                
                               pointTable = p
                           }).OrderBy(x=>x.pointTable.IdP).ToList();
            ViewBag.listPoint = listPoi;
            //ViewBag.listType = qLNCKHDHTDTD.Types.ToList();
            //List<Models.Type> typelist = qLNCKHDHTDTD.Types.ToList();
            //ViewBag.listtypes = new SelectList(typelist, "IdTy", "Name");
        }
        public ActionResult Update(PointTable point)
        {
            var detail = qLNCKHDHTDTD.PointTables.Find(point.IdP);
            detail.NameP = point.NameP;
            detail.MaxTime = point.MaxTime;
            detail.Value = point.Value;
            qLNCKHDHTDTD.Entry(detail).State=System.Data.Entity.EntityState.Modified;
            qLNCKHDHTDTD.SaveChanges();
             viewbag();
            return View("Index"); 
        }
         
         
        [HttpPost]
        public ActionResult CreatePoint(string NameP, int MaxTime, float Value)
        {
            PointTable point = new PointTable();
            point.NameP = NameP;
            point.MaxTime = MaxTime;
            point.Value = Value;
            qLNCKHDHTDTD.PointTables.Add(point);            
            qLNCKHDHTDTD.SaveChanges();
            viewbag();

            return RedirectToAction("Index", "UpdatePoint");


        }
    }
}