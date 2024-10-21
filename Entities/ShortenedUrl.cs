using System.ComponentModel.DataAnnotations.Schema;

namespace URL_Shortener.Entities;

[Table("ShortenedUrls")]
public class ShortenedUrl
{
    [Column("id")] public int Id { get; set; }

    [Column("originalUrl")] public string originalUrl { get; set; }

    [Column("shortenedUrl")] public string shortenedUrl { get; set; }
}