using Flunt.Notifications;
using Flunt.Validations;

namespace Login.Core.UseCases.CreateAccount
{
    public static class CreateAccountValidation
    {
        public static Contract<Notification> Assert(CreateAccountRequest request)
        {
            return new Contract<Notification>()
                .Requires()
                .IsNotNullOrEmpty(request.Name, "Name", "Nome não pode ser nulo ou vazio.")
                .IsLowerThan(request.Name.Length, 160, "Name", "Nome não pode ser maior que 160 caracteres.")
                .IsGreaterThan(request.Name.Length, 3, "Name", "Nome não pode ser maior que 3 caracteres.")
                .IsNotNullOrEmpty(request.Email, "Email", "Email não pode ser nulo ou vazio.")
                .IsEmail(request.Email, "Email", "Email inválido.")
                .IsNotNullOrEmpty(request.Password, "Password", "não pode ser nulo ou vazio.")
                .IsLowerThan(request.Password.Length, 40, "Password", "Senha não pode ser maior que 40 caracteres.")
                .IsGreaterThan(request.Password.Length, 8, "Password", "Senha não pode ser menor que 8 caracteres.");
        }
    }
}