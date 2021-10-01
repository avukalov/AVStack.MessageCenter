namespace AVStack.MessageCenter.Models.Interfaces
{
    public interface IUserRegistrationEmailModel : IEmailModel
    {
        string FullName { get; set; }
        string EmailAddress { get; set; }
        string ConfirmationLink { get; set; }
    }
}