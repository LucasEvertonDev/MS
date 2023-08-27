using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;

namespace MS.Services.Gateway.Core.Domain.Models.Courses;

public class SearchCourseDto
{
    public SearchCourseDto() { }

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

public class SearchedCourseModel
{
    [DefaultValue("F97E565D-08AF-4281-FC11-C0206EAE06FA")]
    public string Id { get; set; }
    [DefaultValue("Adminstração")]
    public string Name { get; set; }
    [DefaultValue("2023-01-01")]
    public DateTime StartDate { get; set; }
    [DefaultValue("2023-12-31")]
    public DateTime EndDate { get; set; }
}
