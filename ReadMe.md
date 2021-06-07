Tools used 
Serilog is used for the logging 
dotnet ef migrations add InitialMigration -c ApplicationDbContext

This will add the migration for tables needed by ASP.NET Identity.

dotnet ef migrations add InitialCreate --project "C:\Users\Abuba\source\repos\Amygdalab\Amygdalab.Data"

dotnet ef migrations add InitialIdentityServerPersistedGrantDbMigration --project "C:\Users\Abuba\source\repos\Amygdalab\Amygdalab.Data" -c PersistedGrantDbContext -o Migrations/IdentityServer/PersistedGrantDb

This adds the migration for two tables: PersistedGrants and DeviceCodes needed by IdentityServer

dotnet ef migrations add InitialIdentityServerConfigurationDbMigration -c 
ConfigurationDbContext -o Migrations/IdentityServer/ConfigurationDb

/.well-known/openid-configuration

https://localhost:44354/.well-known/openid-configuration