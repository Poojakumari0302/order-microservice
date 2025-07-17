namespace Domain.Shared.Settings
{
    public class CacheSettings
    {
        public string KeyName { get; set; }
        public string Server { get; set; }
        public int CacheExpirationInMinutes { get; set; }
    }
}