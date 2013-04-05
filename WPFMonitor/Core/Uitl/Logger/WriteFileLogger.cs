using System;
using System.IO;
using System.Text;
using System.Windows;

namespace WPFMonitor.Core.Util
{
    internal class WriteFileLogger : ILogger
    {
        #region Fields

        private string _channel;

        private LoggerLevel _level;

        #endregion

        #region Public Memebers
        public WriteFileLogger(string channel)
        {
            SetChannel(channel);
        }

        public void SetLoggerLevel(LoggerLevel level)
        {
            SetLevel(level);
        }

        public void Error(string fuctionName, string title, string message)
        {
            WriteError(fuctionName, title, message);
        }

        public void Error(string fuctionName, string title, string message, params object[] args)
        {
            WriteError(fuctionName, title, message, args);
        }

        public void Debug(string fuctionName, string title, string message)
        {
            WriteDebug(fuctionName, title, message);
        }

        public void Debug(string fuctionName, string title, string message, params object[] args)
        {
            WriteDebug(fuctionName, title, message, args);
        }

        public void Trance(string fuctionName, string title, string message)
        {
            WriteTrance(fuctionName, title, message);
        }

        public void Trance(string fuctionName, string title, string message, params object[] args)
        {
            WriteTrance(fuctionName, title, message, args);
        }

        #endregion

        #region Private Memebers

        private void SetChannel(string channel)
        {
            _channel = channel;
        }

        private void SetLevel(LoggerLevel level)
        {
            _level = level;
        }

        private void WriteError(string fuctionName, string title, string message)
        {
            if (_level != LoggerLevel.NoLog)
            {
                MessageBox.Show(string.Concat("错误 ", title), message);
                LogMessage(fuctionName, title, message);
            }
        }

        private void WriteError(string fuctionName, string title, string message, object[] args)
        {
            if (_level != LoggerLevel.NoLog)
            {
                MessageBox.Show(string.Concat("错误 ", title), string.Format(message, args));
                LogMessage(fuctionName, title, message, args);
            }
        }

        private void WriteDebug(string fuctionName, string title, string message)
        {
            if (_level != LoggerLevel.NoLog
                && _level != LoggerLevel.Error)
            {
                MessageBox.Show(string.Concat("调试 ", title), message);
                LogMessage(fuctionName, title, message);
            }
        }

        private void WriteDebug(string fuctionName, string title, string message, object[] args)
        {
            if (_level != LoggerLevel.NoLog
                && _level != LoggerLevel.Error)
            {
                MessageBox.Show(string.Concat("调试 ", title), string.Format(message, args));
                LogMessage(fuctionName, title, message, args);
            }
        }

        private void WriteTrance(string fuctionName, string title, string message)
        {
            if (_level != LoggerLevel.NoLog
                && _level != LoggerLevel.Error
                && _level != LoggerLevel.Debug)
            {
                LogMessage(fuctionName, title, message);
            }
        }

        private void WriteTrance(string fuctionName, string title, string message, object[] args)
        {
            if (_level != LoggerLevel.NoLog
                && _level != LoggerLevel.Error
                && _level != LoggerLevel.Debug)
            {
                LogMessage(fuctionName, title, message, args);
            }
        }

        private void LogMessage(string fuctionName, string title, string message)
        {
            WriteLog(string.Format("<{0}> <{1}> <{2}> <{3}> <{4}> {5}",
                DateTime.Now.ToString("mm:ss"),
                _channel,
                _level,
                fuctionName,
                title,
                message));
        }

        private void LogMessage(string fuctionName, string title, string message, params object[] args)
        {
            WriteLog(string.Format(
                string.Format("<{0}> <{1}> <{2}> <{3}> <{4}> {5}",
                   DateTime.Now.ToString("mm:ss"),
                   _channel,
                   _level,
                    fuctionName,
                    title,
                   message), args));
        }

        private void WriteLog(string msg)
        {
            try
            {
                var path = Common.GetAppPath("Log", DateTime.Now.ToString("yyyy-MM-dd.log"));
                File.AppendAllText(msg, path, Encoding.Default);
            }
            catch { }
        }

        #endregion
    }
}
