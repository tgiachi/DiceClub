using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using DiceClub.Api.Data.Rest;
using DiceClub.Database.Dao;
using DiceClub.Database.Dao.Account;
using DiceClub.Database.Entities.Account;
using DiceClub.Web.Data.Rest.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;

namespace DiceClub.Web.Controllers.Auth
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v1/[controller]")]
    public class LoginController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly DiceClubUserDao _diceClubUserDao;

        public LoginController(IConfiguration configuration, DiceClubUserDao diceClubUserDao)
        {
            _configuration = configuration;
            _diceClubUserDao = diceClubUserDao;
        }

        private async Task<RestResultObject<LoginResponseData>> GenerateToken(DiceClubUser user)
        {
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, _configuration["Jwt:Subject"]),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString(CultureInfo.InvariantCulture)),
                new Claim("user_id", user.Id.ToString()),
                new Claim("nickname", user.NickName),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim("serialId", user.SerialId),
                new Claim(JwtRegisteredClaimNames.Name, user.Name),
            }.ToList();

            foreach (var group in user.UserGroups)
            {
                claims.Add(new Claim(ClaimTypes.Role, group.Group.GroupName));
            }

            var expire = DateTime.UtcNow.AddDays(1);
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                _configuration["Jwt:Issuer"],
                _configuration["Jwt:Audience"],
                claims,
                expires: expire,
                signingCredentials: signIn);

            var refreshToken = GenerateRefreshToken();

            await _diceClubUserDao.UpdateRefreshToken(user.Email, refreshToken);

            return RestResultObjectBuilder<LoginResponseData>.Create().Data(new LoginResponseData
            {
                AccessToken = new JwtSecurityTokenHandler().WriteToken(token), RefreshToken = refreshToken,
                AccessTokenExpire = expire
            }).Build();
        }


        [HttpPost]
        [Route("auth")]
        public async Task<ActionResult<RestResultObject<LoginResponseData>>> Login(
            [FromBody] LoginRequestData requestData)
        {
            if (requestData != null && requestData.Email != null && requestData.Password != null)
            {
                var user = await _diceClubUserDao.Authenticate(requestData.Email, requestData.Password);

                if (user != null)
                {
                    var token = await GenerateToken(user);

                    return RestResultObjectBuilder<LoginResponseData>.Create().Data(token.Result).Build();
                }

                return BadRequest(RestResultObjectBuilder<LoginResponseData>.Create()
                    .Error(new Exception("Invalid login")).Build());
            }

            return BadRequest();
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("refresh_token")]
        public async Task<ActionResult<RestResultObject<LoginRequestData>>> RefreshToken(
            [FromBody] LoginResponseData tokenModel)
        {
            if (tokenModel is null)
            {
                return BadRequest("Invalid client request");
            }

            var user = await _diceClubUserDao.FindUserByRefreshToken(tokenModel.RefreshToken);
            if (user == null)
            {
                return BadRequest(RestResultObjectBuilder<LoginResponseData>.Create()
                    .Error(new Exception("Invalid user")).Build());
            }

            return Ok(GenerateToken(user));
        }


        private static string GenerateRefreshToken()
        {
            var randomNumber = new byte[64];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }
    }
}