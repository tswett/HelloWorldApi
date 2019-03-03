using Sample.Domain.Abstract;
using Sample.Domain.Entities;
using System.Threading.Tasks;

namespace Sample.Infrastructure
{
    /// <summary>
    /// A service which provides <c>Greeting</c>s that have the text "Hello World".
    /// </summary>
    public class GreetingService : IGreetingService
    {
        public async Task<Greeting> GetGreetingAsync()
        {
            return new Greeting("Hello World");
        }
    }
}
