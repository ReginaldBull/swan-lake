namespace ProductApi.Models.Auth
{
    using System.ComponentModel;
    public class TokenGenerationDto
    {
        public string UserName { get; set; }
        [PasswordPropertyText]
        public string Password { get; set; }
    }
}

