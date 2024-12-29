using GvmHttpProxy.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace GvmHttpProxy.Service
{
    public class TokenService
    {
        private readonly IConfiguration _configuration;
        private readonly PasswordService _passwordService;
        private readonly GvmService _gvmService;

        public TokenService(IConfiguration configuration, PasswordService passwordService, GvmService gvmService)
        {
            _configuration = configuration;
            _passwordService = passwordService;
            _gvmService = gvmService;
        }

        public string GetToken(Credentials credentials)
        {
            var gvmResponse = _gvmService.ExecuteGvmCommand(credentials.Username!, credentials.Password!, "<get_version/>");

            if (string.IsNullOrEmpty(gvmResponse.Error))
            {
                var token = GenerateJwtToken(credentials.Username!);

                _passwordService.AddCredentials("Bearer " + token, credentials);

                return token;
            }

            throw new UnauthorizedAccessException("Wrong username or password");
        }

        private string GenerateJwtToken(string username)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Authorization:Key"]!));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                expires: DateTime.Now.AddMinutes(Convert.ToInt64(_configuration["Authorization:TokenExpirationInMinutes"]!)),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
