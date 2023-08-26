using MS.Libs.Core.Domain.Models.Error;

namespace MS.Services.Auth.Core.Domain.Contansts.User;

public class AuthValidatorsErrors
{
    public ErrorModel USER_GROUP_NOT_FOUND = new ErrorModel("Grupo de usuário inválido.", "USER_GROUP_NOT_FOUND");

    public ErrorModel USERNAME_REQUIRED = new ErrorModel("Username é obrigatorio", "USERNAME_REQUIRED");

    public ErrorModel USERNAME_INVALID = new ErrorModel("Username inválido", "USERNAME_INVALID");

    public ErrorModel EMAIL_INVALID = new ErrorModel("Email inválido", "EMAIL_INVALID");

    public ErrorModel EMAIL_REQUIRED = new ErrorModel("Email é obrigatório", "EMAIL_REQUIRED");

    public ErrorModel PASSWORD_LENGTH = new ErrorModel("Senha deve ter no mínimo 6 caracteres", "PASSWORD_LENGTH");

    public ErrorModel PASSWORD_REQUIRED = new ErrorModel("Senha é obrigatória", "PASSWORD_REQUIRED");

    public ErrorModel USER_GROUP_REQUIRED = new ErrorModel("Grupo de usuário é obrigatório", "USER_GROUP_REQUIRED");
}
