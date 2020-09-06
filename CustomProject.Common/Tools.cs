using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Sockets;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CustomProject.Common
{
    public static class Tools
    {

		public static Result<bool> Exec(this SqlCommand cmd)
        {
            try
            {
                if (cmd.Connection.State!=ConnectionState.Open)
                {
					cmd.Connection.Open();
                }
				int res = cmd.ExecuteNonQuery();

				return new Result<bool>
				{
					IsSucced = true,
					Message = "it is okay",
					Data = res > 0
				};
            }
            catch (Exception ex)
            {
				string na = ex.Message;
				return new Result<bool> { 
				IsSucced =false,
				Message = ex.Message
				
				};
            }
            finally
            {
                if (cmd.Connection.State!=ConnectionState.Closed)
                {
					cmd.Connection.Close();
                }
            }
        }



		private static SqlConnection connection;

		public static SqlConnection Connection
		{
			get {
				if (connection == null)
				{
					connection = new SqlConnection(ConfigurationManager.ConnectionStrings["MyBlog"].ConnectionString);
				}
					return connection;
			    }
			set { connection = value; }
		}

		public static Result<List<T>> Changer<T>(this SqlDataAdapter source) where T:new()
		{
            try
            {
				DataTable dt = new DataTable();
				source.Fill(dt);
				Type tip = typeof(T);
				List<T> lisd = new List<T>();
				PropertyInfo[] props = tip.GetProperties();

				foreach (DataRow item in dt.Rows)
				{
					T a = new T();
					foreach (PropertyInfo pi in props)
					{

						object valu = item[pi.Name];

                        if (valu != System.DBNull.Value && valu != null)
                        {
                            pi.SetValue(a, valu);
                        }
                    }
					lisd.Add(a);
				}

				return new Result<List<T>> {
					IsSucced = true,
					Message = "it is okay",
					Data = lisd
				};
			}
            catch (Exception ex)
            {

                return new Result<List<T>> { 
				IsSucced = false,
				Message = ex.Message
				};
            }
			
		}



        private static SqlCommand cmd;

        public static SqlCommand CMD
        {
            get {
                if (cmd==null)
                {
					cmd = new SqlCommand();
                }
				return cmd;
			}
        }

    }
}
