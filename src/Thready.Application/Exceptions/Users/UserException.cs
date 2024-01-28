using System.Runtime.Serialization;

namespace Thready.Application.Exceptions.Users;

[Serializable]
public class UserException : Exception
{
    public string? ErrorCode { get; set; }
    public UserException()
    {
    }

    public UserException(string? message) : base(message)
    {
        ErrorCode = message;
    }

    public UserException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}