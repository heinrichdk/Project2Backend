namespace Project2Backend.Models.Requests;

public class ShareImageRequest
{
    public  string Username { get; set; }
    public  Guid ImageId { get; set; }
}