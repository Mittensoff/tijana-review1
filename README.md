# TijanaSpalevic_Omega
1.step
Download MS SQL Server Management Studio from this link
https://docs.microsoft.com/en-us/sql/ssms/download-sql-server-management-studio-ssms?view=sql-server-ver15

1.1.
In git repository, in folder Database, you will find FoodDeliveryDB.dacpac file that you should deploy after installation of MS SQL Server Management Studio is finished

1.2.
Opening MS SQL Server Management Studio first time, you will have to set up the connection
In opened form, for server type, you should select Database Engine.
Next one is Server name, that will be "yourComputerName/sqlexpress"

***To find name of your computer, please follow these simple steps: Click on the Start button. In the search box, type Computer. Right click on This PC within the search results and select Properties. 
Under Computer name, you will see name of your computer.***

In SQL Server next one is Authentication. There you will choose "Windows Authentication" and click "Connect"

1.3.
Now, when you're connected, you can deploy that .dacpac file
Under your server name tab, you will see "Databases". Right click on "Databases" and choose "Deploy Data-tier application".
This will be very simple form NNF, where you only choose downloaded .dacpac file from downloaded location and if not, for database name you type in "FoodDeliveryDB".

1.4.
Under databases tab, you will now see FoodDeliveryDB, and all its tables and dependencies. If not, just click on Databases and refresh.

-----------------------------------------------------------------------------------------------------------------------------------------------------------------
2.step
In order to see code for my ASP.NET MVC Web Application, you will have to install Microsoft Visual Studio that has .NET Framework in it.
https://visualstudio.microsoft.com/downloads/
(Downloads-> Visual Studio 2019 -> Choose Community)
(.NET Framework version 4.8)

2.1.
Open .sln file in Visual Studio, go to web.config and change Connection string.
Under name "FoodDeliveryDBEntities" you have to change Data source section to your Server name, which you set in MS SQLServer Management Studio in 1.2.step.

2.2.
You are reaady to build and run "Food Delivery Sunshine&Co" app.
