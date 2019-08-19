using EventsExpress.Core.DTOs;
using System.Threading.Tasks;

namespace EventsExpress.Core.IServices
{
    public interface IEmailService
    {
        Task SendEmailAsync(EmailDTO emailDTO);
    }
}
