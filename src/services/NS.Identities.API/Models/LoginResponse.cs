namespace NS.Identities.API.Models
{
    public class LoginResponse
    {
        public string AccessToken { get; set; }

        public double ExpiresIn { get; set; }

        public UserToken UserToken { get; set; }
    }
}