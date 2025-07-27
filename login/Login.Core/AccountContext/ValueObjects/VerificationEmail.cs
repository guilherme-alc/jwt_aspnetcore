using Login.Core.AccountContext.Exceptions;

namespace Login.Core.AccountContext.ValueObjects
{
    public class VerificationEmail : Verification
    {
        public VerificationEmail() { }
        public bool IsActive => VerifiedAt != null && ExpiresAt == null;
        public override void Verify(string code)
        {
            if (IsActive)
                throw new InvalidCodeException("Esse item já foi ativado.");
            base.Verify(code);
        }
    }
}
