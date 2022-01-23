using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using System.Web.UI.WebControls;
using DuAnQLNCKH.Models;


namespace DuAnQLNCKH.Controllers
{
    public class StatisticController : Controller
    {
        // GET: Statistic
        DHTDTTDNEntities1 dHTDTTDNEntities1 = new DHTDTTDNEntities1();
        List<Author> authors= new DHTDTTDNEntities1().Authors.ToList();
        List<TopicOfLecture> topicOfLectures = new DHTDTTDNEntities1().TopicOfLectures.ToList();
        List<Subject> subjects = new DHTDTTDNEntities1().Subjects.ToList();
        List<PointTable> pointTables = new DHTDTTDNEntities1().PointTables.ToList();
        List<Faculty> faculties = new DHTDTTDNEntities1().Faculties.ToList();
        List<Information> information = new DHTDTTDNEntities1().Information.ToList();
        List<TopicOfStudent> topicOfStudents = new DHTDTTDNEntities1().TopicOfStudents.ToList();
        List<ProgressLe> progressLes = new DHTDTTDNEntities1().ProgressLes.ToList();
        List<ProgressSt> progressSts = new DHTDTTDNEntities1().ProgressSts.ToList();
        //[Authorize(Roles = "1")]
        public ActionResult Index()
        {
            Session["Acess"] = "1";
            Session["UserName"] = "phamquangthao";
            viewbag();
           
            return View();
             
        }
        public ActionResult getStudentList(string IdSu)
        {
            
            dHTDTTDNEntities1.Configuration.ProxyCreationEnabled = false;
            if (IdSu=="")
            {
                List<TopicOfStudent> DetailList1 = dHTDTTDNEntities1.TopicOfStudents.ToList();
                return Json(DetailList1, JsonRequestBehavior.AllowGet);
            }
            List<TopicOfStudent> DetailList = dHTDTTDNEntities1.TopicOfStudents.Where(x => x.IdSu == IdSu).ToList();
            return Json(DetailList, JsonRequestBehavior.AllowGet);
        }
        public ActionResult getLectureList(string IdFa)
        {
            
            dHTDTTDNEntities1.Configuration.ProxyCreationEnabled = false;
            if (IdFa=="")
            {
                List<Information> DetailList1 = dHTDTTDNEntities1.Information.ToList();
                return Json(DetailList1, JsonRequestBehavior.AllowGet);
            }
            List<Information> DetailList = dHTDTTDNEntities1.Information.Where(x => x.IdKhoa == IdFa).ToList();
            return Json(DetailList, JsonRequestBehavior.AllowGet);
        }
        
        public void ExportExcel1(List<TopicLectureStatisticModel> list)
        {
            Session["listEx1"] = list;
        }
        public void ExportExcel2(List<TopicStudenStatisticModel> list1)
        {
            Session["listEx2"] = list1;
        }
           
        public void viewbag()
        {
             
                  var topicOfLecture = (from t in topicOfLectures
                                        join a in authors on t.IdTp equals a.IdTp
                                      join p in pointTables  on t.IdP equals p.IdP
                                       join i in information on a.Email equals i.Email
                                      join f in faculties on i.IdKhoa equals f.IdFa
                                      join pr in progressLes on t.IdTp equals pr.IdTp
                                      where t.Status==1 || t.Status==4
                                      select new TopicOfLectureView
                                      {
                                          
                                          pointTable=p,
                                          author=a,
                                          topicOfLecture = t,
                                          information = i,
                                          faculty = f,
                                          progressLe=pr
                                      }).ToList();
                ViewBag.listTopicOfLecture = topicOfLecture;
                var topicOfStudent1 = (from t in topicOfStudents                                       
                                       join p in pointTables on t.IdP equals p.IdP
                                        join s in subjects  on t.IdSu equals s.IdSu
                                       join pr in progressSts on t.IdTp equals pr.IdTp
                                       where t.Status == 1
                                       select new TopicOfStudentView
                                       {

                                           topicOfStudent = t,
                                           subject=s,
                                          
                                           pointTable = p,
                                           progressSt=pr

                                       }).ToList();
                ViewBag.listtopicOfStudents = topicOfStudent1;
                
                ViewBag.listNameStu = new SelectList(topicOfStudents, "IdSV", "NameSt");

            ViewBag.listtype = new SelectList(pointTables, "IdP", "NameP");
             ViewBag.listFacul = new SelectList(faculties, "IdFa", "Name");
             ViewBag.subjects1 = new SelectList(subjects, "IdSu", "NameCu");
             ViewBag.listNameLe = new SelectList(information, "IdLe", "NameLe");
            ViewBag.listProgress = new SelectList(new List<SelectListItem> {
                       new SelectListItem { Value = "1" , Text = "đang thực hiện" },
                       new SelectListItem { Value = "2" , Text = "quá hạn" },
                       new SelectListItem { Value = "3" , Text = "đã nghiệm thu" }
            }, "Value", "Text");
         }

