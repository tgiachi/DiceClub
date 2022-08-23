

using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using DiceClub.Database.Dao;
using DiceClub.Database.Entities.Account;
using DiceClub.Services.Data.Rest;
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

        private async Task<LoginResponseData> GenerateToken(DiceClubUser user)
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

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                _configuration["Jwt:Issuer"],
                _configuration["Jwt:Audience"],
                claims,
                expires: DateTime.UtcNow.AddMinutes(100),
                signingCredentials: signIn);

            var refreshToken = GenerateRefreshToken();

            await _diceClubUserDao.UpdateRefreshToken(user.Email, refreshToken);

            return new LoginResponseData
            { AccessToken = new JwtSecurityTokenHandler().WriteToken(token), RefreshToken = refreshToken };
        }


        [HttpPost]
        [Route("auth")]
        public async Task<IActionResult> Login([FromBody] LoginRequestData requestData)
        {

            if (requestData != null && requestData.Email != null && requestData.Password != null)
            {
                var user = await _diceClubUserDao.Authenticate(requestData.Email, requestData.Password);

                if (user != null)
                {

                    var token = await GenerateToken(user);

                    return Ok(token);
                }

                return BadRequest("Invalid credentials");
            }

            return BadRequest();
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("refresh_token")]
        public async Task<IActionResult> RefreshToken([FromBody] LoginResponseData tokenModel)
        {
            if (tokenModel is null)
            {
                return BadRequest("Invalid client request");
            }

            var user = await _diceClubUserDao.FindUserByRefreshToken(tokenModel.RefreshToken);
            if (user == null)
            {
                return BadRequest("Invalid client request");
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
