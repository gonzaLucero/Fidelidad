  <!-- Appender for sending snmp traps -->
    <appender name="NetworkManagement" type="Hexacta.Core.Tools.CustomAppenders.SnmpAppender, Hexacta.Core.Tools.CustomAppenders">
      <ManagementServerAddress value="localhost" />
      <ManagementServerListenPort value="162" />
      <LocalAgentAddress value="127.0.0.1" />
      <EnterpriseOid value="1.3.6.1.4.1.24.0" />
      <!--Valid values are ColdStart, WarmStart, LinkDown, LinkUp, AuthenticationFailure, EgpNeighborLoss, EnterpriseSpecific -->
      <GenericTrapType value="EnterpriseSpecific" />
      <SpecificTrapType value="0" />
      <CommunityString value="public" />
      <ApplicationTrapOid value="1.3.6.1.4.1.24.12.10.22.64" />
      <!-- Valid versions are V1 and V2 -->
      <Version value="V1" />
      <threshold value="ALL" />
      <layout type="log4net.Layout.PatternLayout">
        <!-- Pattern to output the caller's file name and line number -->
        <conversionPattern value="%newline%property{log4net:HostName} ::%date%newline%-5level%newline%type.%method{1} thread:[%thread]%newline%message%newline%newline%exception%newline" />
        <!--<conversionPattern value="%date [%level] %type.%method{1} - %message" />-->
      </layout>
    </appender>