using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;

namespace MS.Services.Gateway.Core.Domain.Models.Students;

public class SeacrhStudentDto
{
    public SeacrhStudentDto() { }


    [FromHeader(Name = "Authorization")]
    public string Token { get; set; }

    [DefaultValue("1")]
    [FromRoute(Name = "pagenumber")]
    public int PageNumber { get; set; }

    [DefaultValue("10")]
    [FromRoute(Name = "pagesize")]
    public int PageSize { get; set; }

    [FromQuery(Name = "name")]
    public string Name { get; set; }
}

public class SearchedStudentModel
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
