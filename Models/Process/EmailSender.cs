using Microsoft.AspNetCore.Identity.UI.Services;
using System.Threading.Tasks;
namespace MapDirections.Models.Process
{
    public class EmailSender : IEmailSender
    {
        public Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            // Cấu hình gửi email ở đây, ví dụ dùng SMTP hoặc các dịch vụ gửi email khác
            // Ở đây chỉ đơn giản là giả lập việc gửi email.

            Console.WriteLine($"Sending Email to: {email}, Subject: {subject}, Message: {htmlMessage}");
            return Task.CompletedTask;
        }
    }

}
