using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Web;
using TwilioWebApp.Models;

namespace TwilioWebApp.Data
{
    public class DbTextMessagePersistor : ITextMessagePersistor
    {
        DbPersistorHelper persistorHelper = new DbPersistorHelper();

        public TextMessage Get(string requestId)
        {
            TextMessage msg = new TextMessage();
            using (OleDbConnection conn = persistorHelper.GetConnection())
            {
                OleDbCommand cmd = conn.CreateCommand();
                cmd.CommandText = "SELECT * FROM smsLog WHERE Id = ?";
                cmd.Parameters.AddWithValue("?", requestId);

                DataTable dataTable = new DataTable();
                conn.Open();
                dataTable.Load(cmd.ExecuteReader());
                int rowCount = dataTable.Rows.Count;
                foreach (DataRow row in dataTable.Rows)
                {
                    msg.Id = row["id"].ToString();
                    msg.ToPhone = row["toPhone"].ToString();
                    msg.FromPhone = row["fromPhone"].ToString();
                    msg.Message = row["message"].ToString();
                    msg.When = (DateTime)row["when"];
                    break;
                }
                conn.Close();
            }

            return msg;
        }

        public void Put(TextMessage message)
        {
            using (OleDbConnection conn = persistorHelper.GetConnection())
            {
                conn.Open();
                OleDbCommand cmd = conn.CreateCommand();
                if (String.IsNullOrEmpty(message.Direction))
                    message.Direction = Constants.Direction.Inbound;
                if (message.Direction == Constants.Direction.Inbound)
                {
                    if (String.IsNullOrEmpty(message.Id))
                        message.Id = Guid.NewGuid().ToString();

                    message.When = DateTime.Now;

                    cmd.CommandText = "insert into smsLog (Id,ToPhone,FromPhone,Message,When,ProviderId,Direction) values (?,?,?,?,?,?,?)";
                    cmd.Parameters.AddWithValue("?", message.Id);
                    cmd.Parameters.AddWithValue("?", message.ToPhone);
                    cmd.Parameters.AddWithValue("?", message.FromPhone);
                    cmd.Parameters.AddWithValue("?", message.Message);
                    cmd.Parameters.AddWithValue("?", message.When);
                    cmd.Parameters.AddWithValue("?", message.ProviderId);
                    cmd.Parameters.AddWithValue("?", message.Direction);
                }
                else
                {
                    cmd.CommandText = "UPDATE smsLog SET FromPhone = ?, ProviderId = ? WHERE Id = ?";
                    cmd.Parameters.AddWithValue("?", message.FromPhone);
                    cmd.Parameters.AddWithValue("?", message.ProviderId);
                    cmd.Parameters.AddWithValue("?", message.Id);
                }
                cmd.ExecuteNonQuery();
                conn.Close();
            }
        }
    }
}