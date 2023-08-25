using MS.Libs.Core.Domain.Models.Base;

namespace MS.Libs.Core.Domain.Models.Error;
public class ErrorsModel : BaseModel
{
    public ErrorsModel() { }

    public bool Sucess { get; set; } = false;

    public string[] Messages { get; set; }

    public ErrorsModel(params string[] message)
    {
        Messages = message;
    }
}
