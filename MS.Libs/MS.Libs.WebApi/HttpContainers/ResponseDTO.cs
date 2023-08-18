namespace MS.Libs.WebApi.HttpContainers;

public class ResponseDTO<TModel>
{
    public ResponseDTO()
    {
        Content = Activator.CreateInstance<TModel>();
        Sucess = true;
    }

    public TModel Content { get; set; }

    public bool Sucess { get; set; }
}
