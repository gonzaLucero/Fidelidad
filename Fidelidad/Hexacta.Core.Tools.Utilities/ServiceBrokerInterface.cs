using Hexacta.Core.Tools.Utilities.Interfaces;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Xml.Linq;


namespace Hexacta.Core.Tools.Utilities.ServiceBroker
{
    [Serializable()]
    public class Message
    {
        public string MessageType { get; set; }
        public string RecivedMessage { get; set; }
        public XDocument RecivedXMLMessage { get; set; }
        public Guid ConversationGroup { get; set; }
        public Guid ConversationHandle { get; set; }
    }

    public delegate void ConnectionErrorEventHandler(object sender, ConnectionErrorEventArgs e);
    public class ConnectionErrorEventArgs : EventArgs
    {

        private Exception exception;

        public ConnectionErrorEventArgs(Exception exception)
        {
            this.exception = exception;
        }
        public Exception Exception
        {
            get { return exception; }
        }
    }

    public delegate void MessageArrivedEventHandler(object sender, MessageArrivedEventArgs e);
    public class MessageArrivedEventArgs : EventArgs
    {

        private Message message;

        public MessageArrivedEventArgs(Message message)
        {
            this.message = message;
        }
        public Message Message
        {
            get { return message; }
        }
    }

    public class ServiceBrokerInterface : IDisposable
    {
        const string RECIVE_SQL = "WAITFOR (RECEIVE TOP(1)  @msgtype = message_type_name, @msg = CONVERT(varchar(max), message_body), @cg = conversation_group_id, @dh = conversation_handle FROM [{0}]), TIMEOUT {1};";
        const string MESSAGE_TYPE_END_CONVERSATION = @"http://schemas.microsoft.com/SQL/ServiceBroker/EndDialog";
        private const string END_DIALOG_COMMAND = "END CONVERSATION @dh WITH CLEANUP";
        private bool dequeueInTransaction = false;

        public string QueueName { get; set; }
        public event ConnectionErrorEventHandler ConnectionErrorEvent;
        public event MessageArrivedEventHandler MessageArrivedEvent;

        private SqlConnection cnn;
        private SqlTransaction tran=null;
        private SqlCommand cmd;
        int timeOut = 10000;
        string reciveMessageType;
        private ConnectionStringSettings getConnectionString(string connectionName)
        {
            if (0 < ConfigurationManager.ConnectionStrings.Count)
            {
                ConnectionStringSettings connString = ConfigurationManager.ConnectionStrings[connectionName];
                if (null != connString)
                {
                    return connString;

                }
                else
                    throw new ApplicationException("No Connection String was provided.");
            }
            else
            {
                throw new ApplicationException("No Connection String was provided.");
            }
        }
        string connectionStringName = "";
        IValidator validator = null;

        public ServiceBrokerInterface(string ConnectionStringName, string queueName, string reciveMessageType, int timeOut)
        {
            Initialice(ConnectionStringName, queueName, reciveMessageType, timeOut);
        }

        public ServiceBrokerInterface(IValidator validator, string ConnectionStringName, string queueName, string reciveMessageType, int timeOut)
        {
            Initialice(ConnectionStringName, queueName, reciveMessageType, timeOut);
            this.validator = validator;
        }

        private void Initialice(string ConnectionStringName, string queueName, string reciveMessageType, int timeOut)
        {
            this.timeOut = timeOut;
            this.QueueName = queueName;
            this.reciveMessageType = reciveMessageType;
            this.connectionStringName = ConnectionStringName;

            bool.TryParse(ConfigurationManager.AppSettings["dequeueInTransaction"], out this.dequeueInTransaction);
        }

