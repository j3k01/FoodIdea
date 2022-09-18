using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace FoodIdea.API.Controllers
{
    [Route("api/authentication")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {

        public class AuthenticationRequestBody
        {
            public string? Username { get; set; }
            public string? Password { get; set; }
        }

        private class FoodIdeaUserInfo
        {
            public int UserId { get; set; }
            public string UserName { get; set; }
            public string UserSurname { get; set; }
            public int UserAge { get; set; }

            public FoodIdeaUserInfo(int userid, string username, string usersurname, int userage)
            {
                UserId = userid;
                UserName = username;
                UserSurname = usersurname;
                UserAge = userage;
            }
        }
        private readonly IConfiguration _configuration;
        public AuthenticationController(IConfiguration configuration)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }

        [HttpPost("authenticate")]
        public ActionResult<string> Authenticate(AuthenticationRequestBody authenticationRequestBody)
        {
            // username and password validation
            var user = ValidateUserCredentials(authenticationRequestBody.Username, authenticationRequestBody.Password);

            if(user == null)
            {
                return Unauthorized();
            }
            // create a token
            var tokenKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_configuration["Authentication:SecretForKey"]));
            var signingCredentials = new SigningCredentials(tokenKey, SecurityAlgorithms.HmacSha256);

            // claims that

            var claimsForToken = new List<Claim>();
            claimsForToken.Add(new Claim("sub", user.UserId.ToString()));
            claimsForToken.Add(new Claim("name", user.UserName));
            claimsForToken.Add(new Claim("surname", user.UserSurname));
            claimsForToken.Add(new Claim("age", user.UserAge.ToString()));

            var jwtSecurityToken = new JwtSecurityToken(
                _configuration["Authentication:Issuer"],
                _configuration["Authentication:Audience"],
                claimsForToken,
                DateTime.UtcNow,
                DateTime.UtcNow.AddHours(1),
                signingCredentials);

            var tokenToReturn = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);

            return Ok(tokenToReturn);
        }

        private FoodIdeaUserInfo ValidateUserCredentials(string? username, string? password)
        {
            //UserDB in progress, I'll change it later :)
            //
            //
            return new FoodIdeaUserInfo(1, "Mike", "King", 18);
        }
    }
}
