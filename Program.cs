using Microsoft.EntityFrameworkCore;
using URL_Shortener.Context;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContextPool<ShortenedUrlContext>(opt =>
    opt.UseNpgsql(builder.Configuration.GetConnectionString("UrlShortenerContext")));
var  devCors = "_devCors";
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: devCors,
        policy  =>
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