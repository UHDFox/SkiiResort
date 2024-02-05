namespace Application.Exceptions;

public class SkipassStatusException : Exception
{
    public SkipassStatusException(string? message) : base(message)
    {
    }
}