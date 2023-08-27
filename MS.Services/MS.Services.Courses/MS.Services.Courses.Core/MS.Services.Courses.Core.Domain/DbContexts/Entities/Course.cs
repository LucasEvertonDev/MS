using MS.Libs.Core.Domain.DbContexts.Entities.Base;

namespace MS.Services.Courses.Core.Domain.DbContexts.Entities;

public class Course : BaseEntityLastUpdateBy
{
    public string Name { get; set; }

    public DateTime StartDate { get; set; }

    public DateTime EndDate { get; set; }
}
