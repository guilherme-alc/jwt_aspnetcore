using Flunt.Notifications;
using Flunt.Validations;

namespace Login.Core.Contexts.AccountContext.UseCases.Authenticate;

public static class AuthenticateValidation
{
    public static Contract<Notification> Assert(AuthenticateRequest request)
    {
        return new Contract<Notification>()
            .Requires()
            .IsNotNullOrEmpty(request.Password, "Password", "Senha não pode ser nulo ou vazio.")
            .IsNotNullOrEmpty(request.Email, "Email", "Email não pode ser nulo ou vazio.")
            .IsEmail(request.Email, "Email", "Email inválido.");
    }
}