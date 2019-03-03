using Sample.Domain.Entities;
using System.Threading.Tasks;

namespace Sample.Domain.Abstract
{
    /// <summary>
    /// A service which can provide a <c>Greeting</c> object.
    /// </summary>
    public interface IGreetingService
    {
        /// <summary>
        /// Get the current greeting.
        /// </summary>
        Task<Greeting> GetGreetingAsync();
    }
}
