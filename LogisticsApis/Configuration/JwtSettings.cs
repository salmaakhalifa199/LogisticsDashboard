namespace LogisticsApis.Configuration
{
    public class JwtSettings
    {
        public string Secret { get; set; }
        public int TokenLifetimeInMinutes { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
    }
}
