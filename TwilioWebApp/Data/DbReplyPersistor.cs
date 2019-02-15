using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Web;
using TwilioWebApp.Models;

namespace TwilioWebApp.Data
{
    public class DbReplyPersistor : IReplyPersistor
    {
        DbPersistorHelper persistorHelper = new DbPersistorHelper();

        public void Put(TextReply reply)
        {
            using (OleDbConnection conn = persistorHelper.GetConnection())
            {
                conn.Open();
                OleDbCommand cmd = conn.CreateCommand();
                cmd.CommandText = "INSERT INTO smsReply (smsId, Received,FromPhone,Message,ProviderId) VALUES (?,?,?,?,?)";
                cmd.Parameters.AddWithValue("?", Guid.NewGuid().ToString());
                cmd.Parameters.AddWithValue("?", DateTime.Now);
                cmd.Parameters.AddWithValue("?", reply.FromPhone);
                cmd.Parameters.AddWithValue("?", reply.Message);
                cmd.Parameters.AddWithValue("?", reply.ProviderId);
                cmd.ExecuteNonQuery();
                conn.Close();
            }
        }
    }
}