namespace Hexacta.Core.Tools.Utilities
{
    using log4net;
    using log4net.Config;
    using log4net.Repository;
    using System;
    using System.Configuration;
    using System.Reflection;
    using System.Diagnostics;
    using System.Collections.Generic;
    using System.Threading;

    public static class LoggerUtility
    {
        private static string loggerName = "default";
        private static ILog logueador;

        static LoggerUtility()
        {
            if (ConfigurationManager.AppSettings["Hexacta.LoggerName"] != null)
            {
                loggerName = ConfigurationManager.AppSettings["Hexacta.LoggerName"];
            }
            logueador = LogManager.GetLogger(loggerName);
            XmlConfigurator.Configure();
            log4net.GlobalContext.Properties["hostname"] = Environment.MachineName;
        }

        #region "Debug"

        public static void Debug(object message)
        {
            actualLog(log4net.Core.Level.Debug, message, null);
        }

        public static void Debug(string loggerConfigName, object message)
        {
            var logObj=LogManager.GetLogger(loggerConfigName);
            actualLog(log4net.Core.Level.Debug, message, null, logObj, 2);
        }

        public static void Debug(string loggerConfigName, object message, Exception exception)
        {
            var logObj = LogManager.GetLogger(loggerConfigName);
            actualLog(log4net.Core.Level.Debug, message, exception, logObj, 2);
        }

        public static void Debug(object message, Exception exception)
        {
            actualLog(log4net.Core.Level.Debug, message, exception);
        }

        public static void DebugFormat(string format, params object[] args)
        {
            actualLog(log4net.Core.Level.Debug, string.Format(format, args), null);
        }

        public static void DebugFormat(IFormatProvider provider, string format, params object[] args)
        {
            actualLog(log4net.Core.Level.Debug, string.Format(provider, format, args), null);
        }

        #endregion

        #region "Info"

        public static void Info(object message)
        {
            actualLog(log4net.Core.Level.Info, message, null);
        }

        public static void Info(object message, Exception exception)
        {
            actualLog(log4net.Core.Level.Info, message, exception);
        }

        public static void Info(string loggerConfigName, object message)
        {
            var logObj=LogManager.GetLogger(loggerConfigName);
            actualLog(log4net.Core.Level.Info, message, null, logObj, 2);
        }

        public static void Info(string loggerConfigName, object message, Exception exception)
        {
            var logObj = LogManager.GetLogger(loggerConfigName);
            actualLog(log4net.Core.Level.Info, message, exception, logObj, 2);
        }


        
        public static void InfoFormat(string format, params object[] args)        
        {
            actualLog(log4net.Core.Level.Info, string.Format(format, args), null);
        }

        public static void InfoFormat(IFormatProvider provider, string format, params object[] args)
        {
            actualLog(log4net.Core.Level.Info, string.Format(provider, format, args), null);
        }

        #endregion

        #region "Warn"

        public static void Warn(object message)
        {
            actualLog(log4net.Core.Level.Warn, message, null);
        }

        public static void Warn(object message, Exception exception)
        {
            actualLog(log4net.Core.Level.Warn, message, exception);
        }

        public static void Warn(string loggerConfigName, object message)
        {
            var logObj = LogManager.GetLogger(loggerConfigName);
            actualLog(log4net.Core.Level.Warn, message, null, logObj, 2);
        }

        public static void Warn(string loggerConfigName, object message, Exception exception)
        {
            var logObj = LogManager.GetLogger(loggerConfigName);
            actualLog(log4net.Core.Level.Warn, message, exception, logObj, 2);
        }

        public static void WarnFormat(string format, params object[] args)
        {
            actualLog(log4net.Core.Level.Warn, string.Format(format, args), null);

        }

        public static void WarnFormat(IFormatProvider provider, string format, params object[] args)
        {
            actualLog(log4net.Core.Level.Warn, string.Format(provider, format, args), null);
        }

        #endregion

        #region "Error"

        public static void Error(object message)
        {
            actualLog(log4net.Core.Level.Error, message, null);
        }

        public static void Error(object message, Exception exception)
        {
            actualLog(log4net.Core.Level.Error, message, exception);
        }


