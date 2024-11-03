# ToDoApp

This is my first API project made with asp.net. This projects represents a simple Rest API for a ToDoApp.

## Technologies Used
- ASP.NET Core 8
- Entity Framework Core
- Swagger for API documentation
- Microsoft SQL Server

## Prerequisites
- .NET SDK installed
- Microsoft SQL Server
- EF Core CLI tools installed

## NuGet packages
- Microsoft.AspNetCore.Authentication.JwtBearer
- Microsoft.AspNetCore.Identity.EntityFrameworkCore
- Microsoft.AspNetCore.Mvc.NewtonsoftJson
- Microsoft.AspNetCore.OpenApi
- Microsoft.EntityFrameworkCore.Design
- Microsoft.EntityFrameworkCore.SqlServer
- Microsoft.EntityFrameworkCore.Tools
- Microsoft.Extensions.Identity.Core
- Swashbuckle.AspNetCore

By default this projects uses `http://localhost:5213` and runs with Swagger. 
You can change the http address in launchSettings.json file in Properties folder(Path: ToDoApp/backend/api/Properties/launchSettings.json) at the line 17 and dont forget to copy new address in appsettings.json(Path: ToDoApp/backend/api/appsettings.json) lines 16 and 17.

## Setup instructions
If you would like to run this project folow this steps:
1) Got to `ToDoApp/backend/api/appsettings.json` and write a Connectionstring to a Microsoft SQL Server(Section `"ConnectionStrings":"DefaultConnection"`, line 4).
   Example: `"DefaultConnection"`:`"Data Source={PCName}\\SQLEXPRESS;Initial Catalog={DataBaseName};Persist Security Info=True;User ID=sa;Password={Password};Pooling=False;Encrypt=True;Trust Server Certificate=True"`
1) Got to `ToDoApp/backend/api/appsettings.json` and write a Connectionstring to a Microsoft SQL Server(Section `"ConnectionStrings":"DefaultConnection"`, line 4)
2) also in appsettings.json fill the section `"JWT":"SignInKey"` line 18 with a long Key(at least 64 characters)
3) if you set another http/https address in `launchSettings.json` dont forget to paste it at `"JWT":"Issuer"` line 16 and `"JWT":"Audience"` line 17.
4) before starting delete folder Migrations(Path: `ToDoApp/backend/api/Migrations`) and run in the terminal command `dotnet ef migrations add NAME` and `dotnet ef database update`.
   It should generate you new Migrations folder and new tables in your database.
5) now you can run the project with the command `dotnet watch run` or you can start debugging

After running the project, Swagger will launch at `http://localhost:5213/swagger` by default, providing an interactive UI to test the API. 

## API overview

## Account controller
Account controller contains 2 POST Methods:
1) POST /api/account/login - requaiers Username or Email and Password of an user
2) POST /api/account/register - requaiers email, username, password and password repeat to register new user

I implemented Authorization using JWT to ensure that only authorized users can access their ToDos. For testing ToDo controller you will also need a valid authorization. Therfore please register new user and copy the token from response body section "token": "A_Huge_Token". Then click Authorize button(top-right side of the page), paste the Token in value field and click authorize. You can save the token and use it again.

## ToDo controller
ToDo controller contains 4 Methods:
1) GET /api/todos - it shows all ToDos
2) POST /api/todos - it creates new ToDo. It requiers a titel, a task and state. Optionaly you can add a time shedule, just set Withdeadline as true and fill FromeDate and ToDate fields in format yyyy-mm-dd. Make sure those are the dates in the future.
3) DELETE /api/todos/{id} - it removes the ToDo with the id of type integer from Route
4) PUT /api/todos/{id} - it chenges the ToDo with the id of type integer from Route with the data from body. You can optionaly update the FromDate and ToDate.


Thank you for your attention. I hope you enjoy my project.
