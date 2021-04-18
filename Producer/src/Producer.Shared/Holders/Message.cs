using System.Net;

namespace Producer.Shared.Holders
{
    public struct Message
    {
        public string Content { get; init; }

        public HttpStatusCode Code { get; init; }
    }
}