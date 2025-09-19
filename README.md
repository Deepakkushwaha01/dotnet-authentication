# 🔐 Authentication API in .NET — Step by Step Explanation

Maine ek **Authentication API** banayi hai jo user ke **authentication (login, JWT tokens)** aur **authorization** ke liye kaam aati hai.
Is blog mein hum **Program.cs, Swagger setup, Versioning, aur Identity setup** ko line by line samjhenge.

---

## 📌 Program.cs — Entry Point of the Application

Sabse pehle `.NET 6/7/8` ke projects mein `Program.cs` entry point hota hai.

```csharp
using Authentication.API.Registers;

namespace Authentication.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            try
            {
                var builder = WebApplication.CreateBuilder(args);
                var app = BuildApplication(builder);
                RunApplication(app);
            }
            catch (System.Exception)
            {
                throw;
            }
        }
```

### 🔎 Explanation:

* `using Authentication.API.Registers;`
  👉 Humne ek **custom extension methods** wali class banayi hai (`Registers` folder ke andar). Ye modular code banane ke liye hai, taki saare registrations (Swagger, Identity, Versioning) clean rahen.

* `WebApplication.CreateBuilder(args)`
  👉 Ye ek **application builder** banata hai jisme configuration, logging, aur DI container register hota hai.

* `BuildApplication(builder)`
  👉 Humne application build karne ka logic ek alag method me rakha hai for **clean code**.

* `RunApplication(app)`
  👉 Ye method actual API ko **start/run** karta hai.

* `try-catch`
  👉 Exception handling ke liye, taki agar koi error aaye to crash na ho silently.

---

## 📌 BuildApplication Method

```csharp
private static WebApplication BuildApplication(WebApplicationBuilder builder)
{
    builder.Services.AddControllers();
    builder.Services
        // .AddDatabaseRegister(builder.Configuration)
        .RegisterSwagger(builder.Configuration)
        // .AddIdentityWithJwt(builder.Configuration)
        .RegisterVersioning();

    return builder.Build();
}
```

### 🔎 Explanation:

* `builder.Services.AddControllers();`
  👉 Ye **MVC controllers** ko register karta hai, jo APIs banane ke liye use hota hai.

* `.RegisterSwagger(builder.Configuration)`
  👉 Ye humara **Swagger (API documentation tool)** register karta hai. Swagger se hume ek **UI milta hai jaha hum API ko test kar sakte hain**.

* `.RegisterVersioning()`
  👉 Ye API **versioning** ko enable karta hai. Maan lo kal ko hum `v1` ke baad `v2` API banayein, to dono ko maintain karna easy hoga.

* `.AddDatabaseRegister` aur `.AddIdentityWithJwt` abhi commented hain
  👉 Ye future ke liye placeholders hain. Isme hum database aur JWT-based authentication register karenge.

---

## 📌 RunApplication Method

```csharp
private static void RunApplication(WebApplication app)
{
    app.UseSwaggerDocumentation(app.Configuration);
    app.UseHttpsRedirection();
    app.UseAuthentication();
    app.UseAuthorization();

    app.MapControllers();
    app.Run();
}
```

### 🔎 Explanation:

* `app.UseSwaggerDocumentation(app.Configuration)`
  👉 Ye humne ek custom extension banayi hai Swagger ke liye, taki code clean rahe. Ye Swagger ko configure karke ek **UI endpoint (`/docs`)** create karta hai.

* `app.UseHttpsRedirection();`
  👉 Ye sab requests ko **HTTPS pe redirect** karta hai (security ke liye).

* `app.UseAuthentication();`
  👉 Authentication middleware register karta hai. Ye check karega ki request ke headers me valid JWT token hai ya nahi.

* `app.UseAuthorization();`
  👉 Ye ensure karega ki user ke paas **role/permissions** hai ya nahi API access karne ke liye.

* `app.MapControllers();`
  👉 Ye humare `Controllers` ko endpoints banata hai.

* `app.Run();`
  👉 Application ko **start** karta hai.

---

## 📌 Swagger Register Extension

```csharp
public static IServiceCollection RegisterSwagger(this IServiceCollection services, IConfiguration config)
{
    services.AddEndpointsApiExplorer();
    services.AddSwaggerGen(o =>
    {
        o.SwaggerDoc("v1", new OpenApiInfo
        {
            Title = config["Swagger:Title"],
            Version = config["Swagger:Version"],
            Description = config["Swagger:Description"],
        });

        var securityScheme = new OpenApiSecurityScheme
        {
            Name = "Authorization",
            Type = SecuritySchemeType.Http,
            Scheme = "bearer",
            BearerFormat = "JWT",
            In = ParameterLocation.Header,
            Description = "Enter 'Bearer {token}'"
        };

        o.AddSecurityDefinition("Bearer", securityScheme);

        o.AddSecurityRequirement(new OpenApiSecurityRequirement
        {
            {
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    }
                },
                new string[] {}
            }
        });
    });

    return services;
}
```

### 🔎 Explanation:

* `services.AddSwaggerGen()`
  👉 Ye Swagger generator ko add karta hai jo JSON documentation banata hai.

* `SwaggerDoc("v1", new OpenApiInfo {...})`
  👉 Ye ek document banata hai API ke liye jisme **title, version, description** hoti hai.

* **JWT Security Scheme**
  👉 Humne ek security scheme define kiya hai taki Swagger UI me ek **Authorize button** aaye jaha user JWT token dal sake.

* `AddSecurityRequirement`
  👉 Ye ensure karta hai ki jo endpoints `[Authorize]` use karte hain, unko ye JWT scheme lage.

---

## 📌 Swagger UI Middleware

```csharp
public static IApplicationBuilder UseSwaggerDocumentation(this IApplicationBuilder app, IConfiguration config)
{
    app.UseSwagger(c => c.RouteTemplate = "docs/{documentName}/authentication.json");
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/docs/v1/authentication.json", config["Swagger:Title"]);
        c.RoutePrefix = config["Swagger:RoutePrefix"];
    });
    return app;
}
```

### 🔎 Explanation:

* `app.UseSwagger(...)`
  👉 Ye Swagger JSON document generate karta hai at `/docs/v1/authentication.json`.

* `app.UseSwaggerUI(...)`
  👉 Ye ek Swagger UI (interactive web page) create karta hai jaha hum apni API test kar sakte hain.

* `c.RoutePrefix = "docs"`
  👉 Matlab Swagger UI open karne ke liye hum `/docs` likhenge.

---

## 📌 appsettings.json — Configuration

```json
"Swagger": {
  "Title": "Authentication API",
  "Description": "API for user authentication and authorization",
  "Version": "v1",
  "RoutePrefix": "docs"
}
```

### 🔎 Explanation:

* `Title` → Swagger UI ke upar title
* `Description` → Short info about API
* `Version` → Current API version
* `RoutePrefix` → Swagger UI ka route (example: `/docs`)

---

✅ Abhi tak humne:

* Application structure samjha
* Swagger (API docs) setup kiya
* Middleware pipeline samjhi
* JWT security definition add kiya
