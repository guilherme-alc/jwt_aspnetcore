using System.Security.Claims;

namespace JwtAspnet.Extensions
{
    public static class ClaimTypesExtension
    {
        public static int Id (this ClaimsPrincipal user)
        {
            try
            {
                var id = user.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
                return id != null ? int.Parse(id) : 0;
            } catch
            {
                return 0;
            }
        }

        public static string Name(this ClaimsPrincipal user)
        {
            try
            {
                var name = user.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value;
                return name != null ? name : string.Empty;
            }
            catch
            {
                return string.Empty;
            }
        }

        public static string Email(this ClaimsPrincipal user)
        {
            try
            {
                var email = user.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;
                return email != null ? email : string.Empty;
            }
            catch
            {
                return string.Empty;
            }
        }

        public static string GivenName(this ClaimsPrincipal user)
        {
            try
            {
                var givenName = user.Claims.FirstOrDefault(c => c.Type == ClaimTypes.GivenName)?.Value;
                return givenName != null ? givenName : string.Empty;
            }
            catch
            {
                return string.Empty;
            }
        }

        public static string Image(this ClaimsPrincipal user)
        {
            try
            {
                var image = user.Claims.FirstOrDefault(c => c.Type == "image")?.Value;
                return image != null ? image : string.Empty;
            }
            catch
            {
                return string.Empty;
            }
        }
    }
}
