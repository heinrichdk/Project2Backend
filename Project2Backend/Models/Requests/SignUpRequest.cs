namespace Project2Backend.Models.Requests;

public class SignUpRequest
{
    public string Username { get; set; }
    public string Password { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
}