using DataTransfer.Api.Model;
using DataTransfer.Dal.Context;
using Microsoft.EntityFrameworkCore;
using DataTransfer.Business.Extensions;
using DataTransfer.Dal.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.LoadDalLayerExtension(builder.Configuration);
builder.Services.LoadBusinessLayerExtension();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.Configure<AppSettings>(builder.Configuration.GetSection("AppSettings"));

string defaultConnectionString = builder.Configuration["AppSettings:DomainConnectionString"];

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(defaultConnectionString));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
