using MS.Libs.Core.Domain.Models.Error;

namespace MS.Services.Students.Core.Domain.Contansts.Student;

public class StudentBusinessErrors
{

    public ErrorModel STUDENT_NOT_FOUND = new ErrorModel("Não foi possível recuperar o aluno pela chave passada.", "STUDENT_NOT_FOUND");

    public ErrorModel ALREADY_STUDENT = new ErrorModel("Já existe um aluno cadastrado com o cpf informado", "ALREADY_STUDENT");
}
