using System.Threading.Tasks;
using EventsExpress.Core.DTOs;

namespace EventsExpress.Core.IServices
{
    public interface IEmailService
    {
        Task SendEmailAsync(EmailDto emailDto);
    }
}
