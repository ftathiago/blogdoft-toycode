using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using WebApi.Shared.Extensions;

namespace WebApi.Shared.Holders
{
    public class MessageHolder : IMessageHolder
    {
        private readonly List<Message> _messages;

        public MessageHolder() =>
            _messages = new List<Message>();

        public HttpStatusCode StatusCode { get; protected set; }

        public void AddMessage(HttpStatusCode code, string content)
        {
            _messages.Add(
                new Message
                {
                    Code = code,
                    Content = content,
                });

            StatusCode = code;
        }

        // You may build a exception parser to build a semantic HttpStatusCode.
        public void AddException(Exception exception) => AddMessage(
            HttpStatusCode.InternalServerError,
            exception.GetAllMessage(Environment.NewLine)
                    .ToString()
                    .Trim());

        public bool Any() => _messages.Count > 0;

        public string StringifyMessages() => _messages
            .Aggregate(new StringBuilder(), (sb, message) => sb.AppendLine(message.Content))
            .ToString()
            .Trim();

        public IEnumerable<(HttpStatusCode Code, string Content)> All() => _messages
            .Select(message => (message.Code, message.Content));
    }
}