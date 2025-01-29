using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using System.Text;
using ElderlyCareSupport.Application;
using ElderlyCareSupport.Application.IService;
using ElderlyCareSupport.Application.Service;
using ElderlyCareSupport.Infrastructure;
using ElderlyCareSupport.Server.Configuration;
using ElderlyCareSupport.Server.Middleware;
using FluentValidation;


var builder = WebApplication.CreateBuilder(args);
var jwtSettings = builder.Configuration.GetSection("JWT");

builder.Services.AddApplication()
    .AddInfrastructure()
    .AddCompressionConfig();

builder.Services.AddCors();

builder.Host.UseSerilog((context, configuration) => { configuration.ReadFrom.Configuration(context.Configuration); });

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddValidatorsFromAssembly(typeof(Program).Assembly);
builder.Services.AddSwaggerConfiguration();

builder.Services.AddScoped<IDbConnectionFactory, DbConnectionFactory>(db =>
    new DbConnectionFactory(builder.Configuration.GetConnectionString("ElderDB")!));

// builder.Services.AddDbContext<ElderlyCareSupportContext>(options =>
// {
//     options.UseSqlServer(builder.Configuration.GetConnectionString("ElderDB"));
// });

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.Authority = jwtSettings["Issuer"];
        options.Audience = jwtSettings["ClientId"]; // This is the Client ID you created in Keycloak
        options.RequireHttpsMetadata = false; // For development purposes only. Set to true in production.
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidAudience = "ElderlyCareAccountClient",
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["SecretKey"]!))
        };
    });

builder.Services.AddAuthorization();
builder.Services.AddLogging();

builder.Services.AddScoped<GlobalErrorHandler>();
// Repository Registration

builder.Services.AddHttpClient();


var app = builder.Build();

app.UseCompressionConfig();
app.UseCors(
    options => options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseSerilogRequestLogging();
app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();


app.UseMiddleware<GlobalErrorHandler>(); 

app.MapControllers();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
await app.RunAsync();