﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CUDJobAPiIdentity.Contracts;
using NLog;

namespace CUDJobAPiIdentity.Services
{
    public class LoggerService : ILoggerService
    {
        public static ILogger _logger = LogManager.GetCurrentClassLogger();
        public void LogDebug(string message)
        {
            _logger.Debug(message);
        }

        public void LogError(string message)
        {
            _logger.Error(message);
        }

        public void LogInfo(string message)
        {
            _logger.Info(message);
        }

        public void LogWarn(string message)
        {
            _logger.Warn(message);
        }
    }
}
