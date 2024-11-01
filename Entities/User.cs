using System.ComponentModel.DataAnnotations.Schema;

namespace URL_Shortener.Entities;

[Table("users")]
public class User
{
    [Column("id")] public int Id { get; set; }

    [Column("username")] public string username { get; set; }

    [Column("password")] public string password { get; set; }
    [Column("email")] public string email { get; set; }
}