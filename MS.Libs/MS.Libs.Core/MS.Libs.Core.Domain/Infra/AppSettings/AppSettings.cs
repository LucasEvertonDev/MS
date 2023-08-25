using Microsoft.Extensions.Configuration;

namespace MS.Libs.Core.Domain.Infra.AppSettings;

public class AppSettings
{
    public AppSettings() 
    {
        MemoryCache = new MemoryCacheConfig(5, 10);
    }   
    public AppSettings(IConfiguration config)
    {
        DbConnection = config.GetConnectionString("SqlConnection");

        MemoryCache = new MemoryCacheConfig(
            slidingExpirationInMinutes: int.Parse(config["MemoryCache:SlidingExpirationInMinutes"] ?? "1"),
            absoluteExpirationInMinutes: int.Parse(config["MemoryCache:SlidingExpirationInMinutes"] ?? "1")
        );
    }
    public string DbConnection { get; set; }

    public MemoryCacheConfig MemoryCache { get; set; }
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
