using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.OleDb;
using System.Linq;
using System.Web;

namespace TwilioWebApp.Data
{
    public class DbPersistorHelper
    {
        String ConnectionString;

        public DbPersistorHelper()
        {
            ConnectionStringSettings connStrSettings = ConfigurationManager.ConnectionStrings["Host"];
            ConnectionString = connStrSettings.ConnectionString;
        }

        public OleDbConnection GetConnection()
        {
            OleDbConnection conn = new OleDbConnection(ConnectionString);
            return conn;
        }
    }
}