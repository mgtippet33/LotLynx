namespace UserIdentity.Domain.Infrastructure.Exceptions;

public class NotFoundValidatorException : Exception
{
    public NotFoundValidatorException() : base("The requested validator was not found. Please check your configuration or input.")
    {
    }
}