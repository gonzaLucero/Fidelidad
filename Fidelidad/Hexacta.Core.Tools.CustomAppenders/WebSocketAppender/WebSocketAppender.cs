using log4net.Appender;
using log4net.Core;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Quobject.SocketIoClientDotNet.Client;
using System;
using System.Threading;


namespace Hexacta.Core.Tools.CustomAppenders
{

    public class WebSocketAppender : AppenderSkeleton, System.IDisposable
    {
        private FixFlags m_fixFlags = FixFlags.All;
        private string m_serverUrl;
        
        public string ServerUrl
        {
            get { return m_serverUrl; }
            set { m_serverUrl = value; }
        }

        protected override void Append(LoggingEvent loggingEvent)
        {
            loggingEvent.Fix = this.Fix;
            //ManualResetEvent ManualResetEvent = null;
            Quobject.SocketIoClientDotNet.Client.Socket socket = IO.Socket(m_serverUrl); //new Client(m_serverUrl);
            ManualResetEvent ManualResetEvent = new ManualResetEvent(false);

            string output = JsonConvert.SerializeObject(new
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
            }, Newtonsoft.Json.Formatting.Indented, new Newtonsoft.Json.JsonSerializerSettings
            {
                CheckAdditionalContent = true,
                ConstructorHandling = Newtonsoft.Json.ConstructorHandling.AllowNonPublicDefaultConstructor,
                MetadataPropertyHandling = Newtonsoft.Json.MetadataPropertyHandling.ReadAhead,
                MissingMemberHandling = Newtonsoft.Json.MissingMemberHandling.Ignore,
                NullValueHandling = Newtonsoft.Json.NullValueHandling.Include,
                ObjectCreationHandling = Newtonsoft.Json.ObjectCreationHandling.Reuse,
                PreserveReferencesHandling = Newtonsoft.Json.PreserveReferencesHandling.All,
                ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Serialize
            });
            JObject jobj = JObject.Parse(output);
            try
            {
                socket.On(Socket.EVENT_CONNECT, () =>
                {
                    //socket.Emit("nickname", Environment.MachineName);
                    socket.Emit("sendMessage", jobj);
                    ManualResetEvent.Set();
                });
                ManualResetEvent.WaitOne(5000, true);
                socket.Disconnect();
                socket.Close();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Trace.TraceError("<<<< WebSocketAppender ERROR >>>> {0}", ex.ToString());
            }
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
                    //socket.Dispose();
                }
            }
            this.disposed = true;
        }

        /// <summary>
        /// Destructor de la instancia
        /// </summary>
        ~WebSocketAppender()
        {
            this.Dispose(false);
        }
        #endregion
    }
}