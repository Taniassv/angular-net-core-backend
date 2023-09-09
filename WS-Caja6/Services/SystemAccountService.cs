using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WS_Caja6.Models;
using WS_Caja6.Models.Common;
using WS_Caja6.Models.Request;
using WS_Caja6.Models.Response;
using WS_Caja6.Tools;

namespace WS_Caja6.Services
{
    public class SystemAccountService : ISystemAccountService
    {
        private readonly AppSettings _appSettings;

        public SystemAccountService(IOptions<AppSettings> appSettings) 
        {
            _appSettings = appSettings.Value;
        }
        public SystemAccountResponse Auth(AuthRequest model)
        {
            SystemAccountResponse systemaccountresponse = new SystemAccountResponse();
            using (var db = new DbCajaContext()) 
            {
                string spassword = Encrypt.GetSha256(model.Password);
                var user = db.SystemAccounts.Where(d=>d.Email == model.Email && 
                d.PasswordHash == spassword).FirstOrDefault();
                if (user == null) return null;
                systemaccountresponse.Email = user.Email;
                systemaccountresponse.Token = GetToken(user);
            }
            return systemaccountresponse;
        }

        private string GetToken(SystemAccount usuario) 
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var llave = Encoding.ASCII.GetBytes(_appSettings.Secreto);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new System.Security.Claims.ClaimsIdentity(
                    new Claim[]
                    {
                        new Claim(ClaimTypes.NameIdentifier, usuario.Id.ToString()),
                        new Claim(ClaimTypes.NameIdentifier, usuario.Email)
                    }),
                Expires = DateTime.UtcNow.AddDays(60),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(llave), 
                SecurityAlgorithms.HmacSha256Signature),
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
