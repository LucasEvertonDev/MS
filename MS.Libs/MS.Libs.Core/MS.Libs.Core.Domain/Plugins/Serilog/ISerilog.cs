namespace MS.Libs.Core.Domain.Plugins.Serilog
{
    public interface ISerilog
    {
        void LogError<T>(Exception ex, string message);
        void LogError<T>(string message);
        void LogInformation<T>(string message);
        void LogWarning<T>(string message);
    }
}