namespace Login.Core.AccountContext.Exceptions
{
    public class InvalidEmailException : Exception
    {
        public InvalidEmailException(string message) : base(message)
        {
        }
        public InvalidEmailException() : base("E-mail inválido.")
        {
        }
        public InvalidEmailException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
