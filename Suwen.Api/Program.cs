using Application;
using Application.Mappers;
using Domain.Constants;
using Infrastructure;
using Microsoft.EntityFrameworkCore;
using Persistence.Context;
using Suwen.Persistence;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddApplicationServices(builder.Configuration);
builder.Services.AddInfrastructureService(builder.Configuration);
builder.Services.AddPersistenceServices(builder.Configuration);

// Database Context
builder.Services.AddDbContext<SuwenDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});


builder.Services.Configure<JwtOptions>(builder.Configuration.GetSection("JwtOptions"));
builder.Services.AddAutoMapper(typeof(ProductMap).Assembly);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Middleware Pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication(); // Önce Authentication
app.UseAuthorization();  // Sonra Authorization
app.MapControllers();

app.Run();