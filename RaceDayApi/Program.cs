using Microsoft.EntityFrameworkCore;
using RaceDayApi.Data;
using RaceDayApi.Middlewares;

var builder = WebApplication.CreateBuilder(args);

// Configure In-Memory Database
builder.Services.AddDbContext<RaceDayDbContext>(options =>
    options.UseInMemoryDatabase("RaceDayDb"));

// Configure Session Management
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

// Standard SwaggerGen setup (no Microsoft.OpenApi reference required)
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Seed initial database records
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<RaceDayDbContext>();
    db.Database.EnsureCreated();
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseRouting();
app.UseSession();
app.UseMiddleware<SessionAuthMiddleware>();

app.MapControllers();

app.Run();

public partial class Program { }