using Core.Entity.Concrete;
using Core.Extensions;
using Core.Utilities.Security.Encyption;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace Core.Utilities.Security.Jwt
{
    public class JwtHelper : ITokenHelper
    {
        public IConfiguration Configuration { get;}

        private TokenOptions _tokenOptions;

        private DateTime _accessTokenExpirations;

        public JwtHelper(IConfiguration configuration) { 
            Configuration = configuration;
            _tokenOptions = new TokenOptions
            {
                Audience = configuration.GetSection("TokenOptions:Audience").Value!,
                Issuer = configuration.GetSection("TokenOptions:Issuer").Value!,
                AccessTokenExpiration = int.Parse(configuration.GetSection("TokenOptions:AccessTokenExpiration").Value!),
                SecurityKey = configuration.GetSection("TokenOptions:SecurityKey").Value!
            };
            _accessTokenExpirations = DateTime.Now.AddMinutes(_tokenOptions.AccessTokenExpiration);
        }

        public AccessToken CreateToken(User user, List<OperationClaim> operationClaims)
        {
            var securityKey = SecurityKeyHelper.CreateSecurityKey(_tokenOptions.SecurityKey);
            var signingCredentials = SigningCredentialHelper.CreateSigningCredentials(securityKey);
            var jwt=CreateJwtSecurityToken(_tokenOptions,user,signingCredentials,operationClaims);
            var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
            var token=jwtSecurityTokenHandler.WriteToken(jwt);

            return new AccessToken() {
                Token = token,
                Expiration = _accessTokenExpirations,
            };
        }

        public JwtSecurityToken CreateJwtSecurityToken(TokenOptions tokenOptions,User user,SigningCredentials signingCredentials, List<OperationClaim> operationClaims)
        {
            var jwt = new JwtSecurityToken(
                issuer : tokenOptions.Issuer,
                audience : tokenOptions.Audience,
                expires :_accessTokenExpirations,
                notBefore : DateTime.Now,
                claims : SetClaims(user,operationClaims),
                signingCredentials : signingCredentials
                );

            return jwt;
        }

        private IEnumerable<Claim> SetClaims(User user, List<OperationClaim> operationClaims)
        {
            var claims = new List<Claim>();
            claims.AddNameIdentifier(user.Id.ToString());
            claims.AddEmail(user.Email);
            claims.AddName($"{user.FirstName} {user.LastName}");
            claims.AddRoles(operationClaims.Select(c=>c.Name).ToArray());
            return claims;
        }
    }
}
