namespace MS.Services.Auth.Core.Domain.Models;

public interface IServiceDTO<TRequest, TResponse> : IServiceDTO
{
    public TRequest Request { get; set; }

    public TResponse Response { get; set; } 
}

public interface IServiceDTO
{
}
