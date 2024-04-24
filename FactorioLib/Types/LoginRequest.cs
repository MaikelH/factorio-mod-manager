namespace FactorioLib.Types
{
    public class LoginRequest
    {
        public required string Username { get; set; } // Required. Account username or e-mail.
        public required string Password { get; set; } // Required. Account password.
        public int ApiVersion { get; set; } = 4; // (Technically) not required. API responses will be different than described on this page when not set to 4. Currently 4.
        public bool? RequireGameOwnership { get; set; } // Not required. If set to 'true', will fail authentication if the user account hasn't actually purchased Factorio.
        public string? EmailAuthenticationCode { get; set; } // Not required. If a previous login failed with email-authentication-required, email authentication can be completed by including the code sent to the user via mail.
    }
}