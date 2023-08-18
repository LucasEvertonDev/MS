using MS.Libs.Core.Domain.Models.Base;

namespace MS.Services.Auth.Core.Domain.Models;

public class TokenModel : BaseModel
{
    public string Token { get; set; }

    public DateTime Expiration { get; set; }
}
