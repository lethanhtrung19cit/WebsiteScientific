using DuAnQLNCKH.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
 
namespace DuAnQLNCKH.Controllers
{
    public class TopicOfLectureController : Controller
    {
        // GET: DeTaiGV
        DHTDTTDNEntities1 qLNCKHDHTDTD = new DHTDTTDNEntities1();
        TopicOfLectureModel dtgv = new TopicOfLectureModel();
        List<TopicOfStudent> studentList = new List<TopicOfStudent>();
        List<Subject> subjects = new DHTDTTDNEntities1().Subjects.ToList();
        List<Extension> extensions = new DHTDTTDNEntities1().Extensions.ToList();
        List<TopicOfLecture> topicOfLectures = new DHTDTTDNEntities1().TopicOfLectures.ToList();
        List<TopicOfStudent> topicOfStudents = new DHTDTTDNEntities1().TopicOfStudents.ToList();
        List<Information> information = new DHTDTTDNEntities1().Information.ToList();
        List<PointTable> pointTables = new DHTDTTDNEntities1().PointTables.ToList();
        List<Faculty> faculties = new DHTDTTDNEntities1().Faculties.ToList();
        List<ProgressLe> progressLes = new DHTDTTDNEntities1().ProgressLes.ToList();
        List<ProgressSt> progressSts = new DHTDTTDNEntities1().ProgressSts.ToList();
        List<Author> authors = new DHTDTTDNEntities1().Authors.ToList();
         
