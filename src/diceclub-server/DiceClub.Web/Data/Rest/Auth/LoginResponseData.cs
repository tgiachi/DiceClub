namespace DiceClub.Web.Data.Rest.Auth
{
    public class LoginResponseData
    {
        public string AccessToken { get; set; }

        public string RefreshToken { get; set; }

        public DateTime AccessTokenExpire { get; set; }
    }
}
