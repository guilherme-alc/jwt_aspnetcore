using Login.Core;
using Login.Core.Contexts.AccountContext.Entities;
using Login.Core.Contexts.AccountContext.UseCases.CreateAccount.Contracts;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace Login.Infra.Contexts.AccounContext.UseCases.CreateAccount
{
    public class CreateAccountService : ICreateAccountService
    {
        public async Task SendVerificationEmailAsync(User user, CancellationToken cancellationToken)
        {
            var client = new SendGridClient(Configuration.SendGrid.ApiKey);
            var from = new EmailAddress(Configuration.Email.DefaultFromEmail, Configuration.Email.DefaultFromName);
            const string subject = "Verifique sua conta";
            var to = new EmailAddress(user.Email, user.Name);
            var content = $"Código {user.Email.Verification.Code}";
            var msg = MailHelper.CreateSingleEmail(from, to, subject, content, content);
            await client.SendEmailAsync(msg, cancellationToken);
        }
    }
}