using System.Runtime.Serialization;

namespace Thready.Application.Exceptions.Users;

[Serializable]
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
}