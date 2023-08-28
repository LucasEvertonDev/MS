using MS.Libs.Core.Domain.Models.Base;

namespace MS.Libs.Core.Domain.Models.Error;

public class BusinessErrorModel : BaseModel
{
    public BusinessErrorModel(string message, string code, string context = "")
    {
        this.ErrorCode = code;
        this.ErrorMessage = message;
        this.Context = context;
    }
    public string ErrorMessage { get; set; }
    public string Context { get; set; }
    public string ErrorCode { get; set; }
}
