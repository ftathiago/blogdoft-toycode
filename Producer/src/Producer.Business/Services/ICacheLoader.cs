using System.Threading.Tasks;

namespace Producer.Business.Services
{
    public interface ICacheLoader
    {
        Task Load();
    }
}