        public ActionResult getTypeList(int IdTy)
        {
            dHTDTTDNEntities1.Configuration.ProxyCreationEnabled = false;
            List<PointTable> DetailList = dHTDTTDNEntities1.PointTables.Where(x => x.IdP == IdTy).ToList();
            return Json(DetailList, JsonRequestBehavior.AllowGet);
        }
        public ActionResult SearchLecture(string name)
        {
            List<TopicOfLecture> listTopicOfLecture = dHTDTTDNEntities1.TopicOfLectures.Where(x=>x.Name.Contains(name)).ToList();
            List<TopicOfStudent> listTopicOfStudent = dHTDTTDNEntities1.TopicOfStudents.ToList();
            ViewBag.listTopicOfStudent = listTopicOfStudent;
            ViewBag.listTopicOfLecture = listTopicOfLecture;
            return View("Index");
        }
     
        private SqlConnection con;
        public string IdP1;
        public void connection()
        {
            string constr = @"Data Source=DESKTOP-ECMGDNK\SQLEXPRESS;initial catalog=nckh_dhdn;integrated security=True";
            con = new SqlConnection(constr);

        }
        [HttpPost]
        public ActionResult ExportExcel()
        {
            var gv = new GridView();

            gv.DataSource = Session["listEx1"];
            gv.DataBind();
             
            gv.HeaderRow.Cells[0].Text = "Tên đề tài";
            gv.HeaderRow.Cells[1].Text = "Nhóm tác giả";
             
            gv.HeaderRow.Cells[2].Text = "Loại đề tài";           
            gv.HeaderRow.Cells[3].Text = "Ngày bắt đầu";
             
            gv.HeaderRow.Cells[4].Text = "Kết thúc";
            gv.HeaderRow.Cells[5].Text = "Kinh phí";
            gv.HeaderRow.Cells[6].Text = "Trạng thái";
            gv.HeaderRow.Cells[7].Text = "Điểm";
               
            Response.ClearContent();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment; filename=DemoExcel.xls");
            Response.ContentType = "application/ms-excel";
            Response.Charset = "";
            StringWriter objStringWriter = new StringWriter();
            HtmlTextWriter objHtmlTextWriter = new HtmlTextWriter(objStringWriter);
            gv.RenderControl(objHtmlTextWriter);
            Response.Output.Write(objStringWriter.ToString());
            Response.Flush();
            Response.End();
            viewbag();
            return View("Index");
        }
        [HttpPost]
        public ActionResult ExportExcelStu()
        {
            var gv = new GridView();

            gv.DataSource = Session["listEx2"];
            gv.DataBind();
          
            gv.HeaderRow.Cells[0].Text = "Tên đề tài";
            gv.HeaderRow.Cells[1].Text = "Tên sinh viên";
            gv.HeaderRow.Cells[2].Text = "Chuyên sâu";
            gv.HeaderRow.Cells[3].Text = "Loại đề tài";
            gv.HeaderRow.Cells[4].Text = "GV hướng dẫn";
            gv.HeaderRow.Cells[5].Text = "Ngày bắt đầu";
            gv.HeaderRow.Cells[6].Text = "Thời gian";
            gv.HeaderRow.Cells[7].Text = "Kết thúc";
            gv.HeaderRow.Cells[8].Text = "Kinh phí";
            gv.HeaderRow.Cells[9].Text = "Trạng thái";             
            gv.HeaderRow.Cells[10].Text = "Điểm";
            
            Response.ClearContent();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment; filename=DemoExcel.xls");
            Response.ContentType = "application/ms-excel";
            Response.Charset = "";
            StringWriter objStringWriter = new StringWriter();
            HtmlTextWriter objHtmlTextWriter = new HtmlTextWriter(objStringWriter);
            gv.RenderControl(objHtmlTextWriter);
            Response.Output.Write(objStringWriter.ToString());
            Response.Flush();
            Response.End();
            viewbag();
            return View("Index");
        }

        //[HttpPost]
        
        //public JsonResult ExportToExcel(string IdPa, DateTime dateEnd, DateTime dateSt)
        //{
        //    ViewBag.IdP = IdPa;
        //    IdP1 = IdPa;
        //    viewbag();
        //    return Json("Index");
        //}
        public string idp()
        {

            return ViewBag.IdP;
        }
    }
}