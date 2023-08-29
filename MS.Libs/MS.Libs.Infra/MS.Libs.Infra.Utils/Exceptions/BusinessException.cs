using MS.Libs.Core.Domain.Models.Error;
using MS.Libs.Infra.Utils.Exceptions.Base;
using System.Runtime.Serialization;

namespace MS.Libs.Infra.Utils.Exceptions;

[Serializable]
public class BusinessException : MSException
{
    public ErrorModel Error { get; set; }

    public BusinessException(ErrorModel error) : base(error.Message)
    {
        Error = error;
    }

    protected BusinessException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }
}
