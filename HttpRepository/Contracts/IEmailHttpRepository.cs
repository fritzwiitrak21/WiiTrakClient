using WiiTrakClient.DTOs;

namespace WiiTrakClient.HttpRepository.Contracts
{
    public interface IEmailHttpRepository
    {
        Task SendMailAsync(MailRequest request);
       
    }
}
