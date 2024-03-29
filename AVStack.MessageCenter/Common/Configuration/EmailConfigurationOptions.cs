namespace AVStack.MessageCenter.Common.Configuration
{
    public class EmailConfigurationOptions
    {
        public const string EmailConfigurationSection = "EmailConfiguration";

        public string SmtpServer { get; set; }

        public int Port { get; set; }

        public bool SslEnabled { get; set; }
        public string From { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }
    }
}