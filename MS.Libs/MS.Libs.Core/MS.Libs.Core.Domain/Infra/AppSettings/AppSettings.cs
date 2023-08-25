using Microsoft.Extensions.Configuration;

namespace MS.Libs.Core.Domain.Infra.AppSettings;

public class AppSettings
{
    public AppSettings() 
    {
        MemoryCache = new MemoryCacheConfig(5, 10);
        Messages = new Messages();
    }   
    public AppSettings(IConfiguration config)
    {
        DbConnection = config.GetConnectionString("SqlConnection");

        MemoryCache = new MemoryCacheConfig(
            slidingExpirationInMinutes: int.Parse(config["MemoryCache:SlidingExpirationInMinutes"] ?? "1"),
            absoluteExpirationInMinutes: int.Parse(config["MemoryCache:SlidingExpirationInMinutes"] ?? "1")
        );

        Messages = new Messages();
    }
    public string DbConnection { get; set; }

    public MemoryCacheConfig MemoryCache { get; set; }

    public Messages Messages { get; set; } 
}

public class MemoryCacheConfig
{
    public MemoryCacheConfig(long slidingExpirationInMinutes, long absoluteExpirationInMinutes)
    {
        SlidingExpirationInMinutes = slidingExpirationInMinutes;
        AbsoluteExpirationInMinutes = absoluteExpirationInMinutes;
    }

    public long SlidingExpirationInMinutes { get; set; }

    public long AbsoluteExpirationInMinutes { get; set; }
}

public class Messages
{
    public string Forbidden { get; set; } = "Não autorizado. Credenciais fornecidas ausentes, inválidas ou expiradas";

    public string Unauthorized { get; set; } = "Acesso negado.Você não tem permissões suficientes para acessar esta API";
}
