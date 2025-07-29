using Login.Core.Contexts.AccountContext.Exceptions;
using Login.Core.Contexts.SharedContext.Extensions;
using Login.Core.Contexts.SharedContext.ValueObjects;
using System.Text.RegularExpressions;

namespace Login.Core.Contexts.AccountContext.ValueObjects
{
    public partial class Email : ValueObject
    {
        private const string _pattern = @"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$";

        protected Email() { }

        public Email(string adress)
        {
            if(string.IsNullOrEmpty(adress))
                throw new InvalidEmailException("E-mail não pode ser nulo ou vazio.");

            Address = adress.Trim().ToLower();

            if (Address.Length < 5)
                throw new InvalidEmailException("E-mail deve ter pelo menos 5 caracteres.");
            if(!EmailRegex().IsMatch(Address))
                throw new InvalidEmailException("E-mail inválido.");
        }

        public string Address { get; }
        public string Hash => Address.ToBase64();
        public VerificationEmail Verification { get; private set; } = new VerificationEmail();

        public void ResendVerification()
        {
            if (Verification.IsActive)
                throw new InvalidEmailException("Esse e-mail já foi verificado.");
            Verification = new VerificationEmail();
        }

        // source generator para regex
        [GeneratedRegex(_pattern)]
        private static partial Regex EmailRegex();

        public override string ToString()
        {
            return Address;
        }

        public static implicit operator string(Email adress)
        {
            return adress.ToString();
        }
        public static implicit operator Email(string adress)
        {
            return new Email(adress);
        }
    }
}
