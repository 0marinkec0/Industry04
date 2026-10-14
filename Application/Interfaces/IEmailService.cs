namespace Application.Interfaces;

public interface IEmailService
{
    Task SendConfirmationEmail(string email, string token);
}
