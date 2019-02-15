using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TwilioWebApp.Models;
using System.Data;
using System.Data.Common;
using System.Configuration;
using System.Data.OleDb;

namespace TwilioWebApp.Data
{
    public class DbRequestPersistor : IRequestPersistor
    {
        DbPersistorHelper persistorHelper = new DbPersistorHelper();

        public TextRequest Get(string requestId)
        {
            TextRequest tr = new TextRequest();
            using (OleDbConnection conn = persistorHelper.GetConnection())
            {
                OleDbCommand cmd = conn.CreateCommand();
                cmd.CommandText = "SELECT * FROM smsSms WHERE Id = ?";
                cmd.Parameters.AddWithValue("?", requestId);

                DataTable dataTable = new DataTable();
                conn.Open();
                dataTable.Load(cmd.ExecuteReader());
                int rowCount = dataTable.Rows.Count; 
                foreach (DataRow row in dataTable.Rows)
                {
                    tr.Id = row["id"].ToString();
                    tr.ToPhone = row["toPhone"].ToString();
                    tr.Body = row["message"].ToString();
                    tr.RequestTime = (DateTime)row["reqTime"];
                    break;
                }
                conn.Close();
            }
            return tr;
        }

        public void Put(TextRequest request)
        {
            using (OleDbConnection conn = persistorHelper.GetConnection())
            {
                conn.Open();
                OleDbCommand cmd = conn.CreateCommand();
                cmd.CommandText = "UPDATE smsSms SET RespTime = ?, Response = ? WHERE Id = ?";
                cmd.Parameters.AddWithValue("?", request.ResponseTime);
                cmd.Parameters.AddWithValue("?", request.Response);
                cmd.Parameters.AddWithValue("?", request.Id);
                cmd.ExecuteNonQuery();
                conn.Close();
            }
        }

    }
}