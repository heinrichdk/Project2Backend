using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Project2Backend.Models;

[Table("User")]
public class User:DataModel
{
    public string Username { get; set; }
    public string Password { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    [JsonIgnore]public string Salt { get; set; }
}