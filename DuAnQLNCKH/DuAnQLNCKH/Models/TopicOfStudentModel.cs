using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace DuAnQLNCKH.Models
{
    public class TopicOfStudentModel
    {

        //string cs = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
        private SqlConnection con;
        private DHTDTTDNEntities1 qLNCKHDHTDTD = null;
        public TopicOfStudentModel()
        {
            qLNCKHDHTDTD = new DHTDTTDNEntities1();
        }
        public void connection()
        {
            string constr = @"Data Source=DESKTOP-ECMGDNK\SQLEXPRESS;initial catalog=nckh_dhdn;integrated security=True";
            con = new SqlConnection(constr);

        }
        public string IdTp()
        {
            connection();
            con.Open();
            string sql = string.Format("declare cur_IdTpSV cursor for select count(IdTp) from TopicOfStudent open cur_IdTpSV declare @count int fetch next from cur_IdTpSV into @count if @count=0 begin set dateformat dmy insert into TopicOfStudent(IdTp, DateSt, CountAuthor, Times) values ('1', '20/11/2021', '1', 2) select IdTp='DTSV01' from TopicOfStudent delete from TopicOfStudent where IdTp=1 ;end; else begin select distinct IdTp='DTSV0'+CAST(@count+1 as varchar(10)) from TopicOfStudent ;fetch next from cur_IdTpSV into @count ;end; close cur_IdTpSV deallocate cur_IdTpSV");
            SqlCommand a = new SqlCommand(sql, con);
            String a1 = (String)a.ExecuteScalar();
            con.Close();
            return a1;
        }
        public List<TopicOfStudent> listchuaduyet()
        {
            var list = qLNCKHDHTDTD.Database.SqlQuery<TopicOfStudent>("select * from TopicOfStudent where Status like N'chưa duyệt'").ToList();
            return list;
        }
        public List<TopicOfStudent> listAll()
        {
            var list = qLNCKHDHTDTD.Database.SqlQuery<TopicOfStudent>("select * from TopicOfStudent where Status=N'đã duyệt'").ToList();
            return list;
        }
         
        public TopicOfStudent ViewDetail(string IdTp)
        {
            return qLNCKHDHTDTD.TopicOfStudents.Find(IdTp);
        }
        public bool xoa(string IdTp)
        {
            try
            {
                var topic = qLNCKHDHTDTD.TopicOfStudents.Find(IdTp);
                qLNCKHDHTDTD.TopicOfStudents.Remove(topic);

                qLNCKHDHTDTD.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }

         
        }
        public bool xetduyet1(string IdTp)
        {
            try
            {


                //detai.TrangThai = dtgv1.TrangThai;                 
                qLNCKHDHTDTD.Database.ExecuteSqlCommand("update TopicOfStudents set Status=N'đã duyệt' where IdTp=@IdTp",
                     new SqlParameter("@IdTp", IdTp)
                    );

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool AddTopicStudent(TopicOfStudent topicOfStudent, string IdTp)
        {

            connection();
            SqlCommand com = new SqlCommand("AddNewTopicOfStudent", con);
            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.AddWithValue("@IdTp", IdTp);
            com.Parameters.AddWithValue("@Name", topicOfStudent.Name);
            com.Parameters.AddWithValue("@NameSt", topicOfStudent.NameSt);
            com.Parameters.AddWithValue("@IdSV", topicOfStudent.IdSV);
            com.Parameters.AddWithValue("@Emmail", topicOfStudent.Email);
            com.Parameters.AddWithValue("@IdP", topicOfStudent.IdP);
            com.Parameters.AddWithValue("@DateSt", topicOfStudent.DateSt);
            com.Parameters.AddWithValue("@Times", topicOfStudent.Times);
            com.Parameters.AddWithValue("@Expense", topicOfStudent.Expense);
            com.Parameters.AddWithValue("@Status", 0);
            com.Parameters.AddWithValue("@CountAuthor", topicOfStudent.CountAuthor);
            com.Parameters.AddWithValue("@IdFa", topicOfStudent.IdSu);
            com.Parameters.AddWithValue("@FileDemo", topicOfStudent.FileDemo);
            com.Parameters.AddWithValue("@LectureIntr", topicOfStudent.LectureIntruc);
            con.Open();
            int i = com.ExecuteNonQuery();
            con.Close();
            if (i >= 1)
            {

                return true;

            }
            else
            {

                return false;
            }
        }
    }
}