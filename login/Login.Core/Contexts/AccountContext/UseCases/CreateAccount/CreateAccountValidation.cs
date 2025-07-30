using Flunt.Notifications;
using Flunt.Validations;

namespace Login.Core.Contexts.AccountContext.UseCases.CreateAccount
{
    public static class CreateAccountValidation
    {
        public static Contract<Notification> Assert(CreateAccountRequest request)
        {
            var contract = new Contract<Notification>()
                .Requires()
                .IsNotNullOrEmpty(request.Name, "Name", "Nome não pode ser nulo ou vazio.")
                .IsNotNullOrEmpty(request.Email, "Email", "Email não pode ser nulo ou vazio.")
                .IsEmail(request.Email, "Email", "Email inválido.");
            
            if (!string.IsNullOrWhiteSpace(request.Name))
            {
                contract
                    .IsLowerThan(request.Name.Length, 160, "Name", "Nome não pode ser maior que 160 caracteres.")
                    .IsGreaterThan(request.Name.Length, 3, "Name", "Nome deve ter pelo menos 3 caracteres.");
            }
                    
            if (!string.IsNullOrEmpty(request.Password))
            {
                contract
                    .IsLowerThan(request.Password.Length, 40, "Password", "Senha não pode ser maior que 40 caracteres.")
                    .IsGreaterThan(request.Password.Length, 8, "Password", "Senha deve ter pelo menos 8 caracteres.");
            }
            
            return contract;
        }
    }
}