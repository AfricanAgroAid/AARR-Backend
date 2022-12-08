using Gateway.Extensions;
using IOC.Extensions;


var builder = WebApplication.CreateBuilder(args);
builder.Services.AddCors(c => c.AddPolicy("AARRPolicy", app =>
{
    app.AllowAnyHeader();
    app.AllowAnyMethod();
    app.AllowAnyOrigin();
}));
// Add services to the container.
builder.Services.AddIOCService(builder.Configuration);
builder.Services.AddControllers();
builder.Services.AddGateway();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseCors("AARRPolicy");
 
app.MapControllers();

app.Run();
