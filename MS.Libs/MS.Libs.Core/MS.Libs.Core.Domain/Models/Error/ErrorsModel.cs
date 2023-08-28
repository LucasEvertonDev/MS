using MS.Libs.Core.Domain.Models.Base;

namespace MS.Libs.Core.Domain.Models.Error;
public class ErrorsModel : BaseModel
{
    public ErrorsModel() { }

    public ErrorModel[] Messages { get; set; }

    public ErrorsModel(params ErrorModel[] message)
    {
        Messages = message;
    }
}
