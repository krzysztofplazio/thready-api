using System.Runtime.Serialization;

namespace Thready.API.Exceptions.Users;

public class BadUsernameOrPasswordException : UserException
{
    public BadUsernameOrPasswordException()
    {
    }

    public BadUsernameOrPasswordException(string? message) : base(message)
    {
    }

    public BadUsernameOrPasswordException(string? message, Exception? innerException) : base(message, innerException)
    {
    }

    protected BadUsernameOrPasswordException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }

}