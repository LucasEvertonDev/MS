using MS.Libs.Core.Domain.Models.Error;

namespace MS.Services.Auth.Core.Domain.Contansts;

public class AuthErrors
{
    public static ErrorModel LOGIN_INVALIDO = new ErrorModel("Login ou senha inválidos!", "LOGIN_INVALIDO");
}

public class UserErrors
{
    public static ErrorModel ALREADY_USERNAME = new ErrorModel("Já existe um usuário cadastrado com o login informado", "ALREADY_USERNAME");

    public static ErrorModel ALREADY_EMAIL = new ErrorModel("Já existe um usuário cadastrado com o email informado", "ALREADY_EMAIL");

    public static ErrorModel USERNAME_REQUIRED = new ErrorModel("Username é obrigatorio", "USERNAME_REQUIRED");

    public static ErrorModel USERNAME_INVALID = new ErrorModel("Username inválido", "USERNAME_INVALID");

    public static ErrorModel EMAIL_INVALID = new ErrorModel("Email inválido", "EMAIL_INVALID");

    public static ErrorModel PASSWORD_INVALID = new ErrorModel("Senha inválida", "PASSWORD_INVALID");
}