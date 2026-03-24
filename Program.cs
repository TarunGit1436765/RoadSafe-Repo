using Microsoft.EntityFrameworkCore;
using RoadSafe.API.Models; 
// 1. ADD THESE USING STATEMENTS so Program.cs can find your new files
using RoadSafe.API.Repositories.Interfaces;
using RoadSafe.API.Repositories.Implementations;
using RoadSafe.API.Services.Interfaces;
using RoadSafe.API.Services.Implementations;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// This will now use the RoadSafeDbContext found in your Models folder
builder.Services.AddDbContext<RoadSafeDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// =====================================================================
// 2. REGISTER YOUR SERVICES & REPOSITORIES (Dependency Injection)
// =====================================================================
builder.Services.AddScoped<IAssetRegistryRepository, AssetRegistryRepository>();
builder.Services.AddScoped<IAssetRegistryService, AssetRegistryService>();

// Note: If you still have your User service from the earlier screenshot, 
// make sure to register those here too so they don't break!
// builder.Services.AddScoped<IUserRepository, UserRepository>();
// builder.Services.AddScoped<IUserService, UserService>();
// =====================================================================

builder.Services.AddCors(options => {
    options.AddPolicy("AllowAngularApp",
        policy => policy.WithOrigins("http://localhost:4200").AllowAnyHeader().AllowAnyMethod());
});

var app = builder.Build();

if (app.Environment.IsDevelopment()) {
    app.UseSwagger();
    app.UseSwaggerUI();
}

// =====================================================================
// 3. ENABLE STATIC FILES (Critical for your Document Upload feature)
// This allows Angular to actually view the files stored in wwwroot/uploads
// =====================================================================
app.UseStaticFiles(); 

// app.UseHttpsRedirection(); // Optional: Comment this out if you have certificate issues locally
app.UseCors("AllowAngularApp");
app.UseAuthorization();
app.MapControllers();

app.Run();