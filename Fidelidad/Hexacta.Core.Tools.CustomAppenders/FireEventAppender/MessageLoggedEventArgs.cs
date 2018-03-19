namespace Hexacta.Core.Tools.CustomAppenders
{
    using System;

    public class MessageLoggedEventArgs : EventArgs
    {
        private log4net.Core.LoggingEvent m_loggingEvent;

        public MessageLoggedEventArgs(log4net.Core.LoggingEvent loggingEvent)
        {
            this.m_loggingEvent = loggingEvent;
        }

        public log4net.Core.LoggingEvent LoggingEvent
        {
            get
            {
                return this.m_loggingEvent;
            }
        }
    }
}
