namespace Login.Core.Contexts.AccountContext.Exceptions
{
    public class InvalidCodeException : Exception
    {
        public InvalidCodeException(string message) : base(message)
        {
        }
        public InvalidCodeException() : base("Código inválido.")
        {
        }
        public InvalidCodeException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
