namespace CleanArchitecture.Domain.Exceptions
{
    public class BadRequestObjectException : Exception
    {
        public BadRequestObjectException(string? message) : base(message)
        {
        }
    }
}
