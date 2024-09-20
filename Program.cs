using GasStationAPI.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// เพิ่มบริการ DbContext
builder.Services.AddDbContext<GasStationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// เพิ่มบริการ Controllers
builder.Services.AddControllers();

// ตั้งค่า CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins",
        builder =>
        {
            builder.AllowAnyOrigin()
                   .AllowAnyMethod()
                   .AllowAnyHeader();
        });
});

// ตั้งค่า Authentication ด้วย JWT
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        var key = builder.Configuration["Jwt:Key"] 
            ?? throw new InvalidOperationException("JWT Key not found in configuration.");
        var issuer = builder.Configuration["Jwt:Issuer"] 
            ?? throw new InvalidOperationException("JWT Issuer not found in configuration.");
        var audience = builder.Configuration["Jwt:Audience"] 
            ?? throw new InvalidOperationException("JWT Audience not found in configuration.");

        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = issuer,
            ValidAudience = audience,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key))
        };
    });

// เพิ่มบริการสำหรับ Authorization
builder.Services.AddAuthorization();

// ลงทะเบียน AuthService
builder.Services.AddScoped<GasStationAPI.Services.AuthService>();

// ลงทะเบียน EmployeeService
builder.Services.AddScoped<GasStationAPI.Services.EmployeeService>();

var app = builder.Build();


var logger = app.Services.GetRequiredService<ILogger<Program>>();
logger.LogInformation("info: Microsoft.Hosting.Lifetime[14]");
logger.LogInformation("      Now listening on: http://localhost:3001");
logger.LogInformation("info: Microsoft.Hosting.Lifetime[0]");
logger.LogInformation("      Application started. Press Ctrl+C to shut down.");
logger.LogInformation("info: Microsoft.Hosting.Lifetime[0]");
logger.LogInformation("      Hosting environment: Development");
logger.LogInformation("info: Microsoft.Hosting.Lifetime[0]");
logger.LogInformation($"      Content root path: {Directory.GetCurrentDirectory()}");

// ใช้งาน CORS
app.UseCors("AllowAllOrigins");

// ใช้งาน Authentication และ Authorization
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

// ให้แอปพลิเคชันรันแสดงผลแค่ข้อความ log และไม่ฟังที่พอร์ตจริง
Console.WriteLine("Press Ctrl+C to exit...");
var done = new ManualResetEventSlim(false);
Console.CancelKeyPress += (sender, eventArgs) =>
{
    eventArgs.Cancel = true;
    done.Set();
};
done.Wait();
