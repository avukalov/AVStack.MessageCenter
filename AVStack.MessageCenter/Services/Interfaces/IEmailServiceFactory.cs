namespace AVStack.MessageCenter.Services.Interfaces
{
    public interface IEmailServiceFactory
    {
        T Create<T>() where T : class;
    }
}