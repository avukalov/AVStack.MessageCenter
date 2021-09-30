namespace AVStack.MessageCenter.Models
{
    public class EmailConfirmationModel
    {
        public string Type { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string ConfirmationLink { get; set; }
    }
}