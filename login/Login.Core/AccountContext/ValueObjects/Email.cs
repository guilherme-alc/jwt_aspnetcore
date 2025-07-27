using Login.Core.AccountContext.Exceptions;
using Login.Core.SharedContext.Extensions;
using System.Text.RegularExpressions;

namespace Login.Core.AccountContext.ValueObjects
{
    public partial class Email : SharedContext.ValueObjects.ValueObject
    {
        private const string _pattern = @"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$";

        protected Email() { }

        public Email(string adress)
        {
            if(string.IsNullOrEmpty(adress))
                throw new InvalidEmailException("E-mail não pode ser nulo ou vazio.");

            Adress = adress.Trim().ToLower();

            if (Adress.Length < 5)
                throw new InvalidEmailException("E-mail deve ter pelo menos 5 caracteres.");
            if(!EmailRegex().IsMatch(Adress))
                throw new InvalidEmailException("E-mail inválido.");
        }

        public string Adress { get; }
        public string Hash => Adress.ToBase64();
        public VerificationEmail Verification { get; private set; } = new VerificationEmail();

        public void ResendVerification()
        {
            if (Verification.IsActive)
                throw new InvalidEmailException("Esse e-mail já foi verificado.");
            Verification = new VerificationEmail();
        }

        [GeneratedRegex(_pattern)]
        private static partial Regex EmailRegex();

        public override string ToString()
        {
            return Adress;
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
