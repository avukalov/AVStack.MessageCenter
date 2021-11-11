namespace AVStack.MessageCenter.Common.Configuration
{
    public class EmailTemplatesOptions
    {
        public const string EmailTemplatesSection = "EmailTemplates";
        public string BasePath { get; set; }
        public string EmailConfirmation { get; set; }

    }
}