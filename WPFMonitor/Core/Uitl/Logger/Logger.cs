using System;
using System.Diagnostics;

namespace WPFMonitor.Core.Util
{
    internal class Logger : ILogger
    {
        #region Fields

        private string _channel;

        private LoggerLevel _level;

        #endregion

        #region Public Memebers
        public Logger(string channel)
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

        [Conditional("DEBUG")]
        private void SetChannel(string channel)
        {
            _channel = channel;
        }


        [Conditional("DEBUG")]
        private void SetLevel(LoggerLevel level)
        {
            _level = level;
        }

        [Conditional("DEBUG")]
        private void WriteError(string fuctionName, string title, string message)
        {
            if (_level != LoggerLevel.NoLog)
            {
                LogMessage(fuctionName, title, message);
            }
        }

        [Conditional("DEBUG")]
        private void WriteError(string fuctionName, string title, string message, object[] args)
        {
            if (_level != LoggerLevel.NoLog)
            {
                LogMessage(fuctionName, title, message, args);
            }
        }

        [Conditional("DEBUG")]
        private void WriteDebug(string fuctionName, string title, string message)
        {
            if (_level != LoggerLevel.NoLog
                && _level != LoggerLevel.Error)
            {
                LogMessage(fuctionName, title, message);
            }
        }

        [Conditional("DEBUG")]
        private void WriteDebug(string fuctionName, string title, string message, object[] args)
        {
            if (_level != LoggerLevel.NoLog
                && _level != LoggerLevel.Error)
            {
                LogMessage(fuctionName, title, message, args);
            }
        }

        [Conditional("DEBUG")]
        private void WriteTrance(string fuctionName, string title, string message)
        {
            if (_level != LoggerLevel.NoLog
                && _level != LoggerLevel.Error
                && _level != LoggerLevel.Debug)
            {
                LogMessage(fuctionName, title, message);
            }
        }

        [Conditional("DEBUG")]
        private void WriteTrance(string fuctionName, string title, string message, object[] args)
        {
            if (_level != LoggerLevel.NoLog
                && _level != LoggerLevel.Error
                && _level != LoggerLevel.Debug)
            {
                LogMessage(fuctionName, title, message, args);
            }
        }

        [Conditional("DEBUG")]
        private void LogMessage(string fuctionName, string title, string message)
        {
            Console.WriteLine(string.Format("<{0}> <{1}> <{2}> <{3}> <{4}> {5}",
                DateTime.Now.ToString("mm:ss"),
                _channel,
                _level,
                fuctionName,
                title,
                message));
        }

        [Conditional("DEBUG")]
        private void LogMessage(string fuctionName, string title, string message, params object[] args)
        {
            Console.WriteLine(string.Format(
                string.Format("<{0}> <{1}> <{2}> <{3}> <{4}> {5}",
                   DateTime.Now.ToString("mm:ss"),
                   _channel,
                   _level,
                    fuctionName,
                    title,
                   message), args));
        }

        #endregion
    }
}
