using FirstWeb;

string dataDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Database");
AppDomain.CurrentDomain.SetData("DataDirectory", dataDirectory);
new Server("127.0.0.1", 8080).Start();