using MyApp2.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace MyApp2.DAO
{
    public class StudentDAO
    {
        public string ConnectionString { get; set; }

        public StudentDAO(string connetionString)
        {
            this.ConnectionString = connetionString;
        }

        public List<StudentDTO> GetAll()
        {
            DataAccessHelper helper = new DataAccessHelper(this.ConnectionString);
            helper.Open();

            string sqlQuery = "SELECT * FROM [STUDENT]";
            DataTable table = helper.ExecuteSelect(sqlQuery);
            List<StudentDTO> userList = new List<StudentDTO>();

            for (var i = 0; i < table.Rows.Count; i++)
            {
                DataRow row = table.Rows[i];
                StudentDTO studentDTO = new StudentDTO();
                studentDTO.NameID = (string)row["NAME_ID"];
                studentDTO.Name = (string)row["NAME"];
                studentDTO.City = (string)row["CITY"];
                studentDTO.Address = (string)row["ADDRESS"];
                userList.Add(studentDTO);
            }

            return userList;
        }

        internal void AddStudent(StudentDTO studentDTO)
        {
            DataAccessHelper helper = new DataAccessHelper(this.ConnectionString);
            helper.Open();

            string sqlQuery = "INSERT INTO [STUDENT](NAME_ID,NAME,CITY,ADDRESS) VALUES (@NameId,@Name,@City,@Address)";

            List<SqlParameter> sqlParameters = new List<SqlParameter>();

            SqlParameter sqlParam = new SqlParameter("@NameID", SqlDbType.Char);
            sqlParam.Value = studentDTO.NameID;
            sqlParameters.Add(sqlParam);

            sqlParam = new SqlParameter("@Name", SqlDbType.NVarChar);
            sqlParam.Value = studentDTO.Name;
            sqlParameters.Add(sqlParam);

            sqlParam = new SqlParameter("@City", SqlDbType.NVarChar);
            sqlParam.Value = studentDTO.City;
            sqlParameters.Add(sqlParam);

            sqlParam = new SqlParameter("@Address", SqlDbType.NVarChar);
            sqlParam.Value = studentDTO.Address;
            sqlParameters.Add(sqlParam);

            helper.ExecuteNonQuery(sqlQuery, sqlParameters);
        }

        internal void DeleteStudent(string NameID)
        {
            DataAccessHelper helper = new DataAccessHelper(this.ConnectionString);
            helper.Open();

            string sqlQuery = string.Format("DELETE FORM STUDENT WHERE NAME_ID = {0}", NameID);

            helper.Execute(sqlQuery);
        }
    }
}