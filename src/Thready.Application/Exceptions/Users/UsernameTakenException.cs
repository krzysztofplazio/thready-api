using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Thready.Application.Exceptions.Users;

public class UsernameTakenException : UserException
{
    public UsernameTakenException()
    {
    }

    public UsernameTakenException(string? message) : base(message)
    {
    }

    public UsernameTakenException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}
