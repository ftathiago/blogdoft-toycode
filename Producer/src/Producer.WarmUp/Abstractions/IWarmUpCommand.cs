using System.Threading.Tasks;

namespace Producer.WarmUp.Abstractions
{
    public interface IWarmUpCommand
    {
        Task Execute();
    }
}
