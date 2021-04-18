using System;

namespace Producer.Shared.Holders
{
    public interface IHttpContextAccessor
    {
        Guid CorrelationId { get; set; }
    }
}
