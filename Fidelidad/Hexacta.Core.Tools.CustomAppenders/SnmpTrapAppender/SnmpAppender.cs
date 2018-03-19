
namespace Hexacta.Core.Tools.CustomAppenders
{

    using Lextm.SharpSnmpLib;
    using Lextm.SharpSnmpLib.Messaging;
    using System;
    using System.Collections.Generic;
    using System.Net;
    using System.Text;

    public class SnmpAppender : log4net.Appender.AppenderSkeleton
    {
        private DateTime _startedTime = DateTime.MinValue;
        private bool isIntialized;
        private IPAddress localAgentServerIp;
        private IPAddress managementServerIp;

        public SnmpAppender()
        {
            this.ApplicationTrapOid = "1.3.6.1.4.1.24.12.10.22.64";
            this.EnterpriseOid = "1.3.6.1.4.1.24.0";
            this.CommunityString = "public";
            this.GenericTrapType = ((GenericCode) 6).ToString();
            this.SpecificTrapType = 0;
            this.Version = "V1";
            this.ManagementServerListenPort = 0xa1;
        }

        protected override void Append(log4net.Core.LoggingEvent loggingEvent)
        {
            this.Intialize();
            string message = base.RenderLoggingEvent(loggingEvent);
            this.SendInternal(message);
        }

        protected override void Append(log4net.Core.LoggingEvent[] loggingEvents)
        {
            this.Intialize();
            StringBuilder builder = new StringBuilder();
            foreach (log4net.Core.LoggingEvent event2 in loggingEvents)
            {
                builder.Append(base.RenderLoggingEvent(event2)).AppendLine();
            }
            this.SendInternal(builder.ToString());
        }

        private uint GetSystemUptime()
        {
            return (uint) DateTime.Now.Subtract(this._startedTime).TotalMilliseconds;
        }

        protected void Intialize()
        {
            if (!this.isIntialized)
            {
                try
                {
                    IPAddress[] hostAddresses = Dns.GetHostAddresses(this.ManagementServerAddress);
                    this.managementServerIp = (hostAddresses.Length > 0) ? hostAddresses[0] : IPAddress.Loopback;
                    IPAddress[] addressArray2 = Dns.GetHostAddresses(this.LocalAgentAddress);
                    this.localAgentServerIp = (addressArray2.Length > 0) ? hostAddresses[0] : IPAddress.Loopback;
                    this.isIntialized = true;
                    this._startedTime = DateTime.Now;
                }
                catch (Exception exception)
                {
                    string message = string.Format("Could not initialize Snmp Appender. Cause: {0}", exception.Message);
                    this.ErrorHandler.Error(message, exception);
                }
            }
        }

        private void SendInternal(string message)
        {
            if (this.isIntialized)
            {
                try
                {
                    List<Variable> variables = new List<Variable>();
                    OctetString str = new OctetString(message);
                    variables.Add(new Variable(new ObjectIdentifier(this.ApplicationTrapOid), str));
                    if (this.Version.CompareTo("V1") != 0)
                    {
                        if (this.Version.CompareTo("V2") != 0)
                        {
                            throw new NotSupportedException("Unsupported exception only 'V1' and 'V2' are supported");
                        }
                        Messenger.SendTrapV2(0, VersionCode.V2, new IPEndPoint(this.managementServerIp, this.ManagementServerListenPort), new OctetString(this.CommunityString), new ObjectIdentifier(this.EnterpriseOid), this.GetSystemUptime(), variables);
                    }
                    else
                    {
                        Messenger.SendTrapV1(new IPEndPoint(this.managementServerIp, this.ManagementServerListenPort), this.localAgentServerIp, new OctetString(this.CommunityString), new ObjectIdentifier(this.EnterpriseOid), (GenericCode) System.Enum.Parse(typeof(GenericCode), this.GenericTrapType), this.SpecificTrapType, this.GetSystemUptime(), variables);
                    }
                }
                catch (Exception exception)
                {
                    string str2 = string.Format("An error occurred while connecting to the logging service. Cause: {0}", exception.Message);
                    this.ErrorHandler.Error(str2, exception);
                }
            }
            else
            {
                this.ErrorHandler.Error("Appender is not correctly initialized");
            }
        }

        public string ApplicationTrapOid { get; set; }

        public string CommunityString { get; set; }

        public string EnterpriseOid { get; set; }

        public string GenericTrapType { get; set; }

        public string LocalAgentAddress { get; set; }

        public string ManagementServerAddress { get; set; }

        public int ManagementServerListenPort { get; set; }

        public int SpecificTrapType { get; set; }

        public string Version { get; set; }
    }
}

