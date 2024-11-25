namespace SkiiResort.Application.Exceptions;

public class LoginException : Exception
{
    public LoginException(string? message = default) : base(message)
    {
    }
}