        public static void Error(string loggerConfigName, string message)
        {
            var logObj = LogManager.GetLogger(loggerConfigName);
            actualLog(log4net.Core.Level.Error, message, null, logObj, 2);
        }

        public static void Error(string loggerConfigName, object message, Exception exception)
        {
            var logObj = LogManager.GetLogger(loggerConfigName);
            actualLog(log4net.Core.Level.Error, message, exception, logObj, 2);
        }

        public static void ErrorFormat(string format, params object[] args)
        {
            actualLog(log4net.Core.Level.Error, string.Format(format, args), null);
        }

        public static void ErrorFormat(IFormatProvider provider, string format, params object[] args)
        {
            actualLog(log4net.Core.Level.Error, string.Format(provider, format, args), null);
        }

        #endregion

        #region "Fatal"

        public static void Fatal(object message)
        {
            actualLog(log4net.Core.Level.Fatal, message, null);
        }

        public static void Fatal(object message, Exception exception)
        {
            actualLog(log4net.Core.Level.Fatal, message, exception);
        }

        public static void FatalFormat(string format, params object[] args)
        {
            actualLog(log4net.Core.Level.Fatal, string.Format(format, args), null);
        }

        public static void FatalFormat(IFormatProvider provider, string format, params object[] args)
        {
            actualLog(log4net.Core.Level.Fatal, string.Format(provider, format, args), null);

        }

        #endregion

        #region "Delegates"

        private delegate void DebugDelegate(object message);

        private delegate void ErrorDelegate(object message);

        private delegate void ExceptionErrorDelegate(object message, Exception exception);

        private delegate void ExceptionFatalDelegate(object message, Exception exception);

        private delegate void FatalDelegate(object message);

        private delegate void InfoDelegate(object message);

        private delegate void InfoFormatDelegate(string format, params object[] args);

        #endregion

        public static ILog GetLogger()
        {
            return logueador;
        }

        public static string GetLoggerName()
        {
            return logueador.Logger.Name;
        }

        public static ILoggerRepository GetRepository()
        {
            return logueador.Logger.Repository;
        }

        private static void actualLog(log4net.Core.Level level, object message, Exception exception)
        {
            actualLog(level, message, exception, logueador, 3);
        }

        private static void actualLog(log4net.Core.Level level, object message, Exception exception, ILog internalLogger, int skipFrames)
        {
            StackFrame frame = new StackTrace(skipFrames, true).GetFrame(0);
            MethodBase methodBase = frame.GetMethod();
            getFrame(skipFrames, frame, methodBase);
            log4net.Core.LoggingEventData eventData = getEventData(level, message, exception, frame, methodBase);
            log4net.Core.LoggingEvent le = new log4net.Core.LoggingEvent(methodBase.DeclaringType, logueador.Logger.Repository, eventData, log4net.Core.FixFlags.All);
            internalLogger.Logger.Log(le);
        }

        private static void getFrame(int skipFrames, StackFrame frame, MethodBase methodBase) {
            skipFrames++;
            while (methodBase.DeclaringType == null) {
                frame = new StackTrace(skipFrames++, true).GetFrame(0);
                methodBase = frame.GetMethod();
            }
        }