        public void Receive()
        {
            int maxConnectionAttemps;
            int.TryParse(ConfigurationManager.AppSettings["maxConnectionAttemps"], out maxConnectionAttemps);
            if (maxConnectionAttemps<1)
                maxConnectionAttemps = int.MaxValue;

            ConnectionStringSettings connstringSettings = getConnectionString(this.connectionStringName);
            LoggerUtility.Debug("SVC_DEBUG", (object)string.Format("-  Received Method::Started - Connection String: {0}", connstringSettings.Name));
            int conta = 0;
            Exception cnnError=null;
            while ((cnn == null || cnn.State != ConnectionState.Open) && conta<maxConnectionAttemps)
            {
                try
                {
                    LoggerUtility.Debug("SVC_DEBUG", (object)string.Format("-  Received Method [{2}]::Check Connection {1} - Connection State: {0}", (cnn == null ? "Null" : cnn.State.ToString()), (cnn == null ? "" : cnn.ClientConnectionId.ToString()), conta));
                    if (cnn != null)
                    {
                        cnn.Dispose();
                        cnn = null;
                    }

                    cnn = new SqlConnection(connstringSettings.ConnectionString);
                    cnn.Open();
                    if (this.dequeueInTransaction) { 
                        tran = cnn.BeginTransaction();
                    }
                    LoggerUtility.Debug("SVC_DEBUG", (object)string.Format("-  Received Method [{2}]::Connection Openned {1} - Connection State: {0}", (cnn == null ? "Null" : cnn.State.ToString()), (cnn == null ? "" : cnn.ClientConnectionId.ToString()), conta));
                }
                catch (Exception ex)
                {
                    cnnError = ex;
                    LoggerUtility.Error("SVC_ERROR", (object)string.Format("-  Received Method::ERROR - No se puede abrir la conexión (intentos: {3})- {0} - {1} - Connection: {2}", cnn.DataSource, cnn.Database, cnn.ClientConnectionId.ToString(), conta), ex);
                }
                finally {
                    conta++;

                }
            };

            LoggerUtility.Debug("SVC_DEBUG", (object)string.Format("-  Received Method::Started - DataSource: {0} - Database: {1} - Connection: {2}", cnn.DataSource, cnn.Database, cnn.ClientConnectionId.ToString()));
            cmd = cnn.CreateCommand();
            if (this.dequeueInTransaction && tran != null) { 
                cmd.Transaction = tran;
             }

            SqlParameter paramMsgType = new SqlParameter("@msgtype", SqlDbType.NVarChar, -1);
            paramMsgType.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(paramMsgType);


            SqlParameter paramMsg = paramMsg = new SqlParameter("@msg", SqlDbType.Xml, -1);

            paramMsg.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(paramMsg);


            SqlParameter paramConversationGroup = new SqlParameter("@cg", SqlDbType.UniqueIdentifier);
            paramConversationGroup.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(paramConversationGroup);


            SqlParameter paramDialogHandle = new SqlParameter("@dh", SqlDbType.UniqueIdentifier);
            paramDialogHandle.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(paramDialogHandle);

            cmd.CommandText = string.Format(RECIVE_SQL, QueueName, timeOut); //", TIMEOUT 5000";

            try
            {
                if (cnn.State != ConnectionState.Open)
                {
                    if (cnn != null)
                    {
                        cnn.Dispose();
                        cnn = null;
                    }

                    cnn = new SqlConnection(connstringSettings.ConnectionString);
                    cnn.Open();
                    if (this.dequeueInTransaction)
                    {
                        tran = cnn.BeginTransaction();
                    }
                }
                AsyncCallback callback = new AsyncCallback(CallbackHandler);
                cmd.BeginExecuteNonQuery(CallbackHandler, cmd);
                LoggerUtility.Debug("SVC_DEBUG", (object)string.Format("-  Received Method::Finished - {0} - {1} - Connection: {2}", cnn.DataSource, cnn.Database, cnn.ClientConnectionId.ToString()));
            }
            catch (Exception ex)
            {
                LoggerUtility.Error("SVC_ERROR", (object)string.Format("-  Received Method::ERROR - {0} - {1} - Connection: {2}", cnn.DataSource, cnn.Database, cnn.ClientConnectionId.ToString()), ex);
                throw;
            }
        }

        public void CancelReceive()
        {
            try
            {
                if (cmd.Connection.State == ConnectionState.Executing || cmd.Connection.State == ConnectionState.Fetching)
                    cmd.Cancel();

                if (cmd.Connection.State != ConnectionState.Closed)
                    cmd.Connection.Close();
            }
            catch
            {
            }
        }

