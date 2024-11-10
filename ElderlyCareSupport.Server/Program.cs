using ElderlyCareSupport.Server.DataRepository;
using ElderlyCareSupport.Server.HelperInterface;
using ElderlyCareSupport.Server.Helpers;
using ElderlyCareSupport.Server.Interfaces;
using ElderlyCareSupport.Server.Models;
using Microsoft.EntityFrameworkCore;
using Serilog;

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
        options.UseSqlServer(builder.Configuration.GetConnectionString("ElderDB")));

builder.Services.AddMemoryCache();
builder.Services.AddScoped<IFeeRepository, FeeRepository>();   
builder.Services.AddScoped<ILoginRepository, LoginRepository>();   
builder.Services.AddScoped<IRegistrationRepository, RegistrationRepository>();   

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseSerilogRequestLogging();
app.UseHttpsRedirection();

app.UseAuthorization();
app.UseCors("AllowAllOrigins");
app.MapControllers();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.Run();
