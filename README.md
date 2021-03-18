# EF-Dapper-DAL
A demonstration .NET Core Data Access Layer (DAL) using a mixture of Entity Framework (EF) and Dapper ORMs

## Contents

- [Technologies](#Technologies)
- [Installation](#Installation)
- [Usage](#Usage)
- [Recreationsteps](#RecreationSteps)

## Technologies
This solution contains a Data Access, a test project and was created with the following technologies, although earlier IDE versions may be compatible:
* Visual Studio 2019
* .NET Core 3.1
* Entity Framework Core 5.0
* Dapper 2.0
* SQL Server 2019
* NUnit 3.12

## Installation
1. Clone the repo: <code>git clone https://github.com/offstone/EF-Dapper-DAL</code>
1. Install NuGet packages and build.
1. Update the NorthwindDBConnectionString connection string inside the <code>appsettings.json</code> files. There are 2 found in the root of each project.
    Point to your local SQL server instance, use an account with permission to create databases. For this demo I've used a cut down copy of the old Northwind database and called it [NorthwindCopy].
1. To initialise the database either run the EF migrations from the DAL project: <code>PM> Update-Database</code>  
    Or
    Run the full SQL initialisation script: <code> SQL/Install_NorthwindCopy_Full.sql</code>
    If using the full SQL script prevent migrations from running subsequently.

## Usage
This DAL demo can be run against the initialised [NorthwindCopy] database from the integration test project.
Simply run the tests and a number of DAL operations will be performed against the DB using the connection string within the test project's <code>appsettings.json</code>

## RecreationSteps

1. Create a new .NET Core library project.
1. Add the following packages via NuGet or Package Manager -> Install-Package:
```
    PM> Install-Package Microsoft.EntityFrameworkCore.SqlServer  
    PM> Install-Package Microsoft.EntityFrameworkCore.Tools  
    PM> Install-Package Microsoft.EntityFrameworkCore.Design  
    PM> Install-Package System.Configuration.ConfigurationManager  
    PM> Install-Package Microsoft.Extensions.Configuration  
    PM> Install-Package Microsoft.Extensions.Configuration.Json  
    PM> Install-Package Dapper
```
1. Add an <code>appsettings.json</code> config file, connection string for the use by migrations.  
Set to copy to output directory.
1. Create the [NorthwindCopy] database with either Northwind.sql or EF Migrations:
PM> Update-Database
1. Scaffold the entities from DB tables into /Entities folder:  
<code>PM> Scaffold-DbContext -Connection "Server=[ADD_SERVERNAME_HERE];Database=NorthwindCopy;Trusted_Connection=True;" -Provider "Microsoft.EntityFrameworkCore.SqlServer" -Tables Categories,Customers,Employees,EmployeeTerritories,OrderDetails,Orders,Products,Region,Shippers,Suppliers,Territories -OutputDir "Entities"</code>
1. Add a generic repository pattern into /Repository folder.
1. Create the initial migration:  
    <code>PM> Add-Migration InitialMigration -Context NorthwindContext</code>
1. Create a unit test project to set up integration tests over the DAL and DB.
1. Set up ConfigureServices() in the test project to configure dependency injection and connection string from <code>appsettings.json</code>
1. Create a bunch of integration tests to call Data Access Layer functionality.
