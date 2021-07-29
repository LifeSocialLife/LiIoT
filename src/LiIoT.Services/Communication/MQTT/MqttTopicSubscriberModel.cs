// <summary>
// MqttTopicSubscriberModel.
// </summary>
// <copyright file="MqttTopicSubscriberModel.cs" company="LiSoLi">
// Copyright (c) LiSoLi. All rights reserved.
// </copyright>
// <author>Lennie Wennerlund (lempa)</author>

namespace LiIoT.Services.Communication.MQTT
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    /// <summary>
    /// MQTT - Topic information.
    /// </summary>
    public class MqttTopicSubscriberModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MqttTopicSubscriberModel"/> class.
        /// </summary>
        public MqttTopicSubscriberModel()
        {
            this.Subscriber = new();
            this.Poster = new();
            this.Message = string.Empty;
            this.Messages = new();
        }

        /// <summary>
        /// Gets or sets clients subscribing to this topic.
        /// </summary>
        public List<string> Subscriber { get; set; }

        /// <summary>
        /// Gets or sets client posting to this tropic.
        /// </summary>
        public List<string> Poster { get; set; }

        /// <summary>
        /// Gets or sets last message postid to tropic.
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Gets or sets messages on this topic.
        /// </summary>
        public List<MqttTopicMessagesModel> Messages { get; set; }
    }
}
