using Thready.API.Constants;
using Thready.API.Exceptions.Users;

namespace Thready.API.ProblemDetails.Users;

public class UserExceptionDetails : Microsoft.AspNetCore.Mvc.ProblemDetails
{
    public UserExceptionDetails(UserException exception)
    {
        Status = StatusCodes.Status400BadRequest;
        Title = "User Problem.";
        Detail = exception.Message;
        Type = "http://api.thready.com/users/problem";
        switch (exception.ErrorCode)
        {
            case UserExceptionErrorCodes.UserNotExist:
                Type += "/user-not-exist";
                Status = StatusCodes.Status404NotFound;
                break;
            case UserExceptionErrorCodes.BadUsernameOrPassword:
                Type += "/bad-username-or-password";
                Status = StatusCodes.Status404NotFound;
                break;
        }
    }
}