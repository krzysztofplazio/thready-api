using Thready.Application.Exceptions.Roles;

namespace Thready.Application.Utils;

public static class EnumUtils
{
    public static T ToEnum<T>(this string value)
    {
        if (!Enum.TryParse(typeof(T), value, true, out object? result))
        {
            throw new RoleNotExistException();
        }

        return (T)result;
    }
}
