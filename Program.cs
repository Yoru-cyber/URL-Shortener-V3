using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using URL_Shortener.Auth;
using URL_Shortener.Context;
using URL_Shortener.Entities;
using URL_Shortener.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAuthorization();
builder.Services.AddAuthentication("Bearer").AddJwtBearer(options =>
{
    options.Audience = builder.Configuration["Jwt:Audience"];
    options.Authority = builder.Configuration["JWT:Authority"];
    options.RequireHttpsMetadata = false;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateAudience = true,
        ValidAudience = builder.Configuration["Jwt:Audience"],
        ValidateIssuer = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        RequireExpirationTime = false,
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:SecretKey"]))
    };
});
builder.Services.AddDbContextPool<ShortenedUrlContext>(opt =>
    opt.UseNpgsql(builder.Configuration.GetConnectionString("UrlShortenerContext")));
builder.Services.AddSingleton<TokenProvider>();
builder.Services.AddSingleton<AuthService>();
builder.Services.AddSingleton<PasswordHasher<User>>();
var devCors = "_devCors";
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: devCors,
        policy =>
        {
            policy.AllowAnyOrigin();
            policy.AllowAnyHeader();
            policy.AllowAnyMethod();
        });
});
var app = builder.Build();
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    var context = services.GetRequiredService<ShortenedUrlContext>();
    if (context.Database.GetPendingMigrations().Any()) context.Database.Migrate();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();
app.UseCors(devCors);
app.MapControllers();

app.Run();