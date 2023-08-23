using Microsoft.Extensions.Configuration;

namespace MS.Libs.Core.Domain.Infra.AppSettings;

public class AppSettings
{
    public AppSettings() 
    {
        MemoryCache = new MemoryCacheConfig(5, 10, 1024);
    }   
    public AppSettings(IConfiguration config)
    {
        DbConnection = config.GetConnectionString("SqlConnection");

        MemoryCache = new MemoryCacheConfig(
            slidingExpirationInMinutes: int.Parse(config["MemoryCache:SlidingExpirationInMinutes"] ?? "5"),
            absoluteExpirationInMinutes: int.Parse(config["MemoryCache:SlidingExpirationInMinutes"] ?? "10"),
            sizeLimitInMb: int.Parse(config["MemoryCache:SizeLimitInMb"] ?? "1024")
        );
    }
    public string DbConnection { get; set; }

    public MemoryCacheConfig MemoryCache { get; set; }
}

public class MemoryCacheConfig
{
    public MemoryCacheConfig(long slidingExpirationInMinutes, long absoluteExpirationInMinutes, long sizeLimitInMb)
    {
        SlidingExpirationInMinutes = slidingExpirationInMinutes;
        AbsoluteExpirationInMinutes = absoluteExpirationInMinutes;
        SizeLimitInMb = sizeLimitInMb;
    }

    public long SlidingExpirationInMinutes { get; set; }

    public long AbsoluteExpirationInMinutes { get; set; }

    public long SizeLimitInMb { get; set; }
}
