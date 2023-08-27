using Microsoft.AspNetCore.Mvc;
using MS.Libs.Core.Domain.Models.Base;
using System.ComponentModel;
using System.Text.Json.Serialization;

namespace MS.Services.Students.Core.Domain.Models.Students;

public class UpdateStudentDto : BaseModel
{
    [JsonIgnore]
    [FromRoute(Name = "id")]
    public virtual string Id { get; set; }

    [FromBody]
    public UpdateStudentModel Body { get; set; }
}

public class UpdateStudentModel : BaseModel
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

public class UpdatedStudentModel : BaseModel
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
