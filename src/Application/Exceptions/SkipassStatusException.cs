namespace SkiiResort.Application.Exceptions;

public sealed class SkipassStatusException : Exception
{
    public SkipassStatusException(string? message = default) : base(message)
    {
    }
}
