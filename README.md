<div align = "center">
<h1>SqlLiteUtils</h1>

</br>

You will need to download the nuget package `System.Data.SQLite`

Then add the binary to dependencies

<img src="https://i.ibb.co/hWYcvbs/asd.png"/>

<div align = "left">
  
```c#
SqlDatabase database = new SqlDatabase("test2.db");
List<string> colNames = new List<string>();
colNames.Add("name"); // INDEX 0 COLL 0
colNames.Add("age"); //INDEX 1 COLL 1


List<DataTypes> dataTypes = new List<DataTypes>();
dataTypes.Add(DataTypes.TEXT); // Data type for COLL 0
dataTypes.Add(DataTypes.INT); // Data type for COLL 1

database.CreateTable("deez", colNames, dataTypes); // <- creates the table

List<string> data = new List<string>();
data.Add("John"); // Data for COLL 0
data.Add("40");  // Data for COLL 1

database.InsertData("deez", data, colNames, dataTypes); // <- Inserts the data into the table

List<string> colls = new List<string>();
colls.Add("age");
colls.Add("name");

List<string> dataR = database.ReadData("deez", colls); // <- Reads the data for the specific colls
database.Close(); // <- closes the database

foreach (var d in dataR)
{
    Console.WriteLine(d);
}
```
</div>
  
  <h2>How this works:</h2>
  <p>
    Uses multiple listes where the indexes correspond to the colls names or colls data types
  <p/>

</div>
