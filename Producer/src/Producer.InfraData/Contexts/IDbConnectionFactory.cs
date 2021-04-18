using System.Data;

namespace Producer.InfraData.Contexts
{
    public interface IDbConnectionFactory
    {
        IDbConnection GetNewConnection();
    }
}
