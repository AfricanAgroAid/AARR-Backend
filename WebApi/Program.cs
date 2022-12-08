using Gateway.Extensions;
using IOC.Extensions;
using NLog.Web;
using NLog;
using WebApi.Controllers;


// var logger = NLog.LogManager.Setup().LoadConfigurationFromAppSettings().GetCurrentClassLogger();
// try
// {
// logger.Debug("init main");
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddCors(c => c.AddPolicy("AARRPolicy", app =>
{
    app.AllowAnyHeader();
    app.AllowAnyMethod();
    app.AllowAnyOrigin();
}));
// Add services to the container.
builder.Services.AddIOCService(builder.Configuration);
// NLog: Setup NLog for Dependency injection
// builder.Logging.ClearProviders();
// builder.Logging.SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Trace);
builder.Host.UseNLog();

//other classes that need the logger 
// builder.Services.AddTransient<FarmController>();
// builder.Services.AddTransient<FarmerController>();
// builder.Services.AddTransient<UtilityController>();
builder.Services.AddCors(cor => cor.AddPolicy("AARRCORS", builder =>
{
    builder.AllowAnyHeader();
    builder.AllowAnyMethod();
    builder.AllowAnyOrigin();
}));

builder.Services.AddControllers();
builder.Services.AddGateway();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
// if (app.Environment.IsDevelopment())
// {
//     app.UseSwagger();
//     app.UseSwaggerUI();
// }
 app.UseSwagger();
app.UseSwaggerUI();
app.UseCors("AARRCORS");
app.UseHttpsRedirection();

app.UseAuthorization();

app.UseCors("AARRPolicy");
 
app.MapControllers();

app.Run();
// }
// catch (Exception ex)
// {
//     // NLog: catch setup errors
//     logger.Error(ex, "Stopped program because of exception");
//     throw;
// }
// finally
// {
//     // Ensure to flush and stop internal timers/threads before application-exit (Avoid segmentation fault on Linux)
//     NLog.LogManager.Shutdown();
// }
