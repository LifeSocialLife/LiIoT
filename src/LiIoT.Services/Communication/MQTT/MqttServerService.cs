// <summary>
// MqttServerService.
// </summary>
// <copyright file="MqttServerService.cs" company="LiSoLi">
// Copyright (c) LiSoLi. All rights reserved.
// </copyright>
// <author>Lennie Wennerlund (lempa)</author>

namespace LiIoT.Services.Communication.MQTT
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Text;
    using LiIoT.Services.Contracts;
    using LiIoT.Services.Core;
    using LiIoT.Services.Db;
    using Microsoft.Extensions.Logging;
    using MQTTnet;
    using MQTTnet.Adapter;
    using MQTTnet.Client.Receiving;
    using MQTTnet.Diagnostics;
    using MQTTnet.Implementations;
    using MQTTnet.Protocol;
    using MQTTnet.Server;
    using MQTTnet.Server.Status;

    /// <summary>
    /// MQTT server (BROKER).
    /// </summary>
    [SuppressMessage("StyleCop.CSharp.ReadabilityRules", "SA1124:DoNotUseRegions", Justification = "Reviewed.")]
    public class MqttServerService : LiIoTCoreService
    {
#pragma warning disable SA1309 // FieldNamesMustNotBeginWithUnderscore
        private readonly ILogger<MqttServerService> _logger;
        private readonly RunDataService _rundata;
        private readonly ConfigFileService _configfile;
        private readonly SystemCancellationTokenService _systemCancellationToken;

        private IMqttServer? _mqttServer;
#pragma warning restore SA1309 // FieldNamesMustNotBeginWithUnderscore

        /// <summary>
        /// Initializes a new instance of the <see cref="MqttServerService"/> class.
        /// </summary>
        /// <param name="logger">ILogger.</param>
        /// <param name="rundataService">RunDataService.</param>
        /// <param name="configFileService">ConfigFileService.</param>
        /// <param name="systemCancellationTokenService">SystemCancellationTokenService.</param>
        public MqttServerService(
            ILogger<MqttServerService> logger,
            RunDataService rundataService,
            ConfigFileService configFileService,
            SystemCancellationTokenService systemCancellationTokenService)
        {
            this._logger = logger;
            this._rundata = rundataService;
            this._configfile = configFileService;
            this._systemCancellationToken = systemCancellationTokenService;
            this.zzDebug = "MqttServerService";
            this.IsRunning = false;
        }

        /// <summary>
        /// Gets or sets a value indicating whether is this running?.
        /// </summary>
        public bool IsRunning { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("CodeQuality", "IDE0052:Remove unread private members", Justification = "Reviewed.")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "Reviewed.")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.NamingRules", "SA1300:ElementMustBeginWithUpperCaseLetter", Justification = "Reviewed.")]
        private string zzDebug { get; set; }

        /// <summary>
        /// Do init of the service.
        /// </summary>
        /// <returns>Status of the init. if true everthing is ok.</returns>
        public bool Init()
        {
            this.zzDebug = "sdfdsf";
            return true;
        }

        /// <summary>
        /// Run check.
        /// </summary>
        public void Check()
        {
            if (!this.IsRunning)
            {
                var mqttFactory = new MqttFactory();

                this._mqttServer = mqttFactory.CreateMqttServer();

                this.zzDebug = "Sdfdsf";

                this._mqttServer.UseApplicationMessageReceivedHandler(new MqttApplicationMessageReceivedHandlerDelegate(e => this.OnApplicationMessageReceived(e)));

                this.zzDebug = "sdfdsf";

                this._mqttServer.ClientSubscribedTopicHandler = new MqttServerClientSubscribedHandlerDelegate(e =>
                   this.TopicSubscribedHandler(e));

                this.zzDebug = "sdfdsf";

                this._mqttServer.ClientUnsubscribedTopicHandler = new MqttServerClientUnsubscribedTopicHandlerDelegate(e => this.TopicUnSubscribedHandler(e));

                this.zzDebug = "sdfdsf";

                this.OnStart();
            }
        }

        /// <summary>
        /// Start the mqtt server.
        /// </summary>
        protected override void OnStart()
        {
            this.zzDebug = "sdfdsf";

            var serverOptions = new MqttServerOptionsBuilder()
               .WithDefaultEndpointPort(1883) // .WithDefaultEndpointPort(_options.ServerPort)
               .WithConnectionValidator(this.ValidateClientConnection)
               .WithSubscriptionInterceptor(this.TopicSubscribedInterceptor)
               .WithApplicationMessageInterceptor(this.TopicMessageInterceptor)
               .WithPersistentSessions();

            this._mqttServer?.StartAsync(serverOptions.Build()).GetAwaiter().GetResult();

            this._systemCancellationToken.Token.Register(() =>
            {
                this._mqttServer?.StopAsync().GetAwaiter().GetResult();
            });

            // Use this to run background (task) work.
            // ParallelTask.StartLongRunning(ProcessIncomingMqttMessages, this._systemCancellationToken.Token, this._logger);
            this.zzDebug = "sdfdsf";

            this.IsRunning = true;
        }

        private void TopicSubscribedHandler(MqttServerClientSubscribedTopicEventArgs eventarg)
        {
            this.zzDebug = "sdfdsf";

            // eventarg.TopicFilter.
            this.zzDebug = "sdefsd";
        }

        private void TopicUnSubscribedHandler(MqttServerClientUnsubscribedTopicEventArgs eventarg)
        {
            this.zzDebug = "sddfdsf";
        }

        private void OnApplicationMessageReceived(MqttApplicationMessageReceivedEventArgs eventArgs)
        {
            this.zzDebug = "Sdfdsf";

            // eventArgs.ReasonCode = MqttApplicationMessageReceivedReasonCode.NotAuthorized;
            // _inboundCounter.Increment();
            // _incomingMessages.Add(eventArgs);
        }

        #region MQTT Interceptor.

        private void TopicMessageInterceptor(MqttApplicationMessageInterceptorContext mess)
        {
            this.zzDebug = "sfsdf";
            mess.AcceptPublish = true;
        }

        private void TopicSubscribedInterceptor(MqttSubscriptionInterceptorContext e)
        {
            this.zzDebug = "sdfdsf";

            e.AcceptSubscription = true;
        }

        private void ValidateClientConnection(MqttConnectionValidatorContext context)
        {
            this.zzDebug = "sdfdsf";

            // TODO check that user have access to connec to this mqtt broker.
            context.ReasonCode = MQTTnet.Protocol.MqttConnectReasonCode.Success;

            /*
             * context.ReasonCode = MQTTnet.Protocol.MqttConnectReasonCode.Banned;
            if (_options.BlockedClients == null)
            {
                return;
            }

            if (_options.BlockedClients.Contains(context.ClientId ?? string.Empty))
            {
                context.ReasonCode = MQTTnet.Protocol.MqttConnectReasonCode.Banned;
            }
            */
        }

        #endregion
    }
}
