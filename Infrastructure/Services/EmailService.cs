using Application.Interfaces;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Services;

public class EmailService : IEmailService
{
    private readonly ILogger<EmailService> _logger;

    public EmailService(ILogger<EmailService> logger)
    {
        _logger = logger;
    }

    public async Task SendConfirmationEmail(string email, string token)
    {
        var link = $"https://localhost:7051/confirm-email?email={email}&token={Uri.EscapeDataString(token)}";

        _logger.LogInformation(link);
    }
}
