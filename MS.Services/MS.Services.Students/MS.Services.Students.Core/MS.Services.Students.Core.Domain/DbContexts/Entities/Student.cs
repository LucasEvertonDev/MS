using MS.Libs.Core.Domain.DbContexts.Entities.Base;

namespace MS.Services.Students.Core.Domain.DbContexts.Entities;

public class Student : BaseEntityLastUpdateBy
{
    public string Cpf { get; set; }

    public string Name { get; set; }

    public string MotherName { get; set; }

    public string FatherName { get; set; }
}
