using Delta;
using ElderlyCareSupport.Server.Common;
using ElderlyCareSupport.Server.DTOs;
using ElderlyCareSupport.Server.Helpers;
using ElderlyCareSupport.Server.Repositories.Implementations;
using ElderlyCareSupport.Server.Repositories.Interfaces;
using ElderlyCareSupport.Server.Services.Implementations;
using ElderlyCareSupport.Server.Services.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using System.Data;
using System.Text;
using ElderlyCareSupport.Server.Contexts;
using ElderlyCareSupport.Server.Models;
using Microsoft.Extensions.Configuration.UserSecrets;


var builder = WebApplication.CreateBuilder(args);
var jwtSettings = builder.Configuration.GetSection("JWT");

builder.Services.AddCors();

builder.Host.UseSerilog((context, configuration) => { configuration.ReadFrom.Configuration(context.Configuration); });
// Add services to the container.

builder.Services.AddScoped(_ => new SqlConnection(builder.Configuration.GetConnectionString("ElderDB")));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IDbConnection>(db =>
    new SqlConnection(builder.Configuration.GetConnectionString("ElderDB")));

builder.Services.AddDbContext<ElderlyCareSupportContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("ElderDB"));
});

builder.Services.Configure<ApiBehaviorOptions>(options => { options.SuppressModelStateInvalidFilter = true; });
builder.Services.AddAutoMapper(typeof(MappingProfile));
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
            // Client Secret from Keycloak
        };
    });

builder.Services.AddAuthorization();
builder.Services.AddLogging();
// 
builder.Services.AddScoped<ILoginService, LoginService>();
builder.Services.AddScoped<IRegistrationService, RegistrationService>();
builder.Services.AddScoped<IFeeService, FeeService>();
builder.Services.AddScoped<IForgotPaswordService, ForgotPasswordService>();
builder.Services.AddScoped<ITokenService, JwtTokenGenerator>();
builder.Services.AddScoped<IUserProfileService<ElderUserDto>, ElderlyUserServices<ElderUserDto>>();
builder.Services.AddScoped<IUserProfileService<VolunteerUserDto>, VolunteerUserService<VolunteerUserDto>>();
builder.Services.AddScoped<IApiResponseFactoryService, ApiResponseFactory>();
builder.Services.AddScoped<IModelValidatorService, ModelValidatorHelper>();
builder.Services.AddScoped<IClock, ClockService>();
builder.Services.AddScoped<IEmailService, EmailHelper>();
//
builder.Services.AddScoped<IFeeRepository, FeeRepository>();
builder.Services.AddScoped<ILoginRepository, LoginRepository>();
builder.Services.AddScoped<IRegistrationRepository, RegistrationRepository>();
builder.Services.AddScoped<IForgotPasswordRepository, ForgotPasswordRepository>();
builder.Services.AddScoped<IUserRepository<ElderUserDto>, ElderlyUserRepository<ElderUserDto>>();
builder.Services.AddScoped<IUserRepository<VolunteerUserDto>, VolunteerUserRepository<VolunteerUserDto>>();
builder.Services.AddHttpClient();


var app = builder.Build();

app.UseCors(
    options => options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

app.UseDelta();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseSerilogRequestLogging();
app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.Run();