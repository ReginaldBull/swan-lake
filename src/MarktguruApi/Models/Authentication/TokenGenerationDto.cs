namespace MarktguruApi.Models.Authentication
{
    using System.ComponentModel;

    /// <summary>
    /// Data Transfer Object for token generation.
    /// </summary>
    public class TokenGenerationDto
    {
        /// <summary>
        /// Gets or sets the user name.
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// Gets or sets the password.
        /// </summary>
        [PasswordPropertyText]
        public string Password { get; set; }
    }
}