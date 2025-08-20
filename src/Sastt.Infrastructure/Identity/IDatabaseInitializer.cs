using System.Threading.Tasks;

namespace Sastt.Infrastructure.Identity
{
    public interface IDatabaseInitializer
    {
        Task SeedAsync();
    }
}
