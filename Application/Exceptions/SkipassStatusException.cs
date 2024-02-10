namespace Application.Exceptions;

public sealed class SkipassStatusException : Exception
{
    public SkipassStatusException(string? message) : base(message)
    {
    }
}