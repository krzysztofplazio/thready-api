using Microsoft.AspNetCore.Http;
using Thready.Application.Constants;
using Thready.Application.Exceptions.Users;

namespace Thready.Application.ProblemDetails.Users;

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
                break;
            case UserExceptionErrorCodes.UserHasNoIdentity:
                Type += "/user-has-no-identity";
                Status = StatusCodes.Status404NotFound;
                break;
            case UserExceptionErrorCodes.UsernameTaken:
                Type += "/username-taken";
                Status = StatusCodes.Status422UnprocessableEntity;
                break;
        }
    }
}