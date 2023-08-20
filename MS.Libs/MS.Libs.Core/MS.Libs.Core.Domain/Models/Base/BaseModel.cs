using System.Text.Json.Serialization;

namespace MS.Libs.Core.Domain.Models.Base;

public class BaseModel : IModel
{
    public BaseModel()
    {

    }

    public virtual string Id { get; set; }
}
