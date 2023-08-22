using MS.Libs.Core.Domain.Models.Error;
using MS.Libs.Infra.Utils.Exceptions.Base;
using System.Runtime.Serialization;

namespace MS.Libs.Infra.Utils.Exceptions;

[Serializable]
public class BusinessException : MSException
{
    public List<ErrorModel> ErrorsMessages { get; set; }

    public BusinessException(params ErrorModel[] error) : base(string.Empty)
    {
        ErrorsMessages = error.ToList();
    }

    protected BusinessException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }
}
