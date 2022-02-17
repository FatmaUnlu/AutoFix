using AutoFix.Models;
using System.Threading.Tasks;

namespace AutoFix.Services
{
    public interface IEmailSender
    {
        Task SendAsyc(EmailMessage message);


    }
}
