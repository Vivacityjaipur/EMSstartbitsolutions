using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using BAL;
using BOL;

namespace EMSstartbit.TokenAuthentication
{
    public class TokenManager : ITokenManager
    {
        private JwtSecurityTokenHandler tokenHandler;
        private byte[] secretKey;
        private readonly IloginData _ldata;
        public TokenManager(IloginData ldata)
        {
            _ldata = ldata;
            tokenHandler = new JwtSecurityTokenHandler();
            secretKey = Encoding.ASCII.GetBytes("abcdefghijklmnopqrstuvwxyzabcdef");
        }

        public object Ecoding { get; }

        public async Task<string> Authenticate(int eid, string password)
        {
            var logData = await _ldata.getByEid(eid);
            if (logData == null)
            {
                //error message employee not found 
                return "User not Found";
            }
            if (logData.is_active == false)
            {
                return "User is not Active";
            }

            if (!string.IsNullOrWhiteSpace(password) && password == logData.password)
            {
                //Authenticate success
                return "Authenticated";
            }
            else
            {
                //Password doesn't not match
                return "Password Doesn't Match";
            }

        }
        public async Task<string> NewToken(string username)
        {
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[] { new Claim(ClaimTypes.Email, username) }),
                Expires = DateTime.UtcNow.AddMinutes(30),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(secretKey), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            //var JwtString = tokenHandler.WriteToken(token);
            //return  JwtString;
            return await System.Threading.Tasks.Task.Run(() => tokenHandler.WriteToken(token));
        }
        public async Task<string> VerifyToken(string token)
        {
            //var Claims = tokenHandler.ValidateToken(token,
            //     new TokenValidationParameters
            //     {
            //         ValidateIssuerSigningKey = true,
            //         IssuerSigningKey = new SymmetricSecurityKey(secretKey),
            //         ValidateLifetime = true,
            //         ValidateAudience = false,
            //         ValidateIssuer = false,
            //         ClockSkew = TimeSpan.Zero
            //     }, out SecurityToken validatedToken);
            try
            {
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(secretKey),
                    ValidateLifetime = true,
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    // set clockskew to zero so tokens expire exactly at token expiration time (instead of 5 minutes later)
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);

                var jwtToken = (JwtSecurityToken)validatedToken;
                //var accountId = jwtToken.Claims.First(x => x.Type == "email").Value;

                //return accountId;
                return await System.Threading.Tasks.Task.Run(() => jwtToken.Claims.First(x => x.Type == "email").Value);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }
    }
}
