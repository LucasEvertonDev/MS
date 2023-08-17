namespace MS.Infra.WebApi.HttpContainers;

public class RequestDTO<TRequest>
{
    public RequestDTO()
    {
        Body = Activator.CreateInstance<TRequest>();
    }

    public TRequest Body { get; set; }
}