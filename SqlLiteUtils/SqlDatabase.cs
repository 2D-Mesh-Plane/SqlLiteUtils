using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;

namespace SqlLiteUtils
    /**
    * @author Madmegsox1
    */

{
    public class SqlDatabase
    {
        private SQLiteConnection _connection;
        public SqlDatabase(string dbLocation)
        {
            _connection = SqlUtils.createConnection(dbLocation);
        }
        

        public void CreateTable(string tableName, List<string> colNames,List<DataTypes> dataTypes)
        {
            if (colNames.Count > dataTypes.Count || dataTypes.Count > colNames.Count)
            {
                Console.WriteLine("Error");
                return;
            }

            string query = "(id INTEGER PRIMARY KEY AUTOINCREMENT, ";
            int i = 0;
            foreach (var datatype in dataTypes)
            {
                if (i + 1 == dataTypes.Count)
                {
                    switch (datatype)
                    {
                        case DataTypes.INT:
                            query += colNames[i] + " INT";
                            break;
                        case DataTypes.TEXT:
                            query += colNames[i] + " VARCHAR(20)";
                            break;
                        case DataTypes.NULL:
                            query += colNames[i] + " NULL";
                            break;
                        case DataTypes.REAL:
                            query += colNames[i] + " REAL";
                            break;
                    }
                }
                else
                {

                    switch (datatype)
                    {
                        case DataTypes.INT:
                            query += colNames[i] + " INT, ";
                            break;
                        case DataTypes.TEXT:
                            query += colNames[i] + " VARCHAR(20), ";
                            break;
                        case DataTypes.NULL:
                            query += colNames[i] + " NULL, ";
                            break;
                        case DataTypes.REAL:
                            query += colNames[i] + " REAL, ";
                            break;
                    }
                }

                i++;
            }

            query += ")";
            
            SQLiteCommand command = _connection.CreateCommand();
            command.CommandText = "CREATE TABLE " + tableName + " "+ query;
            command.ExecuteNonQuery();
        }

        public void InsertData(string tableName, List<string> data, List<string> colNames, List<DataTypes> dataTypes)
        {
            if (colNames == null)
            {
                Console.WriteLine("Please create a table...");
                return;
            }

            string query = " (";
            int i = 0;
            foreach (var names in colNames)
            {
                if (i + 1 == colNames.Count)
                {
                    query += names;
                }
                else
                {
                    query += names + ", ";
                }

                i++;
            }

            query += ") VALUES (";

            i = 0;
            foreach (var values in data)
            {
                DataTypes type = dataTypes[i];
                if (type == DataTypes.TEXT)
                {
                    if (i + 1 == data.Count)
                    {
                        query += "'" + values + "'";
                    }

                    else
                    {
                        query += "'" + values + "', ";
                    }
                }
                else
                {
                    if (i + 1 == data.Count)
                    {
                        query += values;
                    }
                    else
                    {
                        query += values + ", ";
                    }
                }

                i++;
            }

            query += ")";

            SQLiteCommand command = _connection.CreateCommand();
            command.CommandText = "INSERT INTO " + tableName  + query;
            command.ExecuteNonQuery();
        }

        public List<string> ReadData(string tableName, List<string> cols)
        {
            List<string> rVal = new List<string>();
            
            SQLiteDataReader reader;
            SQLiteCommand command;
            
            command = _connection.CreateCommand();

            string query = "SELECT ";
            int i = 0;
            foreach (var colName in cols)
            {
                if (i + 1 == cols.Count)
                {
                    query += colName + " ";
                }
                else
                {
                    query += colName + ", ";
                }

                i++;
            }

            query += "FROM " + tableName;
            command.CommandText = query;
            
            
            reader = command.ExecuteReader();
            
            while (reader.Read())
            {
                for (var index = 0; index < cols.Count; index++)
                {
                    string text = reader.GetValue(index).ToString();
                    rVal.Add(text);
                }
            }

            reader.Close();
            return rVal;
        }


        public void Close()
        {
            _connection.Close();
        }

        public void ExecuteQuery(string query)
        {
            SQLiteCommand command = _connection.CreateCommand();
            command.CommandText =  query;
            command.ExecuteNonQuery();
        }

    }
}