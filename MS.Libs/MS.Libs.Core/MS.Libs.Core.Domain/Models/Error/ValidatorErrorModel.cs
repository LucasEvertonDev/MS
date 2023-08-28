using MS.Libs.Core.Domain.Models.Base;

namespace MS.Libs.Core.Domain.Models.Error;

public class ValidatorErrorModel : BaseModel, IModel
{
    public ValidatorErrorModel(string propName, string message, string code)
    {
        this.ErrorCode = code;
        this.ErrorMessage = message;
        this.Property = propName;
    }
    public string ErrorMessage { get; set; }
    public string Property { get; set; }
    public string ErrorCode { get; set; }
}
