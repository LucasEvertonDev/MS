using MS.Libs.Core.Domain.Models.Error;

namespace MS.Services.Auth.Core.Domain.Contansts.User;

public class UserBusinessErrors
{

    public ErrorModel USER_NOT_FOUND = new ErrorModel("Não foi possível recuperar o usuário pela chave passada.", "USER_NOT_FOUND");

    public ErrorModel ALREADY_USERNAME = new ErrorModel("Já existe um usuário cadastrado com o login informado", "ALREADY_USERNAME");

    public ErrorModel ALREADY_EMAIL = new ErrorModel("Já existe um usuário cadastrado com o email informado", "ALREADY_EMAIL");
}
