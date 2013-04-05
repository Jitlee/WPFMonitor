
namespace WPFMonitor
{
    internal interface ILogger
    {
        void SetLoggerLevel(LoggerLevel level);

        void Error(string fuctionName, string title, string message);

        void Error(string fuctionName, string title, string message, params object[] args);

        void Debug(string fuctionName, string title, string message);

        void Debug(string fuctionName, string title, string message, params object[] args);

        void Trance(string fuctionName, string title, string message);

        void Trance(string fuctionName, string title, string message, params object[] args);
    }
}
