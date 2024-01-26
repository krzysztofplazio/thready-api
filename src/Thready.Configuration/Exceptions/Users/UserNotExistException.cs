using System.Runtime.Serialization;

namespace Thready.API.Exceptions.Users;

public class UserNotExistException : UserException
{
    public UserNotExistException()
    {
    }

    public UserNotExistException(string? message) : base(message)
    {
    }

    public UserNotExistException(string? message, Exception? innerException) : base(message, innerException)
    {
    }

    protected UserNotExistException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }

}