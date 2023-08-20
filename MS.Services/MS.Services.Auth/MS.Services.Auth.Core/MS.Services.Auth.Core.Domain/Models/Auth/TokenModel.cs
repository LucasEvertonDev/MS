using MS.Libs.Core.Domain.Models.Base;

namespace MS.Services.Auth.Core.Domain.Models.Auth;

public class TokenModel : BaseModel, IToken
{
    public string TokenJWT { get; set; }
    public DateTime DataExpiracao { get; set; }
}
