namespace Login.Core.Contexts.SharedContext.Extensions
{
    public static class StringExtension
    {
        public static string ToBase64(this string value)
        {
            if (string.IsNullOrEmpty(value))
                return string.Empty;

            var bytes = System.Text.Encoding.UTF8.GetBytes(value);
            return Convert.ToBase64String(bytes);
        }
    }
}
