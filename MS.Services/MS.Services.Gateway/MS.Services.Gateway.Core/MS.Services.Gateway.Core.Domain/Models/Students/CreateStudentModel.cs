using Microsoft.AspNetCore.Mvc;
using MS.Libs.Core.Domain.Models.Base;
using System.ComponentModel;

namespace MS.Services.Gateway.Core.Domain.Models.Students;

public class CreateStudentsDto : BaseModel
{
    [FromHeader(Name = "Authorization")]
    public string Token { get; set; }
    [FromBody]
    public CreateStudentModel Body { get; set; }
}

public class CreateStudentModel : BaseModel
{
    [DefaultValue("13201692990")]
    public string Cpf { get; set; }
    [DefaultValue("José de Paula")]
    public string Name { get; set; }
    [DefaultValue("Maria de Paula")]
    public string MotherName { get; set; }
    [DefaultValue("João de Paula")]
    public string FatherName { get; set; }
}

public class CreatedStudentModel : BaseModel
{
    [DefaultValue("F97E565D-08AF-4281-FC11-C0206EAE06FA")]
    public string Id { get; set; }
    [DefaultValue("13201692990")]
    public string Cpf { get; set; }
    [DefaultValue("José de Paula")]
    public string Name { get; set; }
    [DefaultValue("Maria de Paula")]
    public string MotherName { get; set; }
    [DefaultValue("João de Paula")]
    public string FatherName { get; set; }
}
