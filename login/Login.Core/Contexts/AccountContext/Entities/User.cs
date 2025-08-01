using Login.Core.Contexts.AccountContext.Exceptions;
using Login.Core.Contexts.AccountContext.ValueObjects;
using Login.Core.Contexts.SharedContext.Entities;

namespace Login.Core.Contexts.AccountContext.Entities
{
    public class User : Entity
    {
        protected User() { } // Para o EF Core
        public User(string email, Password password, string name)
        {
            Email = email;
            Password = password;
            Name = name;
        }

        public string Name { get; set; } = string.Empty;
        public Email Email { get; private set; } = null!;
        public Password Password { get; private set; } = null!;
        public string Image { get; set; } = string.Empty;
        public ICollection<Role> Roles { get; set; } = new List<Role>();
        
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

            Email = new Email(email);
        }

        public void ChangePassword(string oldPassword, string newPassword)
        {
            if (string.IsNullOrEmpty(oldPassword) || string.IsNullOrEmpty(newPassword))
                throw new InvalidPasswordException("As senhas não podem ser nulas ou vazias.");
            if (!Password.Challenge(oldPassword))
                throw new InvalidPasswordException("Senha antiga inválida.");
            Password = new Password(newPassword);
        }

        public void UpdateName(string name)
        {
            if(string.IsNullOrEmpty(name))
                throw new InvalidNameException("O nome não pode ser nulo ou vazio.");
            Name = name;
        }
    }
}
