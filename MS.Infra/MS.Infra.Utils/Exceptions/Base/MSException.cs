using System.Runtime.Serialization;

namespace MS.Infra.Utils.Exceptions.Base;

[Serializable]
public class MSException : SystemException
{
    public MSException(string message) : base(message)
    {
    }

    protected MSException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }
}
