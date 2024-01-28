using System.Runtime.Serialization;

namespace Thready.Application.Exceptions.Roles;

[Serializable]
public class RoleNotExistException : Exception
{
    public RoleNotExistException()
    {
    }

    public RoleNotExistException(string? message) : base(message)
    {
    }

    public RoleNotExistException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}