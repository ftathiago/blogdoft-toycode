using System.Data;

namespace WebApi.InfraData.Contexts
{
    public interface IDbConnectionFactory
    {
        IDbConnection GetNewConnection();
    }
}