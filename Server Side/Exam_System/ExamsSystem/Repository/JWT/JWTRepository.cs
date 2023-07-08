using ExamsSystem.Repository.IEntities;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ExamsSystem.Repository.JWT
{
    public class JWTRepository : IJWT
    {
        #region Fileds
        IConfiguration _configuration;
        #endregion

        #region Constructors
        public JWTRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        #endregion

        #region Methods
        public string GenentateToken(ICollection<Claim> claims, int numberOfDays)
        {
            #region Secret Key
            SymmetricSecurityKey secritKey =
                new SymmetricSecurityKey(Encoding.ASCII.GetBytes(s: _configuration["Jwt:Key"]));
            #endregion

            SigningCredentials? signingCredentials = new SigningCredentials(secritKey, SecurityAlgorithms.HmacSha256);
            JwtSecurityToken? jwtSecurityToken = new JwtSecurityToken(
                _configuration["Jwt:Issuer"],
                _configuration["Jwt:Audience"],
                claims: claims,
                signingCredentials: signingCredentials,
                expires: DateTime.Now.AddDays(numberOfDays)
                );
            return new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
        }

        public string ClearerToken(string token)
        {
            if (token != null)
            {
                // Create a new set of claims and an expiration date in the past
                List<Claim>? claims = new List<Claim>();
                int expirationDate = -1;

                // Generate a new JWT token with the expired claims and set it as the authentication header
                var newToken = GenentateToken(claims, expirationDate);
                return newToken;

            }

            return token;
        }

        #endregion
    }
}
