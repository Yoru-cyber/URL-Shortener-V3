using Microsoft.EntityFrameworkCore;
using URL_Shortener.Entities;

namespace URL_Shortener.Context;

public class ShortenedUrlContext(DbContextOptions<ShortenedUrlContext> options) : DbContext(options)
{
    public DbSet<ShortenedUrl> ShortenedUrls { get; set; }
    public DbSet<User> Users { get; set; }
}