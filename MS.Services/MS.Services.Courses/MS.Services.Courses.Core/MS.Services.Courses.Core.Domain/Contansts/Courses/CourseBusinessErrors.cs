using MS.Libs.Core.Domain.Models.Error;

namespace MS.Services.Courses.Core.Domain.Contansts.Course;

public class CourseBusinessErrors
{

    public ErrorModel COURSE_NOT_FOUND = new ErrorModel("Não foi possível recuperar o curso pela chave passada.", "COURSE_NOT_FOUND");

    public ErrorModel ALREADY_COURSE = new ErrorModel("Já existe um curso cadastrado com o nome informado", "ALREADY_COURSE");
}
