using Microsoft.EntityFrameworkCore;
using RoadSafe.API.Models; 
using RoadSafe.API.Repositories.Interfaces;
using RoadSafe.API.Repositories.Implementations;
using RoadSafe.API.Services.Interfaces;
using RoadSafe.API.Services.Implementations;
using RoadSafe.API.DTOs; // Important: This allows the app to recognize your DTO types globally

var builder = WebApplication.CreateBuilder(args);

// 1. Add Controllers and Swagger
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// 2. Database Connection
// Ensure "DefaultConnection" in appsettings.json matches your MySQL/SQL Server credentials
builder.Services.AddDbContext<RoadSafeDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// 3. Dependency Injection (The "Engine" of your app)
// This tells .NET: "Whenever a Controller asks for IAssetRegistryService, give it AssetRegistryService"
builder.Services.AddScoped<IAssetRegistryRepository, AssetRegistryRepository>();
builder.Services.AddScoped<IAssetRegistryService, AssetRegistryService>();

// 4. CORS Policy (Allows your Angular frontend at port 4200 to talk to this API)
builder.Services.AddCors(options => {
    options.AddPolicy("AllowAngularApp",
        policy => policy.WithOrigins("http://localhost:4200")
                        .AllowAnyHeader()
                        .AllowAnyMethod());
});

var app = builder.Build();

// 5. Configure the HTTP request pipeline
if (app.Environment.IsDevelopment()) {
    app.UseSwagger();
    app.UseSwaggerUI();
}

// 6. Static Files 
// This is required so your Angular app can load the images/docs you upload to wwwroot
app.UseStaticFiles(); 

// app.UseHttpsRedirection(); // Optional: Comment this out if you have certificate issues locally
app.UseCors("AllowAngularApp");

app.UseAuthorization();

app.MapControllers();

app.Run();