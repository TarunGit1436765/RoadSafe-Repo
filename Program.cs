using Microsoft.EntityFrameworkCore;
using RoadSafe.API.Models;
using RoadSafe.API.Repositories.Interfaces;
using RoadSafe.API.Repositories.Implementations;
using RoadSafe.API.Services.Interfaces;
using RoadSafe.API.Services.Implementations;
using RoadSafe.TimingPlanModule.Repositories;
using RoadSafe.TimingPlanModule.Services;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// --- MYSQL CONNECTION SETUP ---
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<RoadSafeDbContext>(options =>
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));
// -----------------------------------

// =====================================================================
// --- NEW: DEPENDENCY INJECTION REGISTRATIONS ---
// This tells .NET how to find your custom Services and Repositories!
builder.Services.AddScoped<IAssetRegistryRepository, AssetRegistryRepository>();
builder.Services.AddScoped<IAssetRegistryService, AssetRegistryService>();
builder.Services.AddScoped<ITimingPlanRepository, TimingPlanRepository>();
builder.Services.AddScoped<ITimingPlanService, TimingPlanService>();
builder.Services.AddScoped<ITrafficLogicService, TrafficLogicService>();

builder.Services.AddCors(options => {
    options.AddPolicy("AllowAngularApp",
        policy => policy.WithOrigins("http://localhost:4200").AllowAnyHeader().AllowAnyMethod());
});

var app = builder.Build();

if (app.Environment.IsDevelopment()) {
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("AllowAngularApp");
app.UseAuthorization();
app.MapControllers();
app.Run();