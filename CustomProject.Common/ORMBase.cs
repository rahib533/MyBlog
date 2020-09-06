using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CustomProject.Common
{
    public class ORMBase<TT,OT> : IORM<TT> where TT : class, new()
        where OT:class,new()
    {

        public Table tableofTT { 
            get {
                var atta = typeofTT.GetCustomAttributes(typeof(Table),false);
                Table ttt = (Table)atta[0];
                return ttt;
                }
        }

        public Type typeofTT { get {

                return typeof(TT);

            } }


        private static OT _current;

        public static OT Current
        {
            get {
                    if (_current==null)
                       {
                          _current = new OT();
                       }
                     return _current;
                }
        }


        public Result<bool> Delete(TT entity)
        {
            string querry = $"Delete {tableofTT.TableName} where {tableofTT.IdentityColumn} = ";
            PropertyInfo[] props = typeofTT.GetProperties();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = Tools.Connection;
            //Tools.CMD.Connection = Tools.Connection;
            string data = "";

            foreach (PropertyInfo item in props)
            {
                if (item.Name== tableofTT.IdentityColumn)
                {
                    data = item.GetValue(entity).ToString();

                    querry += "@" + item.Name;

                    //Tools.CMD.Parameters.AddWithValue($"@{item.Name}", data);
                    cmd.Parameters.AddWithValue($"@{item.Name}", data);
                    break;
                }
            }
            
            cmd.CommandText = querry;
            //Tools.CMD.CommandText = querry;

            return cmd.Exec();
            //return Tools.CMD.Exec();
        }

        public Result<bool> Insert(TT entity)
        {
            string querry = "insert into ";
            querry += string.Format("{0}(", tableofTT.TableName);
            string values = " values(";
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = Tools.Connection;
            //Tools.CMD.Connection = Tools.Connection;
            // --------------------------------------------------

            PropertyInfo[] props = typeofTT.GetProperties();
            foreach (PropertyInfo item in props)
            {
                if (item.Name == tableofTT.IdentityColumn)
                {
                    continue;
                }

                object Value = item.GetValue(entity);
                if (Value==null)
                {
                    continue;
                }
                querry += string.Format("{0},", item.Name);
                values += string.Format("@{0},", item.Name);
                cmd.Parameters.AddWithValue(string.Format("@{0}", item.Name), Value);
                //Tools.CMD.Parameters.AddWithValue(string.Format("@{0}", item.Name), Value);
            }

            querry = querry.Remove(querry.Length - 1, 1);
            values = values.Remove(values.Length - 1, 1);
            querry += string.Format(") {0})", values);

            cmd.CommandText = querry;
            //Tools.CMD.CommandText = querry;


            return cmd.Exec();
            //return Tools.CMD.Exec();
        }

        public Result<List<TT>> Select()
        {
            string querry = $"select * from {tableofTT.TableName}";

            SqlDataAdapter adp = new SqlDataAdapter(querry, Tools.Connection);
            return adp.Changer<TT>();

        }

        public Result<bool> Update(TT entity)
        {
            string querry = $"Update {tableofTT.TableName} set";
            PropertyInfo[] props = typeofTT.GetProperties();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = Tools.Connection;
            //Tools.CMD.Connection = Tools.Connection;
            string idColumnName = "";
            string idValue = "";

            foreach (PropertyInfo item in props)
            {
                if (item.Name== tableofTT.IdentityColumn)
                {
                    idColumnName = item.Name;
                    idValue = item.GetValue(entity).ToString();
                    continue;
                }
                object data = item.GetValue(entity);
                if (data == null || data == DBNull.Value)
                {
                    data = DBNull.Value;
                }
                querry += $" {item.Name} = @{item.Name},";
                cmd.Parameters.AddWithValue($"@{item.Name}", data);
                //Tools.CMD.Parameters.AddWithValue($"@{item.Name}", data);
            }
            querry = querry.Remove(querry.Length - 1, 1) + $" WHERE {idColumnName} = @{idColumnName}";
            cmd.Parameters.AddWithValue($"@{idColumnName}", idValue);
            //Tools.CMD.Parameters.AddWithValue($"@{idColumnName}", idValue);

            cmd.CommandText = querry;
            //Tools.CMD.CommandText = querry;

            return cmd.Exec();
            //return Tools.CMD.Exec();
        }

        public Result<List<TT>> SelectById(int id)
        {
            string querry = $"select * from {tableofTT.TableName} where {tableofTT.IdentityColumn} = {id}";
            SqlDataAdapter adp = new SqlDataAdapter(querry, Tools.Connection);
            return adp.Changer<TT>();
        }

        public Result<List<TT>> SelectForeignById(int? id)
        {
            string querry = $"select * from {tableofTT.TableName} where {tableofTT.ForeignColumn} = {id} and {tableofTT.IsActiveColumn} = 1";

            SqlDataAdapter adp = new SqlDataAdapter(querry, Tools.Connection);
            return adp.Changer<TT>();
        }

        public Result<List<TT>> SelectForeignByIdCom(int? id)
        {
            string querry = $"select * from {tableofTT.TableName} where {tableofTT.ForeignColumn} = {id} ";

            SqlDataAdapter adp = new SqlDataAdapter(querry, Tools.Connection);
            return adp.Changer<TT>();
        }

        public Result<List<TT>> OrderByDesc(int count)
        {
            string querry = $"SELECT TOP({count}) * FROM {tableofTT.TableName} WHERE IsActive = 1 ORDER BY {tableofTT.IdentityColumn} DESC";

            SqlDataAdapter adp = new SqlDataAdapter(querry, Tools.Connection);
            return adp.Changer<TT>();
        }

        public Result<List<TT>> OrderByAsc(int count)
        {
            string querry = $"SELECT TOP({count}) * FROM {tableofTT.TableName}  WHERE IsActive = 1 ORDER BY {tableofTT.IdentityColumn} ASC";

            SqlDataAdapter adp = new SqlDataAdapter(querry, Tools.Connection);
            return adp.Changer<TT>();
        }

        public Result<List<TT>> Contain(string columnName, string word)
        {
            string querry = $"SELECT * FROM {tableofTT.TableName} where {columnName} like '%{word}%' ";
            SqlDataAdapter adp = new SqlDataAdapter(querry, Tools.Connection);
            return adp.Changer<TT>();
        }

        public Result<List<TT>> SelectActive()
        {
            string querry = $"select * from {tableofTT.TableName} where {tableofTT.IsActiveColumn} = 1";

            SqlDataAdapter adp = new SqlDataAdapter(querry, Tools.Connection);
            return adp.Changer<TT>();
        }

     
    }
}
