Tools used 
Serilog is used for the logging 
Swagger API documentation was used 
Entity Franwork for ORM
Identity Server For API and Token validation 
Sql server db was used
.net 5
Web project - > the Contoller , Middleware configuration and Filters and Extensions also contain the clientapp files 
of angular 12 and typescript.
To run the angular project  -- > npm install is neccessary 
Core Lib  - > Handles the common tool used accross the system 
Domin Lib  -> Handles the repository and managers and UnitOfWork Tool.
Repository design pattern was used.
Data Lib  -> Manages interaction to db using the db context of entity framework  and Migrations 

dotnet ef migrations add InitialMigration -c ApplicationDbContext

This will add the migration for tables needed by ASP.NET Identity.

dotnet ef migrations add InitialCreate --project "C:\Users\Abuba\source\repos\Amygdalab\Amygdalab.Data"

dotnet ef migrations add InitialIdentityServerPersistedGrantDbMigration --project "C:\Users\Abuba\source\repos\Amygdalab\Amygdalab.Data" -c PersistedGrantDbContext -o Migrations/IdentityServer/PersistedGrantDb

This adds the migration for two tables: PersistedGrants and DeviceCodes needed by IdentityServer

dotnet ef migrations add InitialIdentityServerConfigurationDbMigration -c 
ConfigurationDbContext -o Migrations/IdentityServer/ConfigurationDb

/.well-known/openid-configuration

https://localhost:44354/.well-known/openid-configuration

npm install --global @angular/cli@next