        private void CallbackHandler(IAsyncResult asyncResult)
        {
            try
            {
                Message receivedMsg = new Message();
                SqlCommand command = asyncResult.AsyncState as SqlCommand;
                LoggerUtility.Debug("SVC_DEBUG", (object)string.Format("-  CallbackHandler:: Started - {0} - {1} - Connection: {2} - {3} ", command.Connection.DataSource, command.Connection.Database, command.Connection.ClientConnectionId.ToString(), command.Connection.State.ToString()));
                command.EndExecuteNonQuery(asyncResult);
                SqlParameter paramMsgType = command.Parameters["@msgtype"];
                SqlParameter paramMsg = command.Parameters["@msg"];
                SqlParameter paramConversationGroup = command.Parameters["@cg"];
                SqlParameter paramDialogHandle = command.Parameters["@dh"];

                if (paramMsgType.Value != DBNull.Value)
                {
                    LoggerUtility.Debug("SVC_DEBUG", (object)string.Format("-  CallbackHandler:: paramMsgType.Value == {2} - {0} - {1}  - Connection: {3} - {4} ", cnn.DataSource, cnn.Database, paramMsgType.Value, command.Connection.ClientConnectionId.ToString(), command.Connection.State.ToString()));
                    receivedMsg.MessageType = (string)paramMsgType.Value;
                    receivedMsg.RecivedMessage = (paramMsg.Value == DBNull.Value ? null : paramMsg.Value.ToString());
                    if ((receivedMsg.MessageType == this.reciveMessageType) && receivedMsg.RecivedMessage != null)
                    {
                        LoggerUtility.Debug("SVC_DEBUG", (object)string.Format("-  CallbackHandler:: paramMsgType.Value OK - {0} - {1} - Connection: {2} - {3} ", cnn.DataSource, cnn.Database, command.Connection.ClientConnectionId.ToString(), command.Connection.State.ToString()));
                        receivedMsg.RecivedXMLMessage = System.Xml.Linq.XDocument.Parse(receivedMsg.RecivedMessage);
                        receivedMsg.ConversationGroup = (System.Guid)paramConversationGroup.Value;
                        receivedMsg.ConversationHandle = (System.Guid)paramDialogHandle.Value;
                        string msgType = paramMsgType.Value.ToString();
                        cmd.Cancel();
                        if (this.validator != null && this.validator.Validate<string>(receivedMsg.RecivedMessage))
                        {
                            if (this.dequeueInTransaction && tran != null)
                            {
                                tran.Commit();
                            }
                        }
                        else
                        {

                            if (this.dequeueInTransaction && tran != null)
                            {
                                tran.Rollback();
                            }
                            LoggerUtility.Debug("SVC_ERROR",
                                (object)string.Format("-  CallbackHandler:: Validator.validate = False !!! - paramMsgType.Value OK - {0} - {1} - Connection: {2} - {3} ",
                                                        cnn.DataSource, cnn.Database, command.Connection.ClientConnectionId.ToString(), command.Connection.State.ToString()));

                        }
                        cmd.Connection.Close();
                        EndDialog(receivedMsg.ConversationHandle);
                        if (msgType != MESSAGE_TYPE_END_CONVERSATION)
                        {
                            LoggerUtility.Debug("SVC_DEBUG", (object)string.Format("-  CallbackHandler:: msgType != MESSAGE_TYPE_END_CONVERSATION - {0} - {1}", cnn.DataSource, cnn.Database));
                            MessageArrivedEventHandler handler = MessageArrivedEvent;
                            if (handler != null)
                            {
                                handler(this, new MessageArrivedEventArgs(receivedMsg));
                            }
                        }
                    }
                    else
                    {
                        LoggerUtility.Debug("SVC_DEBUG", (object)string.Format("-  CallbackHandler:: paramMsgType.Value NOT ALLOWED - {0} - {1}", cnn.DataSource, cnn.Database));

                        if (this.dequeueInTransaction && tran != null)
                        {
                            tran.Commit();
                        }
                        cmd.Cancel();
                        cmd.Connection.Close();
                        Receive();
                    }
                }
                else
                {
                    LoggerUtility.Debug("SVC_DEBUG", (object)string.Format("-  CallbackHandler:: paramMsgType.Value == DBNull.Value - {0} - {1}", cnn.DataSource, cnn.Database));
                    try
                    {
                        CancelReceive();
                    }
                    catch { }
                    finally
                    {
                        Receive();
                    }
                }

                LoggerUtility.Debug("SVC_DEBUG", (object)string.Format("-  CallbackHandler:: Finished - {0} - {1}", cnn.DataSource, cnn.Database));
            }
            catch (Exception ex)
            {
                LoggerUtility.Error("SVC_ERROR", (object)string.Format("-  CallbackHandler:: ERROR - {0} - {1}", cnn.DataSource, cnn.Database), ex);
                try {
                    if (this.dequeueInTransaction && tran != null)
                    {
                        tran.Rollback();
                    }
                } catch { }
                Receive();
            }
            finally
            {
            }
        }

        public void EndDialog(Guid ConversationHandle)
        {
            LoggerUtility.Debug("SVC_DEBUG", (object)string.Format("-  EndDialog Method:: Started - ConversationHandle: {0}", ConversationHandle));
            CancelReceive();
            if (cnn == null || cnn.State != ConnectionState.Open)
            {
                cnn = new SqlConnection(getConnectionString(this.connectionStringName).ConnectionString);
                cnn.Open();
            }

            SqlCommand cmd = cnn.CreateCommand();
            if (this.dequeueInTransaction && tran != null)
            {
                cmd.Transaction = tran;
            }

            SqlParameter paramDialogHandle = new SqlParameter("@dh", SqlDbType.UniqueIdentifier);
            paramDialogHandle.Value = ConversationHandle;
            cmd.Parameters.Add(paramDialogHandle);
            cmd.CommandText = END_DIALOG_COMMAND;
            try
            {
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                LoggerUtility.Error("SVC_ERROR", (object)string.Format("-  EndDialog Method:: EndDialog ERROR - ConversationHandle: {0}", ConversationHandle), ex);
            }
            finally
            {
                try
                {
                    cnn.Close();
                }
                catch { }
            }
            LoggerUtility.Debug("SVC_DEBUG", (object)string.Format("-  EndDialog Method:: Ending Dialog Finished - ConversationHandle: {0}", ConversationHandle));
        }

        public bool EnoughPermission()
        {
            SqlClientPermission perm = new SqlClientPermission(System.Security.Permissions.PermissionState.Unrestricted);
            try
            {
                perm.Demand();
                LoggerUtility.Debug("SVC_DEBUG", (object)"-  EnoughPermission::True -");
                return true;
            }
            catch (System.Exception ex)
            {
                LoggerUtility.Error("SVC_ERROR", (object)"-  EnoughPermission::False -",ex);
                return false;
            }
        }

        #region IDisposable Members

        public void Dispose()
        {
            try
            {
                CancelReceive();
                cmd.Dispose();
                cnn.Dispose();
            }
            catch { }

        }

        #endregion
    }
}
