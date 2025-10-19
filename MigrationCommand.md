dotnet ef migrations add InitialIdentity -p Core/Authentication.Core.Persistence -s Authentication.Api

dotnet ef database update -p Core/Authentication.Core.Persistence -s Authentication.Api               

'ef migrations remove'