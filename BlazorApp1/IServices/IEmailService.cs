namespace BlazorApp1.IServices
{
    public interface IEmailService
    {
        void SendEmail(EmailDto request);

    }
}
