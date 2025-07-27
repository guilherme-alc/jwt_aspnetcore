using System.Runtime.Serialization;

namespace Login.Core.Context.AccountContext.Exceptions
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
