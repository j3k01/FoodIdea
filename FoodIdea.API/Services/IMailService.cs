namespace FoodIdea.API.Services
{
    public interface IMailService
    {
        void Send(string topic, string content);
    }
}