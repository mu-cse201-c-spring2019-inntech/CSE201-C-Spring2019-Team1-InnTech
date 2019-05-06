STEP 1
Open the project,then you click view---SQL Server object explorer. Click localdb\MSSQLLocalDB---database,left click 'Databases' add new database. Name it InnGameStore, select database locate in your
project's App_data file, ex: C:\Users\Desktop\CSE201-C-Spring2019-Team1-InnTech\NetTest\App_Data.

STEP 2
Open the insertGameApps.sql in visual studio.Before you run the sql file, there is a method called BULK INSERT GameApps from...., change the address to where your GameApps.txt is.

STEP 3
click small green triangle buttom to run it, the connect windows will be showed, select local---MSSQLLocalDB, click connect.
On the left of the green buttom, switch master DB to InnGameStore DB, and run it again. 
It should works now, and try the buttom in Website to see if it is working.
