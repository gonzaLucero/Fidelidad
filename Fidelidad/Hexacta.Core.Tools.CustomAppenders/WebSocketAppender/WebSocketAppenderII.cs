using System;
using SocketIOClient;
using log4net.Appender;
using log4net.Core;

namespace Hexacta.Core.Tools.CustomAppenders
{

    public class WebSocketAppenderII : AppenderSkeleton, System.IDisposable
    {
        private FixFlags m_fixFlags = FixFlags.All;
        private static Client socket;
        private string m_serverUrl;

        public string ServerUrl
        {
            get { return m_serverUrl; }
            set { m_serverUrl = value; }
        }

        private void initializeSocket()
        {
            socket = new Client(m_serverUrl);
            socket.Opened += socket_Opened;
            //socket.Message += SocketMessage;
            socket.SocketConnectionClosed += socket_SocketConnectionClosed;
            socket.Error += socket_Error;
            socket.RetryConnectionAttempts = 1;
            socket.Connect();
        }

        #region "Socket Events"

        static void socket_Error(object sender, SocketIOClient.ErrorEventArgs e)
        {
            System.Diagnostics.Trace.WriteLine("<<<< ERROR >>>> {0}", e.ToString());
        }

        static void socket_SocketConnectionClosed(object sender, System.EventArgs e)
        {
            System.Diagnostics.Trace.WriteLine("<<<< Closed >>>>");
        }

        static void socket_Opened(object sender, System.EventArgs e)
        {
            System.Diagnostics.Trace.WriteLine("<<<< Connected >>>>");
        }

        private static void SocketMessage(object sender, MessageEventArgs e)
        {
            try
            {
                var elMensaje = e.Message.Json.Args;
                var elNombre = e.Message.Json.Name;
                Console.WriteLine(elMensaje != null ? elMensaje.ToString() : e.Message.Event.ToString());
            }
            catch (Exception ex)
            {
                System.Diagnostics.Trace.WriteLine(ex.ToString());
            }
        }

        #endregion

        protected override void Append(LoggingEvent loggingEvent)
        {
            loggingEvent.Fix = this.Fix;
            if (socket == null || !socket.IsConnected)
                initializeSocket();
            socket.Emit("nickname", Environment.MachineName);
            socket.Emit("sendMessage", new
            {
                HostName = Environment.MachineName,
                LoggrName = loggingEvent.LoggerName,
                Level = loggingEvent.Level.DisplayName,
                Message = loggingEvent.RenderedMessage,
                ThreadName = loggingEvent.ThreadName,
                TimeStamp = loggingEvent.TimeStamp,
                ClassName = loggingEvent.LocationInformation.ClassName,
                FileName = loggingEvent.LocationInformation.FileName,
                LineNumber = loggingEvent.LocationInformation.LineNumber,
                MethodName = loggingEvent.LocationInformation.MethodName,
                StackFrames = loggingEvent.LocationInformation.StackFrames,
                UserName = loggingEvent.UserName,
                ExceptionString = loggingEvent.GetExceptionString(),
                Domain = loggingEvent.Domain,
                Identity = loggingEvent.Identity,
                Propeties = loggingEvent.Properties
            });
        }

        public virtual FixFlags Fix
        {
            get
            {
                return this.m_fixFlags;
            }
            set
            {
                this.m_fixFlags = value;
            }
        }

        #region "Dispose"
        private Boolean disposed;

        /// <summary>
        /// Implementación de IDisposable. No se sobreescribe.
        /// </summary>
        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);// GC.SupressFinalize quita de la cola de finalización al objeto.
        }

        /// <summary>
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    socket.Dispose();
                }
            }
            this.disposed = true;
        }

        /// <summary>
        /// Destructor de la instancia
        /// </summary>
        ~WebSocketAppenderII()
        {
            this.Dispose(false);
        }
        #endregion
    }
}