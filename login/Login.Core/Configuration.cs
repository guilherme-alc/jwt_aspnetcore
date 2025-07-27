namespace Login.Core
{
    public static class Configuration
    {
        public static SecretsConfiguration Secrets { get; set; } = new();
        public class SecretsConfiguration
        {
            public string PasswordSaltKey { get; set; } = string.Empty;
        }
    }
}
