using System.Runtime.Serialization;

namespace Thready.Application.Exceptions.Users;

[Serializable]
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
}