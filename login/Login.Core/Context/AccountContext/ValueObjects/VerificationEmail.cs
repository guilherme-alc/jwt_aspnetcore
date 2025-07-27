using Login.Core.Context.AccountContext.Exceptions;

namespace Login.Core.Context.AccountContext.ValueObjects
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
