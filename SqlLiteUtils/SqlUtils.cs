using System;
using System.Data.SqlClient;
using System.Data.SQLite;

namespace SqlLiteUtils
    /**
    * @author Madmegsox1
    */

{
    public class SqlUtils
    {
        public static SQLiteConnection createConnection(string dbLocation)
        {
            SQLiteConnection connection =
                new SQLiteConnection("Data Source="+ dbLocation +";Version=3;New=True;Compress=True;");

            try
            {
                connection.Open();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            return connection;
        }

    }
}