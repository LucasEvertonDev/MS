namespace MS.Libs.Core.Domain.Infra.Attributes;

public class CacheKey : Attribute 
{
    public string Key { get; set; }

    public long SlidingExpirationInMinutes { get; set; }

    public long AbsoluteExpirationInMinutes { get; set; }
}
