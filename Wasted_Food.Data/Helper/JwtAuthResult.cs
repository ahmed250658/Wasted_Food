namespace Wasted_Food.Data.Helper
{
    public class JwtAuthResult
    {
        public string accessToken { get; set; }
        public RefreshToken refreshToken { get; set; }
    }
    public class RefreshToken
    {
        public string UserName { get; set; }
        public string Tokenstring { get; set; }
        public DateTime ExpireAt { get; set; }
    }
}
