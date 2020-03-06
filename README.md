# OrderManagementApi
Using ASPNET to create a order management API

Setup

1. SQL Server Management Studio
2. Make sure you have installed nuget package Microsoft.EntityFrameworkCore.Tools(3.1.2)
3. Go to Manage User Secrets, and set your connectionstring to your localdb.
4. Open Package Manager Console, and run: Update-Database
5. (After starting the Frontend application, built with React), check which port (XXXX) the React App is running on: http://localhost:XXXX/
and set the port on line 53 in Startup.cs: app.UseCors(options => options.WithOrigins("http://localhost:XXXX")


Technologies

C#
ASP.NET
