using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Payment.Api.Controllers.Base;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Payment.Api.Controllers
{
    /// <summary>
    /// Authanticate Process
    /// </summary>
    [ApiVersion("1")]
    [Route("[controller]")]
    [ApiController]
    public class AuthenticateController : BaseController
    {
        IConfiguration _configuration;
        /// <summary>
        /// Authanticate Constructor
        /// </summary>
        /// <param name="configuration"></param>
        public AuthenticateController(IConfiguration configuration, ILogger<AuthenticateController> logger) : base(logger)
        {
            _configuration = configuration;
        }
        /// <summary>
        /// Get Api Token
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        [HttpPost(Name ="GetToken")]
        public string GetToken(string userName,string password)
        {
            _logger.Log(LogLevel.Information, "start",_logger.GetType());
            //TODO:User Service Yazılacak

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWTSettings:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(_configuration["JWTSettings:Issuer"],
              _configuration["JWTSettings:Audience"],
              null,
              expires: DateTime.Now.AddMinutes(120),
              signingCredentials: credentials);
            _logger.Log(LogLevel.Information, "end", _logger.GetType());
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
