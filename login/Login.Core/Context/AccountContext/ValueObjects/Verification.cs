using Login.Core.Context.AccountContext.Exceptions;
using Login.Core.Context.SharedContext.ValueObjects;

namespace Login.Core.Context.AccountContext.ValueObjects
{
    public class Verification : ValueObject
    {
        public Verification() { }
        public string Code { get; } = Guid.NewGuid().ToString("N").Substring(0, 6).ToUpper();
        public DateTime? ExpiresAt { get; private set; } = DateTime.UtcNow.AddMinutes(5);
        public DateTime? VerifiedAt { get; private set; } = null;

        public virtual void Verify(string code)
        {
            if(string.IsNullOrEmpty(code))
                throw new InvalidCodeException("O código de verificação não pode ser nulo ou vazio.");

            if(ExpiresAt < DateTime.UtcNow)
                throw new InvalidCodeException("Esse código já expirou.");
            
            if(!string.Equals(Code.Trim(), code.Trim(), StringComparison.CurrentCultureIgnoreCase))
                throw new InvalidCodeException("Código de verificação inválido.");

            VerifiedAt = DateTime.UtcNow;
            ExpiresAt = null;
        }
    }
}
