using System.Runtime.Serialization;

namespace Thready.API.Exceptions.Roles;

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

    protected RoleNotExistException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }

}