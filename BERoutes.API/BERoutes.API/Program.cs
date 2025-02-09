using BERoutes.API.Data;
using BERoutes.API.Repositories.Implementations;
using BERoutes.API.Repositories.Interfaces;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//builder.Services.
//    AddFluentValidation(option => option.RegisterValidatorsFromAssemblyContaining<Program>());

builder.Services
    .AddFluentValidationAutoValidation()
    .AddFluentValidationClientsideAdapters()
    .AddValidatorsFromAssemblyContaining<Program>();


builder.Services.AddDbContext<BERoutesDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("BERoutesConnection"));
});

builder.Services.AddScoped<IRegionRepository, RegionRepository>();
builder.Services.AddScoped<IActivityRouteRepository, ActivityRouteRepository>();
builder.Services.AddScoped<IRouteDifficultyRepository, RouteDifficultyRepository>();

builder.Services.AddAutoMapper(typeof(Program).Assembly);

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
