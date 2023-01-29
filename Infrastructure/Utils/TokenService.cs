using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace Infrastructure.Utils;

public class TokenService
{
    private const double ExpiryDurationMinutes = 360;

    public string BuildToken(string? key, string issuer, string username, string role,int id)
    {
        var claims = new[] {   
            new Claim("id",id.ToString()),
            new Claim(ClaimTypes.Name, username),
            new Claim(ClaimTypes.Role,role),
            new Claim(ClaimTypes.NameIdentifier,
                Guid.NewGuid().ToString())
        };

        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));        
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);           
        var tokenDescriptor = new JwtSecurityToken(issuer, issuer, claims, 
            expires: DateTime.Now.AddMinutes(ExpiryDurationMinutes), signingCredentials: credentials);        
        return new JwtSecurityTokenHandler().WriteToken(tokenDescriptor);  
    }
    public string BuildTokenRole(string? key, string? issuer, string role)
    {
        var claims = new[] 
        {
            new Claim(ClaimTypes.Role, role),
            new Claim(ClaimTypes.NameIdentifier,
                Guid.NewGuid().ToString())
        };

        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));        
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);           
        var tokenDescriptor = new JwtSecurityToken(issuer, issuer, claims, 
            expires: DateTime.Now.AddMinutes(ExpiryDurationMinutes), signingCredentials: credentials);        
        return new JwtSecurityTokenHandler().WriteToken(tokenDescriptor);  
    }
    
    /*public bool IsTokenValid(string key, string issuer, string? token)
    {
        var mySecret = Encoding.UTF8.GetBytes(key);           
        var mySecurityKey = new SymmetricSecurityKey(mySecret);
        var tokenHandler = new JwtSecurityTokenHandler(); 
        try 
        {
            tokenHandler.ValidateToken(token, 
                new TokenValidationParameters   
                {
                    ValidateIssuerSigningKey = true,
                    ValidateIssuer = true, 
                    ValidateAudience = true,    
                    ValidIssuer = issuer,
                    ValidAudience = issuer, 
                    IssuerSigningKey = mySecurityKey,
                }, out SecurityToken validatedToken);            
        }
        catch
        {
            return false;
        }
        return true;    
    }*/
    
    public bool IsTokenValid(string token, string key, string issuer)
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
        var tokenValidationParameters = new TokenValidationParameters
        {
            ValidateLifetime = true,
            ValidateAudience = false,
            ValidateIssuer = true,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = securityKey,
            ValidIssuer = issuer
        };

        try
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            
            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out var validatedToken);

            if (!tokenHandler.CanReadToken(token))
            {
                return false;
            }

            if (!(validatedToken is JwtSecurityToken jwtSecurityToken))
            {
                return false;
            }

            if (!jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
            {
                return false;
            }

            return true;
        }
        catch (SecurityTokenExpiredException)
        {
            return false;
        }
        catch (SecurityTokenException)
        {
            return false;
        }
    }

    
    
}

