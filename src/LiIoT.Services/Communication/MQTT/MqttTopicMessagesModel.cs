// <summary>
// MqttTopicMessagesModel.
// </summary>
// <copyright file="MqttTopicMessagesModel.cs" company="LiSoLi">
// Copyright (c) LiSoLi. All rights reserved.
// </copyright>
// <author>Lennie Wennerlund (lempa)</author>

namespace LiIoT.Services.Communication.MQTT
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    /// <summary>
    /// Information about all messages that is comming thow mqtt server.
    /// </summary>
    public class MqttTopicMessagesModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MqttTopicMessagesModel"/> class.
        /// </summary>
        public MqttTopicMessagesModel()
        {
            this.DtAdded = DateTime.UtcNow;
            this.ClientId = string.Empty;
            this.Message = string.Empty;
        }

        /// <summary>
        /// Gets or sets datetime when it as added.
        /// </summary>
        public DateTime DtAdded { get; set; }

        /// <summary>
        /// Gets or sets client id this message belongs to.
        /// </summary>
        public string ClientId { get; set; }

        /// <summary>
        /// Gets or sets message from mqtt client posting.
        /// </summary>
        public string Message { get; set; }
    }
}
