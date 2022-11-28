using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace TaskDesk.Identity.Options;

public class JwtOptions
{
    public string Secret { get; set; } = default!;
    public int ExparedInMinutes { get; set; }
    public string Issuer { get; set; } = default!;
    public string Audience { get; set; } = default!;

    public SymmetricSecurityKey GetSymmetricSecurityKey()
    {
        return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Secret));
    }
}