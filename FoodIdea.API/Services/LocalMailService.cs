namespace FoodIdea.API.Services
{
    public class LocalMailService : IMailService
    {
        private readonly string MailTo;
        private readonly string MailFrom;

        public LocalMailService(IConfiguration configuration)
        {
            MailTo = configuration["mailSettings:MailToAddress"];
            MailFrom = configuration["mailSettings:MailFromAddress"];
        }

        public void Send(string topic, string content)
        {
            Console.WriteLine($"Mail to {MailTo} from {MailFrom} with {nameof(LocalMailService)}");
            Console.WriteLine($"Topic: {topic}");
            Console.WriteLine($"Mail containt: {content}");
        }
    }
}
