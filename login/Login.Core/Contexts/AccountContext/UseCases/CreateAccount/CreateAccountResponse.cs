using Flunt.Notifications;
using Login.Core.Contexts.SharedContext.UseCases;

namespace Login.Core.Contexts.AccountContext.UseCases.CreateAccount
{
    public class CreateAccountResponse : Response
    {
        protected CreateAccountResponse()
        { }

        public CreateAccountResponse(string message, int status, IEnumerable<Notification>? notifications = null)
        {
            Message = message;
            Status = status;
            Notifications = notifications;
        }

        public CreateAccountResponse(string message, CreateAccountResponseData responseData)
        {
            message = message;
            Status = 200;
            Notifications = null;
            Data = responseData;
        }

        public CreateAccountResponseData? Data { get; set; }
    }

    public record CreateAccountResponseData(Guid Id, string Name, string Email); 
}