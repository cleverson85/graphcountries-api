namespace Domain.Models
{
    public class AuthResponse
    {
        public bool IsAuthenticaded { get; set; }
        public string Token { get; set; }
        public bool TempUser { get; set; }
    }
}
