using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project2Backend.Models;
[Table("Image")]
public class Image:DataModel
{
    public Image()
    {
        UserImages = new Collection<UserImage>();
    }
    
    public string Name { get; set; }
    public DateTimeOffset DateCaptured { get; set; }
    public string CapturedBy { get; set; }
    public string Tags { get; set; }
    public  Collection<UserImage> UserImages { get; set; }
}