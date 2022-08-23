using System.Threading.Tasks;
namespace Test.Repositories
{
    public interface IAdvicesRepository
    {
        
        Task<Advices> GetAdvices(string topic, int? amount);

    }
}