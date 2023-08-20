namespace MS.Services.Auth.Core.Domain.Models.Auth;

public interface IToken
{
    public string TokenJWT { get; set; }
    public DateTime DataExpiracao { get; set; }
}
