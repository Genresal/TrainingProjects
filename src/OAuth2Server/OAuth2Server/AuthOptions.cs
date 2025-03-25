using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace OAuth2Server;

public class AuthOptions
{
    public const string ISSUER = "MyAuthServer";
    public const string AUDIENCE = "MyAuthClient";
    const string KEY = "mysupersecret_secretkey_that_is_long_enough!@#$%^&*";   // at least 32 bytes
    public const int LIFETIME = 30;

    public static SymmetricSecurityKey GetSymmetricSecurityKey()
    {
        return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(KEY));
    }
}
