using System.ComponentModel.DataAnnotations.Schema;

namespace Project2Backend.Models;

[Table("User")]
public class User:DataModel
{
    public string Username { get; set; }
    public string Password { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public string Salt { get; set; }
}