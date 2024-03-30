ASP.NET WEB API


## Instructions:
### Database
Create new database with name RoyalTea in SQL Server. Run RoyalTea.sql script.

Access appsettings.Development.json in API layer and change ConnectionString to the appropriate on your machine. Leave other parameters there (such as MultipleActiveResultSets).

### Backend
You can simply use "dotnet run" command inside Api folder.
If running through Visual Studio then Release mode is recommended to avoid exceptions from showing before being handled by global exception hander.

### Frontend
Paths and ports can be configured in app/constants/config.ts
Don't forget to run "npm i".

Credentials for an account with all privileges:
Username: admin 
Password: password

Credentials for an account with default user privileges:
Username: user 
Password: password

[WIKI (old)](https://github.com/mihailoilic/RoyalTea_Backend/wiki)
