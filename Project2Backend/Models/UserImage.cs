using System.ComponentModel.DataAnnotations.Schema;

namespace Project2Backend.Models;

[Table("UserImage")]
public class UserImage:DataModel
{
    public Guid UserId { get; set; }
    public Guid ImageId { get; set; }
    public bool IsUploader { get; set; }
    public User User { get; set; }
    public  Image Image { get; set; }
}