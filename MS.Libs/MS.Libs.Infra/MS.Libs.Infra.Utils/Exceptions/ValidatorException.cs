using MS.Libs.Core.Domain.Models.Error;
using MS.Libs.Infra.Utils.Exceptions.Base;
using System.Runtime.Serialization;

namespace MS.Libs.Infra.Utils.Exceptions;

[Serializable]
public class ValidatorException : MSException
{
    public List<ValidatorErrorModel> ErrorsMessages { get; set; }

    public ValidatorException(params ValidatorErrorModel[] error) : base(string.Join(", ", error.Select(a => a.ErrorMessage)))
    {
        ErrorsMessages = error.ToList();
    }

    protected ValidatorException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }
}
