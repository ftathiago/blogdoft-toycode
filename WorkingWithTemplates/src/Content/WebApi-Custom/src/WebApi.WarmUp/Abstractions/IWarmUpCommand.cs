using System.Threading.Tasks;

namespace WebApi.WarmUp.Abstractions
{
    public interface IWarmUpCommand
    {
        Task Execute();
    }
}