        private static log4net.Core.LoggingEventData getEventData(log4net.Core.Level level, object message, Exception exception, StackFrame frame, MethodBase methodBase)
        {
            byte contaSteps = 0;
            string step = string.Empty;
            try {
                int lineNumeber = frame.GetFileLineNumber();
                contaSteps++;
                string file = frame.GetFileName()?? "NOT AVAILABLE";
                contaSteps++;
                log4net.Core.LoggingEventData eventData = new log4net.Core.LoggingEventData();
                contaSteps++;
                step = "AppDomain.CurrentDomain.FriendlyName";
                eventData.Domain = AppDomain.CurrentDomain.FriendlyName;
                contaSteps++;
                step = "exception";
                if (exception != null) eventData.ExceptionString = exception.ToString();
                contaSteps++;
                step = "UserName";
                eventData.UserName = System.Threading.Thread.CurrentPrincipal.Identity.Name;
                contaSteps++;
                step = "eventData.UserName";
                eventData.Identity = eventData.UserName;
                contaSteps++;
                step = "level";
                eventData.Level = level;
                contaSteps++;
                step = "methodBase";
                if (methodBase != null) {
                    string methodBaseName= (methodBase.Name??"NOT AVAILABLE");
                    contaSteps++;
                    step = "methodBaseName";
                    string methodBaseDeclaringType = (methodBase.DeclaringType!= null? methodBase.DeclaringType.Name : "NOT AVAILABLE");
                    contaSteps++;
                    step = "methodBaseDeclaringType";
                    contaSteps++;
                    eventData.LocationInfo = new log4net.Core.LocationInfo(methodBaseDeclaringType, methodBaseName, file, lineNumeber.ToString());
                    step = "eventData.LocationInfo";
                }
                contaSteps++;
                step = "loggerName";
                eventData.LoggerName = loggerName;
                contaSteps++;
                step = "message";
                eventData.Message = message.ToString();
                contaSteps++;
                step = "ThreadName";
                eventData.ThreadName = System.Threading.Thread.CurrentThread.Name ?? string.Empty;
                contaSteps++;
                step = "TimeStamp";
                eventData.TimeStamp = System.DateTime.Now;
                contaSteps++;
                step = "End---";
                return eventData;
            }
            catch (Exception ex) {
                log4net.Core.LoggingEventData eventData = new log4net.Core.LoggingEventData();
                logueador.Error(string.Format("[getEventData]:: {0}", contaSteps), ex);
                eventData.Domain = "Herbalife.SAM-CAM.Framework.Tools.Utilities";
                eventData.ExceptionString = ex.ToString();
                eventData.UserName = "NOT AVAILABLE";
                eventData.Identity = "NOT AVAILABLE"; ;
                eventData.Level = log4net.Core.Level.Error;
                eventData.LoggerName = loggerName;
                eventData.Message = string.Format("Herbalife.SAM-CAM.Framework.Tools.Utilities.LoggerUtility.getEventData Error (contaSteps:{0} - {3}) - ORIGINAL MESSAGE: {1} -::- \n\n{2}", contaSteps, message.ToString(), (methodBase != null ? methodBase.ToString() : "methodBase is null"), step);
                eventData.ThreadName = string.Empty;
                eventData.TimeStamp = System.DateTime.Now;
                return eventData;
            }
        }

        #region "Async"

        //public static void AsyncDebug(object message)
        //{
        //    DebugDelegate work = new DebugDelegate(LoggerUtility.Debug);
        //    AsyncCallback OperationComplete = delegate(IAsyncResult result)
        //    {
        //        work.EndInvoke(result);
        //    };
        //    work.BeginInvoke(message, OperationComplete, null);
        //}

        #region "Debug"

        public static void AsyncDebug(object message)
        {
            AsyncLog(log4net.Core.Level.Debug, message, null);
        }

        public static void AsyncDebug(object message, Exception exception)
        {
             AsyncLog(log4net.Core.Level.Debug, message, exception);
        }

        public static void AsyncDebugFormat(string format, params object[] args)
        {
            AsyncLog(log4net.Core.Level.Debug, string.Format(format, args), null);
        }

        public static void AsyncDebugFormat(IFormatProvider provider, string format, params object[] args)
        {
            AsyncLog(log4net.Core.Level.Debug, string.Format(provider, format, args), null);
        }

        #endregion

        #region "Info"

        public static void AsyncInfo(object message)
        {
            AsyncLog(log4net.Core.Level.Info, message, null);
        }

        public static void AsyncInfo(object message, Exception exception)
        {
            AsyncLog(log4net.Core.Level.Info, message, exception);
        }

        public static void AsyncInfoFormat(string format, params object[] args)
        {
            AsyncLog(log4net.Core.Level.Info, string.Format(format, args), null);
        }

        public static void AsyncInfoFormat(IFormatProvider provider, string format, params object[] args)
        {
            AsyncLog(log4net.Core.Level.Info, string.Format(provider, format, args), null);
        }

        #endregion

        #region "Warn"

