using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace DuAnQLNCKH.Models
{
    public class TypeModel
    {
        private SqlConnection con;
        public void connection()
        {
            string constr = @"Data Source=DESKTOP-ECMGDNK\SQLEXPRESS;initial catalog=nckh_dhdn;integrated security=True";
            con = new SqlConnection(constr);

        }
        public string IdTy()
        {
            connection();
            con.Open();
            string sql = string.Format("declare cur_IdTy cursor for select count(IdTy) from Type open cur_IdTy declare @count int fetch next from cur_IdTy into @count if @count=0 begin insert into Type(IdTy) values ('1') select IdTy='T1' from Type delete from Type where IdTy=1 ;end; else begin select IdTy='T'+CAST(@count+1 as varchar(10)) from Type ;fetch next from cur_IdTy into @count ;end; close cur_IdTy deallocate cur_IdTy");
            SqlCommand a = new SqlCommand(sql, con);
            String a1 = (String)a.ExecuteScalar();
            con.Close();
            return a1;
        }
        public bool AddTypes(Type type, string idty)
        {

            connection();
            
            SqlCommand com = new SqlCommand("AddType", con);
            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.AddWithValue("@IdTy", idty);
            com.Parameters.AddWithValue("@Name", type.Name);         
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
        public bool AddPoint(FilePointTable point)
        {
            
            connection();
            SqlCommand com = new SqlCommand("AddPoint", con);
            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.AddWithValue("@IdTy", point.IdTy);
            com.Parameters.AddWithValue("@NameP", point.NameP);
            com.Parameters.AddWithValue("@File", point.File1);
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