using MS.Core.Domain.Models.Base;

namespace MS.Core.Domain.Models.Error;
public class ErrorsModel : BaseModel
{
    public ErrorsModel() { }

    public string[] Messages { get; set; }

    public ErrorsModel(params string[] message)
    {
        this.Messages = message;
    }
}
