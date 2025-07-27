using System.Runtime.Serialization;

namespace Login.Core.Contexts.AccountContext.Exceptions
{
    public class InvalidNameException : Exception
    {
        public InvalidNameException()
        {
        }

        public InvalidNameException(string? message) : base(message)
        {
        }

        public InvalidNameException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}
