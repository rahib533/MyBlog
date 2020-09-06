using CustomProject.Common;
using CustomProject.Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomProject.ORM
{
    public class UserORM : ORMBase<User,UserORM>
    {
        public string GetUserById(Guid id)
        {
            string querry = $"select UserName from aspnet_Users where UserId = '{id}' ";

            SqlDataAdapter adp = new SqlDataAdapter(querry, Tools.Connection);
            DataTable dt = new DataTable();
            adp.Fill(dt);
            string str = "";
            foreach (DataRow item in dt.Rows)
            {
                str = item["UserName"].ToString();
            }
            return str;
        }

        public Guid GetUserByName(string name)
        {
            string querry = $"select UserId from aspnet_Users where UserName = '{name}' ";

            SqlDataAdapter adp = new SqlDataAdapter(querry, Tools.Connection);
            DataTable dt = new DataTable();
            adp.Fill(dt);
            Guid gid = new Guid();
            foreach (DataRow item in dt.Rows)
            {
                gid = (Guid)item["UserId"];
            }
            return gid;
        }
    }
}
