using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Project2Backend.Models;

[Table("User")]
public class User:DataModel
{

    public User()
    {
        UserImages = new Collection<UserImage>();
    }
    
    public string Username { get; set; }
    [JsonIgnore]public string Password { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    [JsonIgnore]public string Salt { get; set; }
    public Collection<UserImage> UserImages { get; set; }
}