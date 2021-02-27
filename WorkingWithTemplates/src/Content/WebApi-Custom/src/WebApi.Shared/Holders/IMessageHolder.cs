using System;
using System.Collections.Generic;
using System.Net;

namespace WebApi.Shared.Holders
{
    public interface IMessageHolder
    {
        HttpStatusCode StatusCode { get; }

        void AddMessage(HttpStatusCode code, string content);

        void AddException(Exception exception);

        bool Any();

        string StringifyMessages();

        IEnumerable<(HttpStatusCode Code, string Content)> All();
    }
}