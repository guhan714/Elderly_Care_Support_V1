using ElderlyCareSupport.Server.Common;
using ElderlyCareSupport.Server.Contexts;
using ElderlyCareSupport.Server.DTOs;
using ElderlyCareSupport.Server.Helpers;
using ElderlyCareSupport.Server.Repositories.Implementations;
using ElderlyCareSupport.Server.Repositories.Interfaces;
using ElderlyCareSupport.Server.Services.Implementations;
using ElderlyCareSupport.Server.Services.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((context, configuration) =>
{
    configuration.ReadFrom.Configuration(context.Configuration);
});
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ElderlyCareSupportContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("ElderDB"));
});

builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.SuppressModelStateInvalidFilter = true;
});
builder.Services.AddAutoMapper(typeof(MappingProfile));
builder.Services.AddAuthentication(option => { option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    option.RequireAuthenticatedSignIn= true;
})
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = "https://localhost:44313",
            ValidAudience = "https://localhost:44313",
            IssuerSigningKey = new SymmetricSecurityKey("WEFOHOBCWEOIB2399012730123NIONCOANSDCQWEDJOQOIAHGCAOIUBCIOOPWQIE"u8.ToArray())
        };
    });
builder.Services.AddAuthorization();
builder.Services.AddLogging();


// 
builder.Services.AddScoped<ILoginService, LoginService>();
builder.Services.AddScoped<IRegistrationService, RegistrationService>();
builder.Services.AddScoped<IFeeService, FeeService>();
builder.Services.AddScoped<IForgotPaswordService, ForgotPasswordService>();
builder.Services.AddScoped<ITokenService, TokenGenerator>();
builder.Services.AddScoped<IUserProfileService<ElderUserDto>, ElderlyUserServices<ElderUserDto>>();
builder.Services.AddScoped<IApiResponseFactoryService, ApiResponseFactory>();
builder.Services.AddScoped<IModelValidatorService, ModelValidatorHelper>();
builder.Services.AddScoped<IClock, ClockService>();
//
builder.Services.AddScoped<IFeeRepository, FeeRepository>();   
builder.Services.AddScoped<ILoginRepository, LoginRepository>();   
builder.Services.AddScoped<IRegistrationRepository, RegistrationRepository>();   
builder.Services.AddScoped<IForgotPasswordRepository, ForgotPasswordRepository>();   
builder.Services.AddScoped<IUserRepository<ElderUserDto>, ElderlyUserRepository<ElderUserDto>>();   
builder.Services.AddScoped<IUserRepository<VolunteerUserDto>, VolunteerUserRepository<VolunteerUserDto>>();   

var app = builder.Build();

// Configure the HTTP request pipeline.
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
