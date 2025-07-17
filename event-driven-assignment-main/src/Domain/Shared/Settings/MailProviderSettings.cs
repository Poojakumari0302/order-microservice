namespace Domain.Shared.Settings
{
    public class MailProviderSettings
    {
        public string Endpoint { get; set; }
        public string Key { get; set; }
        public string SenderEmail { get; set; }
        public string SenderName { get; set; }
    }
}