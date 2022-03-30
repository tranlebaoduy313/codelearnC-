using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace MyApp.DAO
{
    public class DataAccessHelper
    {
        public string ConnectionString { get; private set; }
        public SqlConnection Connection { get; private set; }

        public DataAccessHelper(string connectionString)
        {
            this.ConnectionString = connectionString;
            this.Connection = new SqlConnection(this.ConnectionString);
        }

        public void Open()
        {
            this.Connection.Open();
        }

        public DataTable ExecuteSelect(string sqlQuery, List<SqlParameter> sqlParameters = null)
        {
            SqlCommand command = new SqlCommand();
            command.Connection = this.Connection;
            command.CommandText = sqlQuery;

            if (sqlParameters != null)
            {
                for (int i = 0; i < sqlParameters.Count; i++)
                {
                    command.Parameters.Add(sqlParameters[i]);
                }
            }

            DataTable table = new DataTable();

            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = command;
            adapter.Fill(table);

            return table;
        }

        internal int ExecuteNonQuery(string sqlQuery, List<SqlParameter> sqlParameters)
        {
            SqlCommand command = new SqlCommand();
            command.Connection = this.Connection;
            command.CommandText = sqlQuery;

            if (sqlParameters != null)
            {
                for (int i = 0; i < sqlParameters.Count; i++)
                {
                    command.Parameters.Add(sqlParameters[i]);
                }
            }

            int result = command.ExecuteNonQuery();

            return result;
        }
    }
}