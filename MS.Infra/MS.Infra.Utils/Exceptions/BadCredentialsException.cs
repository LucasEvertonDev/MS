using MS.Infra.Utils.Exceptions.Base;
using MS.Infra.Utils.Resources;
using System.Runtime.Serialization;

namespace MS.Infra.Utils.Exceptions;

[Serializable]
public class BadCredentialsException : MSException
{
    public BadCredentialsException() : base(ResourceMessages.UserOrPasswordInvalid)
    {

    }

    protected BadCredentialsException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }
}
