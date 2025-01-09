using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using System.Text;
using ElderlyCareSupport.Application;
using ElderlyCareSupport.Application.Common;
using ElderlyCareSupport.Application.Contracts;
using ElderlyCareSupport.Application.Contracts.Login;
using ElderlyCareSupport.Application.DTOs;
using ElderlyCareSupport.Application.Helpers;
using ElderlyCareSupport.Application.IRepository;
using ElderlyCareSupport.Application.IService;
using ElderlyCareSupport.Application.Service;
using ElderlyCareSupport.Application.Validation.AuthenticationValidators;
using ElderlyCareSupport.Application.Validation.UserServiceValidator;
using ElderlyCareSupport.Domain.Models;
using ElderlyCareSupport.Infrastructure;
using ElderlyCareSupport.Infrastructure.Repository;
using FluentValidation;


var builder = WebApplication.CreateBuilder(args);
var jwtSettings = builder.Configuration.GetSection("JWT");

builder.Services.AddCors();

builder.Host.UseSerilog((context, configuration) => { configuration.ReadFrom.Configuration(context.Configuration); });
builder.Services.AddValidatorsFromAssemblyContaining<UserRegistrationValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<UserLoginValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<ForgotPasswordValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<UpdateUserDetailsValidator>();

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddValidatorsFromAssembly(typeof(Program).Assembly);

builder.Services.AddScoped<IDbConnectionFactory, DbConnectionFactory>(db =>
    new DbConnectionFactory(builder.Configuration.GetConnectionString("ElderDB")!));

// builder.Services.AddDbContext<ElderlyCareSupportContext>(options =>
// {
//     options.UseSqlServer(builder.Configuration.GetConnectionString("ElderDB"));
// });

builder.Services.Configure<ApiBehaviorOptions>(options => { options.SuppressModelStateInvalidFilter = true; });
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
builder.Services.AddScoped<IUserRepository<ElderCareAccount,ElderUserDto>, ElderlyUserRepository<ElderCareAccount,ElderUserDto>>();
builder.Services.AddScoped<IUserRepository<VolunteerAccount,VolunteerUserDto>, VolunteerUserRepository<VolunteerAccount,VolunteerUserDto>>();
builder.Services.AddHttpClient();


var app = builder.Build();

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

app.MapControllers();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.Run();