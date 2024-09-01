namespace CleanArchitecture.Application.Services.EmailsService
{
    public interface IEmailsService
    {
        public Task SendEmail(string email, string Message, string? reason);
    }
}
