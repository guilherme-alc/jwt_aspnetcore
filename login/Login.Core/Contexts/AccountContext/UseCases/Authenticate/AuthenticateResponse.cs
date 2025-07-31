using Flunt.Notifications;

namespace Login.Core.Contexts.AccountContext.UseCases.Authenticate;

public class AuthenticateResponse : SharedContext.UseCases.Response
{
    protected AuthenticateResponse()
    { }

    public AuthenticateResponse(string message, int status, IEnumerable<Notification>? notifications = null)
    {
        Message = message;
        Status = status;
        Notifications = notifications;
    }

    public AuthenticateResponse(string message, AuthenticatetResponseData responseData)
    {
        message = message;
        Status = 200;
        Notifications = null;
        Data = responseData;
    }

    public AuthenticatetResponseData? Data { get; set; }
}

public class AuthenticatetResponseData
{
    public string Token { get; set; } = string.Empty;
    public string Id { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string [] Roles { get; set; } = Array.Empty<string>();
};