        public static void AsyncWarn(object message)
        {
            AsyncLog(log4net.Core.Level.Warn, message, null);
        }

        public static void AsyncWarn(object message, Exception exception)
        {
            AsyncLog(log4net.Core.Level.Warn, message, exception);
        }

        public static void AsyncWarnFormat(string format, params object[] args)
        {
            AsyncLog(log4net.Core.Level.Warn, string.Format(format, args), null);

        }

        public static void AsyncWarnFormat(IFormatProvider provider, string format, params object[] args)
        {
            AsyncLog(log4net.Core.Level.Warn, string.Format(provider, format, args), null);
        }

        #endregion

        #region "Error"

        public static void AsyncError(object message)
        {
            AsyncLog(log4net.Core.Level.Error, message, null);
        }

        public static void AsyncError(object message, Exception exception)
        {
            AsyncLog(log4net.Core.Level.Error, message, exception);
        }

        public static void AsyncErrorFormat(string format, params object[] args)
        {
            AsyncLog(log4net.Core.Level.Error, string.Format(format, args), null);
        }

        public static void AsyncErrorFormat(IFormatProvider provider, string format, params object[] args)
        {
            AsyncLog(log4net.Core.Level.Error, string.Format(provider, format, args), null);
        }

        #endregion

        #region "Fatal"

        public static void AsyncFatal(object message)
        {
            AsyncLog(log4net.Core.Level.Fatal, message, null);
        }

        public static void AsyncFatal(object message, Exception exception)
        {
            AsyncLog(log4net.Core.Level.Fatal, message, exception);
        }

        public static void AsyncFatalFormat(string format, params object[] args)
        {
            AsyncLog(log4net.Core.Level.Fatal, string.Format(format, args), null);
        }

        public static void AsyncFatalFormat(IFormatProvider provider, string format, params object[] args)
        {
            AsyncLog(log4net.Core.Level.Fatal, string.Format(provider, format, args), null);

        }

        #endregion
        
        private static object _lock = new object();
        private static Queue<log4net.Core.LoggingEventData> _asyncQueue;
        private static Thread _asyncThread;
        private static bool _asyncFlag = true;
        private static ManualResetEvent _asyncEvent;


        /// <summary>
        /// Logs a message and exception as specific log kind [async]
        /// </summary>
        /// <param name="sender">sender object or class if static type</param>
        /// <param name="exception">exception to log</param>
        /// <param name="message">message to log</param>
        /// <param name="kind">kind of log</param>
        /// <param name="contextID">reference to the logging context</param>
        /// <param name="traceIdentifier">reference to the debug context</param>        
        public static void AsyncLog(log4net.Core.Level level, object message, Exception exception)
        {
            lock (_lock)
            {
                if (_asyncQueue == null)
                {
                    _asyncQueue = new Queue<log4net.Core.LoggingEventData>();
                    _asyncEvent = new ManualResetEvent(false);
                    _asyncThread = new Thread(AsyncThreadExec);
                    _asyncThread.Start();
                }

                StackFrame frame = new StackTrace(3, true).GetFrame(0);
                MethodBase methodBase = frame.GetMethod();
                log4net.Core.LoggingEventData eventData = getEventData(level, message, exception, frame, methodBase);

                _asyncQueue.Enqueue(eventData);

                _asyncEvent.Set(); //-->Envía una señal a los threads q están esperando....
            }
        }

        private static void AsyncThreadExec()
        {
            while (_asyncFlag)
            {
                _asyncEvent.WaitOne();  //--> Espera hasta q el thread reciba una señal, en este caso recibe la señal del  _asyncEvent.Set();
                lock (_lock)
                {
                    while (_asyncQueue.Count > 0)
                    {
                        log4net.Core.LoggingEventData eventData = _asyncQueue.Dequeue();
                        log4net.Core.LoggingEvent le = new log4net.Core.LoggingEvent(eventData);
                        logueador.Logger.Log(le);
                    }
                    _asyncEvent.Reset(); //-->Vuelve a bloquear los threads y los deja a la espera de la próxima señal....
                }
                Thread.Sleep(1);
            }
        }

        #endregion

    }

}