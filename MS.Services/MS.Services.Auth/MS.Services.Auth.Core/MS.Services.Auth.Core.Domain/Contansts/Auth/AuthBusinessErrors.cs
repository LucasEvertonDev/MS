using MS.Libs.Core.Domain.Models.Error;

namespace MS.Services.Auth.Core.Domain.Contansts.Auth;

public class AuthBusinessErrors
{
    public ErrorModel INVALID_LOGIN = new ErrorModel("Login ou senha inválidos!", "INVALID_LOGIN");

    public ErrorModel INVALID_REFRESH_TOKEN = new ErrorModel("Token inválido. Não foi possível atualizar o token", "INVALID_REFRESH_TOKEN");

    public ErrorModel CLIENT_CREDENTIALS_INVALID = new ErrorModel("Client Credentials inválidas.", "CLIENT_CREDENTIALS_INVALID");
}
