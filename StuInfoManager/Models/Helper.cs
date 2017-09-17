using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System.Data;
using System.Reflection;
using System.Data.Common;
using System.Data.SqlClient;
using System.Data.SQLite;

namespace StuInfoManager.Models
{
    /// <summary>
    /// 帮助类
    /// </summary>
    public class Helper
    {
        #region 数据库相关

        SQLiteConnection _Connection
        {
            get
            {
                SQLiteConnection conn = new SQLiteConnection("Data Source=|DataDirectory|\\Database.db");
                conn.Open();
                return conn;
            }
        }

        public void InsertUser(Users user)
        {
            using (SQLiteConnection conn = _Connection)
            {
                try
                {
                    user.Password = GetMd5(user.Password);
                    string sql = string.Format("insert into Users(Name,Password,DisplayName,IsAdmin,IsSuper,IsValid) values('{0}','{1}','{2}','{3}','{4}','{5}')",
                        user.Name, user.Password, user.DisplayName, user.IsAdmin ? 1 : 0, user.IsSuper ? 1 : 0, 1);
                    SQLiteCommand cmd = new SQLiteCommand(sql, conn);
                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }

        }

        public Users Login(string userName, string password)
        {
            using (SQLiteConnection conn = _Connection)
            {
                try
                {
                    password = GetMd5(password);
                    string sql = string.Format("select ID,Name,Password,DisplayName,IsAdmin,IsSuper,IsValid from Users where Name = '{0}' and Password = '{1}' and IsValid = '1'", userName, password);
                    SQLiteCommand cmd = new SQLiteCommand(sql, conn);
                    SQLiteDataAdapter sda = new SQLiteDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    sda.Fill(dt);
                    if (dt.Rows.Count == 0)
                        return default(Users);
                    Users user = DataRow2Model<Users>(dt.Rows[0]);
                    return user;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public bool CheckUser(string userName)
        {
            using (SQLiteConnection conn = _Connection)
            {
                try
                {
                    string sql = string.Format("select count(*) from Users where Name = '{0}' and IsValid = '1'", userName);
                    SQLiteCommand cmd = new SQLiteCommand(sql, conn);
                    return int.Parse(cmd.ExecuteScalar().ToString()) == 0;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public void ChangePass(Users user)
        {
            using (SQLiteConnection conn = _Connection)
            {
                try
                {
                    user.Password = GetMd5(user.Password);
                    string sql = string.Format("update Users set Password = '{0}' where ID = '{1}'", user.Password, user.ID);
                    SQLiteCommand cmd = new SQLiteCommand(sql, conn);
                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public void RemoveStudents(string ids)
        {
            using (SQLiteConnection conn = _Connection)
            {
                try
                {
                    string sql = string.Format("UPDATE Students SET IsValid = '0' WHERE ID IN ({0})", ids);
                    SQLiteCommand cmd = new SQLiteCommand(sql, conn);
                    cmd.ExecuteNonQuery();

                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public void InsertStudent(Students student)
        {
            using (SQLiteConnection conn = _Connection)
            {
                try
                {
                    string sql = string.Format("INSERT INTO Students(Code,Name,Sex,Birth,Nj,Bj,Zy,Ss,Ch,Address,Dh,UserId,IsValid) " +
                        "VALUES('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}')",
                        student.Code, student.Name, student.Sex == true ? 1 : 0, student.Birth.ToString("yyyy-MM-dd"), student.Nj, student.Bj, student.Zy,
                        student.Ss, student.Ch, student.Address, student.Dh, student.UserId, 1);
                    SQLiteCommand cmd = new SQLiteCommand(sql, conn);
                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public IEnumerable<Students> LoadStudents()
        {
            using (SQLiteConnection conn = _Connection)
            {
                try
                {
                    string sqlData = string.Format("SELECT Students.ID,Students.Code,Students.Name,Students.Sex,datetime(Students.Birth) Birth,Students.Nj,Students.Bj," +
                        "Students.Zy,Students.Ss,Students.Ch,Students.Address,Students.Dh,Students.UserId,Students.IsValid,Users.ID,Users.DisplayName FROM Students " +
                        "LEFT JOIN Users ON Users.ID = Students.UserId WHERE Students.IsValid = '1'");
                    SQLiteCommand cmd = new SQLiteCommand(sqlData, conn);
                    IDataReader dr = cmd.ExecuteReader();
                    IEnumerable<Students> result = DataReader2List<Students>(dr);
                    dr.Close();
                    return result;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public void UpdateStudent(Students student)
        {
            using (SQLiteConnection conn = _Connection)
            {
                try
                {
                    string sql = string.Format("UPDATE Students SET Code = '{0}',Name = '{1}', Sex = '{2}', Birth = '{3}',Nj = '{4}', Bj = '{5}', Zy = '{6}', " +
                        "Ss = '{7}', Ch = '{8}', Address = '{9}', Dh = '{10}' WHERE ID = '{11}'",
                        student.Code, student.Name, student.Sex ? 1 : 0, student.Birth.ToString("yyyy-MM-dd"), student.Nj, student.Bj, student.Zy,
                        student.Ss, student.Ch, student.Address, student.Dh, student.ID);
                    SQLiteCommand cmd = new SQLiteCommand(sql, conn);
                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public Students LoadStudent(int id)
        {
            using (SQLiteConnection conn = _Connection)
            {
                try
                {
                    string sqlData = string.Format("SELECT Students.ID,Students.Code,Students.Name,Students.Sex,datetime(Students.Birth) Birth,Students.Nj,Students.Bj," +
                        "Students.Zy,Students.Ss,Students.Ch,Students.Address,Students.Dh,Students.UserId,Students.IsValid,Users.ID,Users.DisplayName FROM Students " +
                        "LEFT JOIN Users ON Users.ID = Students.UserId WHERE Students.ID = '{0}'", id);
                    SQLiteCommand cmd = new SQLiteCommand(sqlData, conn);
                    SQLiteDataAdapter sda = new SQLiteDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    sda.Fill(dt);
                    if (dt.Rows.Count == 0)
                        return default(Students);
                    Students student = DataRow2Model<Students>(dt.Rows[0]);
                    return student;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public IEnumerable<Users> LoadUsers()
        {
            using (SQLiteConnection conn = _Connection)
            {
                try
                {
                    string sqlData = string.Format("SELECT ID,NULL Password,Name,DisplayName,IsAdmin,IsSuper,IsValid FROM Users WHERE IsValid = '1'");
                    SQLiteCommand cmd = new SQLiteCommand(sqlData, conn);
                    IDataReader dr = cmd.ExecuteReader();
                    IEnumerable<Users> result = DataReader2List<Users>(dr);
                    dr.Close();
                    return result;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public bool CheckStudent(string code, int id)
        {
            using (SQLiteConnection conn = _Connection)
            {
                try
                {
                    string sql = string.Format("select count(*) from Students where Code = '{0}' and ID <> '{1}' and IsValid = '1'", code, id);
                    SQLiteCommand cmd = new SQLiteCommand(sql, conn);
                    return int.Parse(cmd.ExecuteScalar().ToString()) == 0;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public void RemoveUsers(string ids)
        {
            using (SQLiteConnection conn = _Connection)
            {
                try
                {
                    string sql = string.Format("UPDATE Users SET IsValid = '0' WHERE ID IN ({0})", ids);
                    SQLiteCommand cmd = new SQLiteCommand(sql, conn);
                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        #endregion

        #region 非数据库方法

        public static string GetMd5(string str)
        {
            var md5 = MD5.Create();
            var bytes = md5.ComputeHash(Encoding.ASCII.GetBytes(str));
            StringBuilder result = new StringBuilder();
            for (int i = 0; i < bytes.Length; i++)
            {
                result.Append(bytes[i].ToString("X2"));
            }
            return result.ToString();
        }

        public static T DataRow2Model<T>(DataRow data) where T : new()
        {
            if (data == null)
                return default(T);
            T row = new T();
            var t = row.GetType();
            foreach (PropertyInfo pi in t.GetProperties())
            {
                if (data[pi.Name] != DBNull.Value)
                {

                    if (!pi.PropertyType.IsGenericType)
                    {
                        //非泛型
                        pi.SetValue(row, Convert.ChangeType(data[pi.Name], pi.PropertyType), null);
                    }
                    else
                    {
                        //泛型Nullable<>
                        Type genericTypeDefinition = pi.PropertyType.GetGenericTypeDefinition();
                        if (genericTypeDefinition == typeof(Nullable<>))
                        {
                            pi.SetValue(row, Convert.ChangeType(data[pi.Name], Nullable.GetUnderlyingType(pi.PropertyType)), null);
                        }
                    }
                }
            }
            return row;
        }

        public static IEnumerable<T> DataTabel2List<T>(DataTable data) where T : new()
        {
            if (data == null)
                return new List<T>();
            List<T> lst = new List<T>();
            T row;
            foreach (DataRow dr in data.Rows)
            {
                row = new T();
                var t = row.GetType();
                foreach (PropertyInfo pi in t.GetProperties())
                {
                    if (dr[pi.Name] != DBNull.Value)
                    {

                        if (!pi.PropertyType.IsGenericType)
                        {
                            //非泛型
                            pi.SetValue(row, Convert.ChangeType(dr[pi.Name], pi.PropertyType), null);
                        }
                        else
                        {
                            //泛型Nullable<>
                            Type genericTypeDefinition = pi.PropertyType.GetGenericTypeDefinition();
                            if (genericTypeDefinition == typeof(Nullable<>))
                            {
                                pi.SetValue(row, Convert.ChangeType(dr[pi.Name], Nullable.GetUnderlyingType(pi.PropertyType)), null);
                            }
                        }
                    }
                }
                lst.Add(row);
            }
            return lst;
        }

        public static IEnumerable<T> DataReader2List<T>(IDataReader data) where T : new()
        {
            if (data == null)
                return new List<T>();
            List<T> lst = new List<T>();
            T row;
            while (data.Read())
            {
                row = new T();
                var t = row.GetType();
                foreach (PropertyInfo pi in t.GetProperties())
                {
                    if (data[pi.Name] != DBNull.Value)
                    {

                        if (!pi.PropertyType.IsGenericType)
                        {
                            //非泛型
                            pi.SetValue(row, Convert.ChangeType(data[pi.Name], pi.PropertyType), null);
                        }
                        else
                        {
                            //泛型Nullable<>
                            Type genericTypeDefinition = pi.PropertyType.GetGenericTypeDefinition();
                            if (genericTypeDefinition == typeof(Nullable<>))
                            {
                                pi.SetValue(row, Convert.ChangeType(data[pi.Name], Nullable.GetUnderlyingType(pi.PropertyType)), null);
                            }
                        }
                    }
                }
                lst.Add(row);
            }
            return lst;
        }

        #endregion
    }
}