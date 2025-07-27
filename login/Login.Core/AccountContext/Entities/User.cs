using Login.Core.AccountContext.Exceptions;
using Login.Core.AccountContext.ValueObjects;
using Login.Core.SharedContext.Entities;

namespace Login.Core.AccountContext.Entities
{
    public class User : Entity
    {
        protected User() { } // Para o EF Core
        public User(string email, string? password = null)
        {
            Email = email;
            Password = new Password(password);
        }

        public string? Name { get; set; } = string.Empty;
        public Email Email { get; private set; } = null!;
        public Password Password { get; private set; } = null!;
        public string Image { get; set; } = string.Empty;

        public void UpdatePassword(string plainTextPassword, string code)
        {
            if (string.IsNullOrEmpty(plainTextPassword))
                throw new InvalidPasswordException("A senha não pode ser nula ou vazia.");

            Password.ResetCode.Verify(code);

            Password = new Password(plainTextPassword);
        }

        public void UpdateEmail(string email, string code)
        {
            if (string.IsNullOrEmpty(email))
                throw new InvalidEmailException("O e-mail não pode ser nulo ou vazio.");

            Email = email;
        }

        public void ChangePassword(string oldPassword, string newPassword)
        {
            if (string.IsNullOrEmpty(oldPassword) || string.IsNullOrEmpty(newPassword))
                throw new InvalidPasswordException("As senhas não podem ser nulas ou vazias.");
            if (!Password.Challenge(oldPassword))
                throw new InvalidPasswordException("Senha antiga inválida.");
            Password = new Password(newPassword);
        }

        public void UpdateName(string? name = null)
        {
            Name = name;
        }
    }
}
