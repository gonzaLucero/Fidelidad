namespace Hexacta.Core.Tools.CustomAppenders
{
    using log4net.Appender;
    using log4net.Core;

    public class FireEventAppender : AppenderSkeleton
    {
        private FixFlags m_fixFlags = FixFlags.All;
        private static FireEventAppender m_instance;

        public event MessageLoggedEventHandler MessageLoggedEvent;

        public FireEventAppender()
        {
            m_instance = this;
        }

        protected override void Append(LoggingEvent loggingEvent)
        {
            loggingEvent.Fix = this.Fix;
            MessageLoggedEventHandler handler = this.MessageLoggedEvent;
            if (handler != null)
            {
                handler(this, new MessageLoggedEventArgs(loggingEvent));
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

        public static FireEventAppender Instance
        {
            get
            {
                return m_instance;
            }
        }
    }
}
