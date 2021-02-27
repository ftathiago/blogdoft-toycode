using System.Net;

namespace WebApi.Shared.Holders
{
    public struct Message
    {
        public string Content { get; init; }

        public HttpStatusCode Code { get; init; }
    }
}