        public ActionResult getTypeList(int IdTy)
        {
            qLNCKHDHTDTD.Configuration.ProxyCreationEnabled = false;
            List<PointTable> DetailList = qLNCKHDHTDTD.PointTables.Where(x => x.IdP == IdTy).ToList();
            return Json(DetailList, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Index()
        {
                
                var topicofLecture = (from t in topicOfLectures
                                      join a in authors on t.IdTp equals a.IdTp
                                      join i in information on a.Email equals i.Email
                                      join f in faculties on i.IdKhoa equals f.IdFa
                                      join p in pointTables on t.IdP equals p.IdP
                                      join e in extensions on t.IdTp equals e.IdTp
                                      join pr in progressLes on t.IdTp equals pr.IdTp
                                      select new TopicOfLectureView
                                      {
                                          author=a,
                                          pointTable = p,
                                          topicOfLecture = t,
                                          information = i,
                                          extension = e,
                                          faculty=f,
                                          progressLe=pr
                                      }).ToList();
                ViewBag.TopicOfLecture = topicofLecture;
                var topics = (from t in topicOfStudents
                              join p in pointTables on t.IdP equals p.IdP
                              join s in subjects on t.IdSu equals s.IdSu
                              join pr in progressSts on t.IdTp equals pr.IdTp
                              where t.Status == 1
                              select new TopicOfStudentView
                              {

                                  topicOfStudent = t,
                                  subject=s,
                                  pointTable = p,
                                  progressSt=pr
                              }).ToList();
                ViewBag.listTopicOfStudent = topics;
            
          
            return View();

        }
        public ActionResult listAcceptanced()
        {                
            var topicofLecture = (from t in topicOfLectures
                                    join a in authors on t.IdTp equals a.IdTp
                                    join i in information on a.Email equals i.Email
                                    join p in pointTables on t.IdP equals p.IdP
                                    where t.Status==4 
                                      select new TopicOfLectureView
                                    {
                                        author=a,
                                        pointTable = p,
                                        topicOfLecture = t,
                                        information = i,
                                         
                                         
                                    }).ToList();
            ViewBag.listAcceptancedLec = topicofLecture;
            var topics = (from t in topicOfStudents
                            join p in pointTables on t.IdP equals p.IdP
                            join s in subjects on t.IdSu equals s.IdSu
                            join pr in progressSts on t.IdTp equals pr.IdTp
                            where t.Status == 4
                            select new TopicOfStudentView
                            {

                                topicOfStudent = t,
                                subject=s,
                                pointTable = p,
                                progressSt=pr
                            }).ToList();
            ViewBag.listAcceptancedStu = topics;
            
          
            return View();

        }
        public void dataMyTopic()
        {

            var topicofLecture = (from t in topicOfLectures
                                  join a in authors on t.IdTp equals a.IdTp
                                  join i in information on a.Email equals i.Email
                                  join p in pointTables on t.IdP equals p.IdP
                                  where i.Email == Session["UserName"].ToString() && t.Status == 0
                                  select new TopicOfLectureView
                                  {
                                      pointTable = p,
                                      topicOfLecture = t,
                                      information = i,
                                      author = a
                                  }).ToList();
            ViewBag.topicExtension = topicofLecture;
            var topic = (from t in topicOfLectures
                         join a in authors on t.IdTp equals a.IdTp
                         join i in information on a.Email equals i.Email
                         join p in pointTables on t.IdP equals p.IdP
                         join e in extensions on t.IdTp equals e.IdTp
                         join pr in progressLes on t.IdTp equals pr.IdTp
                         where i.Email == Session["UserName"].ToString() && t.Status == 1 && new TopicOfLectureModel().dateLec(t.IdTp) == pr.Date
                         select new TopicOfLectureView
                         {
                             author = a,
                             pointTable = p,
                             topicOfLecture = t,
                             extension = e,
                             information = i,
                             progressLe = pr
                         }).ToList();
            ViewBag.topicProgress = topic;
            var topic2 = (from t in topicOfLectures
                          join a in authors on t.IdTp equals a.IdTp
                          join i in information on a.Email equals i.Email
                          join p in pointTables on t.IdP equals p.IdP
                          join e in extensions on t.IdTp equals e.IdTp
                          join pr in progressLes on t.IdTp equals pr.IdTp
                          where i.Email == Session["UserName"].ToString() && t.Status == 3 && new TopicOfLectureModel().dateLec(t.IdTp) == pr.Date
                          select new TopicOfLectureView
                          {
                              author = a,
                              pointTable = p,
                              topicOfLecture = t,
                              extension = e,
                              information = i,
                              progressLe = pr
                          }).ToList();
            ViewBag.topicExEnd = topic2;
            var topic1 = (from t in topicOfLectures
                          join a in authors on t.IdTp equals a.IdTp
                          join i in information on a.Email equals i.Email
                          join p in pointTables on t.IdP equals p.IdP
                          where i.Email == Session["UserName"].ToString() && t.Status == 4
                          select new TopicOfLectureView
                          {
                              pointTable = p,
                              topicOfLecture = t,
                              information = i
                          }).ToList();
            ViewBag.topicEnd = topic1;
        }
        public ActionResult myTopicLecture()
        {
            dataMyTopic();
            return View();
             
        }
        //To Handle connection related activities
        [HttpPost]
        public void ShowIdP(string id)
        {
            ViewBag.idp = id;
            
        }
        [HttpPost]
        public ActionResult CreateTopicOfLecture(HttpPostedFileBase FileDemo1, TopicOfLecture topicOfLecture, string[] email, string[] nameAu, string typeRegister)
        {
            string IdTp = dtgv.IdTp();
            
             if (ModelState.IsValid)
             {
                 string username = Session["UserName"].ToString();
                TopicOfLectureModel topic = new TopicOfLectureModel();
                string idle = dtgv.IdLe(username);
                if (int.Parse(typeRegister) == 1)
                {
                    topicOfLecture.Status = 0;
                }
                else
                {
                    topicOfLecture.Status = 3;
                }    
                topicOfLecture.IdTp = IdTp;
                     
                var Extension = Path.GetExtension(FileDemo1.FileName);
                var fileName = "fileTopic-" + DateTime.Now.ToString("yyyyMMddHHmmssfff") + Extension;
                string path = Path.Combine(Server.MapPath("~/File/FileTopic"), fileName);
                topicOfLecture.FileDemo = Url.Content(Path.Combine("~/File/FileTopic/", fileName));
                        
                topic.AddTopicLecture(topicOfLecture);

                ViewBag.Message = "Đăng ký đề tài thành công";
                FileDemo1.SaveAs(path);
                ViewBag.listtype = new SelectList(pointTables, "IdP", "NameP");

                     
                
                //else
                //{

                //    ProgressLe progressLe = new ProgressLe();
                //    progressLe.IdTp = IdTp;
                //    progressLe.Progress = 3;
                //    qLNCKHDHTDTD.ProgressLes.Add(progressLe);
                //    qLNCKHDHTDTD.SaveChanges();
                //}
                
                Author author = new Author();
                author.Email = Session["UserName"].ToString();
                author.IdTp = IdTp;
                author.NameAu = qLNCKHDHTDTD.Information.Where(x => x.Email == author.Email).Select(x => x.NameLe).FirstOrDefault();
                author.Grade = 0;
                qLNCKHDHTDTD.Authors.Add(author);
                qLNCKHDHTDTD.SaveChanges();
                for (int i = 0; i < email.Length; i++)
                {
                    author.Email = email[i];
                    author.NameAu = nameAu[i];
                    author.IdTp = IdTp;
                    author.Grade = 1;
                    qLNCKHDHTDTD.Authors.Add(author);
                    qLNCKHDHTDTD.SaveChanges();

                }
                qLNCKHDHTDTD.SaveChanges();
                return View("ViewCreateTopicOfLecture");

             }

            
             
             ViewBag.listtype = new SelectList(pointTables, "IdP", "NameP");
            return View("ViewCreateTopicOfLecture", topicOfLecture);
           
        }
       
        public ActionResult ViewCreateTopicOfLecture()
        {
            string email = Session["UserName"].ToString();
            ViewBag.NameLe = qLNCKHDHTDTD.Information.Where(x => x.Email == email).Select(x=>x.NameLe).FirstOrDefault();
            ViewBag.Message = "";
             ViewBag.listtype = new SelectList(pointTables, "IdP", "NameP");

            return View();
        }
        
        public ActionResult chuaduyet()
        {
             
                var topicofLecture = (from t in topicOfLectures
                                      join a in authors on t.IdTp equals a.IdTp
                                      join i in information on a.Email equals i.Email
                                       join f in faculties on i.IdKhoa equals f.IdFa
                                      join p in pointTables on t.IdP equals p.IdP
                                       where t.Status == 0
                                      select new TopicOfLectureView
                                      {
                                          pointTable = p,
                                          topicOfLecture = t,
                                          information = i,
                                          faculty=f,
                                          author=a
                                      }).ToList();
                ViewBag.TopicOfLecture = topicofLecture;
                var topicofStudent = (from t in topicOfStudents
                                     join s in subjects on t.IdSu equals s.IdSu
                                      join p in pointTables on t.IdP equals p.IdP
                                      where t.Status == 0
                                      select new TopicOfLectureView
                                      {
                                          pointTable = p,
                                          topicOfStudent = t,
                                          subject=s
                                      }).ToList();
                ViewBag.TopicOfStudent = topicofStudent;
           
           
            
            return View();
         
        }
         
        public ActionResult detailTopicLecture(string IdTp)
        {
            var listDetail = (from t in topicOfLectures
                              join a in authors on t.IdTp equals a.IdTp
                              join i in information on a.Email equals i.Email
                              join p in pointTables on t.IdP equals p.IdP
                              join f in faculties on i.IdKhoa equals f.IdFa
                              where t.IdTp==IdTp
                              select new TopicOfLectureView
                              {
                                  topicOfLecture = t,
                                  information = i,
                                  faculty = f,
                                  pointTable = p,
                                  author=a
                              }
                            ).ToList();
            ViewBag.listDetail = listDetail;
            return View();
        }
        public void rejectTopic(string IdTp, string Reason)
        {
            var email = (from t in topicOfLectures
                         join a in authors on t.IdTp equals a.IdTp
                         join i in information on a.Email equals i.Email
                         where t.IdTp == IdTp
                            select new
                            {
                                i.Email
                            }).FirstOrDefault().Email;
            TopicOfLecture topicOfLecture = new TopicOfLecture();
            var topic = qLNCKHDHTDTD.TopicOfLectures.Find(IdTp);
            topic.Status = 2;            
            qLNCKHDHTDTD.Entry(topic).State = EntityState.Modified;
            qLNCKHDHTDTD.SaveChanges();
             
            var nameTopic = qLNCKHDHTDTD.TopicOfLectures.Where(x => x.IdTp == IdTp).Select(x => x.Name).FirstOrDefault();
            string from1 = qLNCKHDHTDTD.Emails.Select(x => x.NameEmail).FirstOrDefault();
            string pass = qLNCKHDHTDTD.Emails.Where(x => x.NameEmail == from1).Select(x => x.PassWord).FirstOrDefault();

            using (MailMessage mail = new MailMessage())
            {
                // Define your senders
                mail.From = new MailAddress(from1);
                mail.Body = "Thông báo về đề tài " + nameTopic + " không được duyệt, Lí do : "+Reason;
                mail.Subject = "Thông báo đề tài không được duyệt";

                mail.To.Add(email.ToString());

                mail.IsBodyHtml = true;
                SmtpClient smtp = new SmtpClient();
                smtp.Host = "smtp.gmail.com";
                smtp.EnableSsl = true;
                NetworkCredential networkCredential = new NetworkCredential(from1, pass);
                smtp.UseDefaultCredentials = true;
                smtp.Credentials = networkCredential;
                smtp.Port = 587;
                smtp.Send(mail);
            }
        }
        public ActionResult detailTopicSt(string IdTp)
        {
            var listDetail = (from t in topicOfStudents
                              join p in pointTables on t.IdP equals p.IdP
                              join s in subjects on t.IdSu equals s.IdSu
                              where t.IdTp==IdTp
                              select new TopicOfLectureView
                              {
                                  topicOfStudent = t,                                  
                                  subject = s,
                                  pointTable = p
                              }
                            ).ToList();
            ViewBag.listDetail = listDetail;
            return View();
        }
        public void rejectTopicSt(string IdTp, string Reason)
        {
            var email = (from t in topicOfStudents
                            
                            where t.IdTp == IdTp
                            select new
                            {
                                t.Email
                            }).FirstOrDefault().Email;
            TopicOfStudent topicOfStudent = new TopicOfStudent();
            var topic = qLNCKHDHTDTD.TopicOfStudents.Find(IdTp);
            topic.Status = 2;            
            qLNCKHDHTDTD.Entry(topic).State = EntityState.Modified;
            qLNCKHDHTDTD.SaveChanges();
             
            var nameTopic = qLNCKHDHTDTD.TopicOfStudents.Where(x => x.IdTp == IdTp).Select(x => x.Name).FirstOrDefault();
            string from1 = qLNCKHDHTDTD.Emails.Select(x => x.NameEmail).FirstOrDefault();
            string pass = qLNCKHDHTDTD.Emails.Where(x => x.NameEmail == from1).Select(x => x.PassWord).FirstOrDefault();

            using (MailMessage mail = new MailMessage())
            {
                // Define your senders
                mail.From = new MailAddress(from1);
                mail.Body = "Thông báo về đề tài " + nameTopic + " không được duyệt, Lí do : "+Reason;
                mail.Subject = "Thông báo đề tài không được duyệt";

                mail.To.Add(email.ToString());

                mail.IsBodyHtml = true;
                SmtpClient smtp = new SmtpClient();
                smtp.Host = "smtp.gmail.com";
                smtp.EnableSsl = true;
                NetworkCredential networkCredential = new NetworkCredential(from1, pass);
                smtp.UseDefaultCredentials = true;
                smtp.Credentials = networkCredential;
                smtp.Port = 587;
                smtp.Send(mail);
            }
        }
        public void extension(int IdEx, string NameTo, string Email)
        {

            using (DHTDTTDNEntities1 entities = new DHTDTTDNEntities1())
            {
                entities.Database.ExecuteSqlCommand("update Extension set Status = N'Đã duyệt' where IdEx=" + IdEx);
                entities.SaveChanges();
                string from1 = qLNCKHDHTDTD.Emails.Select(x => x.NameEmail).FirstOrDefault();
                string pass = qLNCKHDHTDTD.Emails.Where(x => x.NameEmail == from1).Select(x => x.PassWord).FirstOrDefault();
                // By using a Message with no constructors, you can define your To recipients below
                using (MailMessage mail = new MailMessage())
                {
                    // Define your senders
                    mail.From = new MailAddress(from1);
                    mail.Body = "Thông báo đề tài " + NameTo + " đã được gia hạn";
                    mail.Subject = "Thông báo gia hạn đề tài";

                    mail.To.Add(Email);

                    mail.IsBodyHtml = true;
                    SmtpClient smtp = new SmtpClient();
                    smtp.Host = "smtp.gmail.com";
                    smtp.EnableSsl = true;
                    NetworkCredential networkCredential = new NetworkCredential(from1, pass);
                    smtp.UseDefaultCredentials = true;
                    smtp.Credentials = networkCredential;
                    smtp.Port = 587;
                    smtp.Send(mail);
                }

            }

        }
        public ActionResult registerExtension(string IdTp, DateTime Times, string Reason)
        {
            
             
                qLNCKHDHTDTD.Database.ExecuteSqlCommand("set dateformat dmy update Extension set Times='" + Times + "', Reason=N'" + Reason + "', Status=N'chưa duyệt' where IdTp='"+IdTp+"'");
                 

                var topicextension = (from t in topicOfLectures
                                      join a in authors on t.IdTp equals a.IdTp
                                      join i in information on a.Email equals i.Email
                                      join e in extensions on t.IdTp equals e.IdTp
                                      where i.Email == Session["UserName"].ToString()

                                      select new TopicOfLectureView
                                      {
                                          extension = e,
                                          topicOfLecture = t,
                                          information = i
                                      }).ToList();
                ViewBag.topicextension = topicextension;

            
            return RedirectToAction("viewRegisterExtension");
        }
        public ActionResult viewRegisterExtension()
        {     
             
                var topicextension = (from t in topicOfLectures
                                      join a in authors on t.IdTp equals a.IdTp
                                      join i in information on a.Email equals i.Email
                                       
                                     join e in extensions on t.IdTp equals e.IdTp
                                     where new TopicOfLectureModel().dateLecEx(t.IdTp) == e.Times
                                      select new TopicOfLectureView
                                     {
                                         extension=e,
                                         topicOfLecture = t,
                                         information=i,
                                        
                                     }).ToList();
                ViewBag.topicextension = topicextension;
                return View();
             
        }
        public ActionResult listextension()
        {
              
                var topicextension =(from t in topicOfLectures 
                                     join a in authors on t.IdTp equals a.IdTp
                                     join e in extensions on t.IdTp equals e.IdTp
                                     join i in information on a.Email equals i.Email
                                     join f in faculties on i.IdKhoa equals f.IdFa
                                     join p in pointTables on t.IdP equals p.IdP
                                     join pr in progressLes on t.IdTp equals pr.IdTp
                                     where e.Status == "chưa duyệt"
                                     select new TopicOfLectureView
                                     {
                                         extension = e,
                                         topicOfLecture = t,
                                         information=i,
                                         pointTable=p,
                                         faculty=f,
                                         progressLe=pr
                                         
                                     }).ToList();
                ViewBag.listextension = topicextension;
                return View();
              
        }
        
        public ActionResult chuaduyetsv()
        {            
            
             var model = dtgv.listchuaduyetsv();
            return View(model);
        }
        [HttpPost]
        [Route("/TopicOfLecture/extension")]
        
        public ActionResult updateExtension(string IdTp, string DateEx, string Reason)
        {
            using (DHTDTTDNEntities1 entities = new DHTDTTDNEntities1())
            {
                List<TopicOfLecture> topicOfLectures = entities.TopicOfLectures.ToList();
                List<Extension> extensions = entities.Extensions.ToList();
                List<Information> information = entities.Information.ToList();

                var topicextension = (from t in topicOfLectures
                                      join a in authors on t.IdTp equals a.IdTp
                                      join i in information on a.Email equals i.Email
                                      join e in extensions on t.IdTp equals e.IdTp
                                      where i.Email == Session["UserName"].ToString()

                                      select new TopicOfLectureView
                                      {
                                          extension = e,
                                          topicOfLecture = t,
                                          information = i
                                      }).ToList();
                ViewBag.topicextension = topicextension;
                DateTime date = Convert.ToDateTime(DateEx);
                entities.Database.ExecuteSqlCommand("set dateformat dmy  update Extension set Times = '"+date+"', Reason=N'"+Reason+"' where IdTp='" + IdTp+"'");
                entities.SaveChanges();
            }
            return RedirectToAction("viewRegisterExtension");
        }
        [HttpPost]       
        public void xetduyet2(string IdTp, string Times, string NameTo, string Email)
        {

            using (DHTDTTDNEntities1 entities = new DHTDTTDNEntities1())
            {
                string a = IdTp;
                DateTime Time = Convert.ToDateTime(Times);
                entities.Database.ExecuteSqlCommand("set dateformat dmy update TopicOfLecture set Status=1 where IdTp='" + IdTp +"' set dateformat dmy insert into ProgressLe values('"+IdTp+"', '"+DateTime.Now.ToString("d")+"', 1)"+ " insert into Extension(IdTp, Times, Status) values('" + IdTp + "', '" + Time + "', 'NULL')");
                entities.SaveChanges();
                string from1 = qLNCKHDHTDTD.Emails.Select(x => x.NameEmail).FirstOrDefault();
                string pass = qLNCKHDHTDTD.Emails.Where(x => x.NameEmail == from1).Select(x => x.PassWord).FirstOrDefault();

                string s = Session["UserName"].ToString();

                // By using a Message with no constructors, you can define your To recipients below
                using (MailMessage mail = new MailMessage())
                {
                    // Define your senders
                    mail.From = new MailAddress(from1);
                    mail.Body = "Thông báo đề tài " + NameTo + " đã được xét duyệt";
                    mail.Subject = "Thông báo xét duyệt đề tài";
                    
                   mail.To.Add(Email);
                     
                    mail.IsBodyHtml = true;
                    SmtpClient smtp = new SmtpClient();
                    smtp.Host = "smtp.gmail.com";
                    smtp.EnableSsl = true;
                    NetworkCredential networkCredential = new NetworkCredential(from1, pass);
                    smtp.UseDefaultCredentials = true;
                    smtp.Credentials = networkCredential;
                    smtp.Port = 587;
                    smtp.Send(mail);
                }
            }
        }
        [HttpPost]
        
        public void xetduyetsv(TopicOfStudent topicOfStudent)
        {
             
                TopicOfStudent topic = (from c in qLNCKHDHTDTD.TopicOfStudents
                                        where c.IdTp == topicOfStudent.IdTp
                                        select c).FirstOrDefault();
                qLNCKHDHTDTD.Database.ExecuteSqlCommand("update TopicOfStudent set Status=N'đã duyệt' where IdTp='" + topic.IdTp + "'");
                qLNCKHDHTDTD.SaveChanges();
             
        }
        public void editExpense(string IdTp, float expense)
        {
            var topic = qLNCKHDHTDTD.TopicOfLectures.Find(IdTp);
            topic.Expense = expense;
            qLNCKHDHTDTD.Entry(topic).State = EntityState.Modified;
            qLNCKHDHTDTD.SaveChanges();
        }
        public ActionResult Register1(string IdTp)
        {
            var topic = qLNCKHDHTDTD.TopicOfLectures.Find(IdTp);
            topic.Status = 3;
            qLNCKHDHTDTD.Entry(topic).State = EntityState.Modified;
            qLNCKHDHTDTD.SaveChanges();
            dataMyTopic();
            return RedirectToAction("myTopicLecture");
        }
        public ActionResult DownloadFile(string filePath)
        {
            string fullName = Server.MapPath("~" + filePath);

            byte[] fileBytes = GetFile(fullName);
            return File(
                fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, filePath);
        }

        byte[] GetFile(string s)
        {
            System.IO.FileStream fs = System.IO.File.OpenRead(s);
            byte[] data = new byte[fs.Length];
            int br = fs.Read(data, 0, data.Length);
            if (br != fs.Length)
                throw new System.IO.IOException(s);
            return data;
        }
    }
}