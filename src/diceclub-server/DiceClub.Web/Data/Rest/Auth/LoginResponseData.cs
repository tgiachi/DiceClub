namespace DiceClub.Web.Data.Rest.Auth
{
    public class LoginResponseData
    {
        public string AccessToken { get; set; } = null!;

        public string RefreshToken { get; set; } = null!;

        public DateTime AccessTokenExpire { get; set; }
    }
}
