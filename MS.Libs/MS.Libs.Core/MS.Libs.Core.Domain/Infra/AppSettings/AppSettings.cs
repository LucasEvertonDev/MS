using Microsoft.Extensions.Configuration;

namespace MS.Libs.Core.Domain.Infra.AppSettings;

public class AppSettings
{
    AppSettings() { }   
    public AppSettings(IConfiguration config)
    {
        DbConnection = config.GetConnectionString("SqlConnection");
    }
    public string DbConnection { get; set; }
}
