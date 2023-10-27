using System.Runtime.Serialization;

namespace Thready.API.Exceptions.Users
{
    public class UserHasNoIdentityException : UserException
    {
        public UserHasNoIdentityException()
        {
        }

        public UserHasNoIdentityException(string? message) : base(message)
        {
        }

        public UserHasNoIdentityException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected UserHasNoIdentityException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}