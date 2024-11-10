using Microsoft.EntityFrameworkCore;
using System.Reflection;
using WorkdayCalendar.DomainLayer.Entities;
using WorkdayCalendar.InfrastructureLayer;
using WorkdayCalendar.ServiceLayer.Middlewares;
using WorkdayCalendar.ServiceLayer.Startup;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.EnableAnnotations();
});

builder.Services.AddDbContext<WorkdayCalendarContext>(options =>
        options.UseSqlite(builder.Configuration.GetConnectionString("WorkdayCalendarDatabase")));

Services.RegisterServices(builder.Services);
builder.Services.Configure<WorkdaySetting>(builder.Configuration.GetSection("WorkdaySettings"));

builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ExceptionMiddleware>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
