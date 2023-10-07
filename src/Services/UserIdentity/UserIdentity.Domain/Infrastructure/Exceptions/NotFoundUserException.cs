namespace UserIdentity.Domain.Infrastructure.Exceptions;

public class NotFoundUserException : Exception
{
    public NotFoundUserException() : base("The user was not found.")
    {
    }
}