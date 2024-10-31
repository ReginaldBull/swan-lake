namespace MarktguruApi.Models.Authentication
{
    using System.ComponentModel;
    public class TokenGenerationDto
    {
        public string UserName { get; set; }
        [PasswordPropertyText]
        public string Password { get; set; }
    }
}

