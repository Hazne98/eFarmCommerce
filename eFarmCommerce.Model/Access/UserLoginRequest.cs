namespace eFarmCommerce.Model.Access;

public class UserLoginRequest
{
    public string KorisnickoIme { get; set; } = null!;
    public string Lozinka { get; set; } = null!;
}