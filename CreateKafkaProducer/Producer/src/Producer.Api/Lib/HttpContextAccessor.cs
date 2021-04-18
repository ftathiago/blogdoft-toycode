using Producer.Shared.Holders;
using System;

namespace Producer.Api.Lib
{
    public class HttpContextAccessor : IHttpContextAccessor
    {
        public Guid CorrelationId { get; set; }
    }
}
