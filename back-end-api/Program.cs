using back_end_api.Context;
using back_end_api.ControlCenter;
using back_end_api.Services;
using back_end_api.Services.Simulation;
using back_end_api.Services.Simulation.Wrokers;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<FlightsDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DbConnection"));
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("policy", builder =>
    {
        builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
    });
});

builder.Services.AddHostedService<BackgroundSimulator>();
builder.Services.AddHostedService<BackgroundFlightMaker>();

builder.Services.AddTransient<IFlightMaker, FlightMaker>();
builder.Services.AddTransient<IFlightManager, FlightManager>();
builder.Services.AddScoped<IControlCenter, ControlCenter>();

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

app.UseCors("policy");

app.Run();
