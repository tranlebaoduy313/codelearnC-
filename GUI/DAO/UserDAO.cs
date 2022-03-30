using MyApp.Models;
using System;
using System.Collections.Generic;
using System.Data;

namespace MyApp.DAO
{
    public class UserDAO
    {
        public string ConnectionString { get; private set; }

        public UserDAO(string connectionString)
        {
            this.ConnectionString = connectionString;
        }

        public List<UserDTO> GetAll()
        {
            DataAccessHelper helper = new DataAccessHelper(this.ConnectionString);
            helper.Open();

            string sqlQuery = "SELECT * FROM [USER]";
            DataTable table = helper.ExecuteSelect(sqlQuery);
            List<UserDTO> userList = new List<UserDTO>();

            for (var i = 0; i < table.Rows.Count; i++)
            {
                DataRow row = table.Rows[i];
                UserDTO userDTO = new UserDTO();
                userDTO.UserId = (string)row["USER_ID"];
                userDTO.UserName = (string)row["USER_NAME"];
                userDTO.Password = (string)row["PASSWORD"];
                userList.Add(userDTO);
            }

            return userList;
        }

        internal void AddUser(UserDTO userDTO)
        {
            DataAccessHelper helper = new DataAccessHelper(this.ConnectionString);
            helper.Open();

            string sqlQuery = "INSERT INTO [USER](USER_ID,USER_NAME,PASSWORD) VALUES (@UserId,@UserName,@Password)";

            List<System.Data.SqlClient.SqlParameter> sqlParameters = new List<System.Data.SqlClient.SqlParameter>();

            System.Data.SqlClient.SqlParameter sqlParam = new System.Data.SqlClient.SqlParameter("@UserId", SqlDbType.VarChar);
            sqlParam.Value = userDTO.UserId;
            sqlParameters.Add(sqlParam);

            sqlParam = new System.Data.SqlClient.SqlParameter("@UserName", SqlDbType.NVarChar);
            sqlParam.Value = userDTO.UserName;
            sqlParameters.Add(sqlParam);

            sqlParam = new System.Data.SqlClient.SqlParameter("@Password", SqlDbType.NVarChar);
            sqlParam.Value = userDTO.Password;
            sqlParameters.Add(sqlParam);

            helper.ExecuteNonQuery(sqlQuery, sqlParameters);
        }
    }
}
