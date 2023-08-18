using MS.Libs.Infra.Utils.Exceptions.Base;
using System.Runtime.Serialization;

namespace MS.Libs.Infra.Utils.Exceptions;

[Serializable]
public class BusinessException : MSException
{
    public List<string> ErrorsMessages { get; set; }

    public BusinessException(params string[] errorsMessage) : base(string.Empty)
    {
        ErrorsMessages = errorsMessage.ToList();
    }

    protected BusinessException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }
}
