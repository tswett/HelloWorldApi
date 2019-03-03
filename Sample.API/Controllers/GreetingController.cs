using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Sample.Domain.Entities;
using Sample.Domain.Abstract;

namespace Sample.API.Controllers
{
    [Route("api/greeting")]
    [ApiController]
    public class GreetingController : ControllerBase
    {
        IGreetingService GreetingService { get; set; }

        /// <summary>
        /// Create a new <c>GreetingController</c> which uses the given <c>IGreetingService</c>.
        /// </summary>
        /// <param name="greetingService"></param>
        public GreetingController(IGreetingService greetingService)
        {
            GreetingService = greetingService;
        }

        // GET api/greeting
        [HttpGet]
        public async Task<ActionResult<Greeting>> Get()
        {
            Greeting result = await GreetingService.GetGreetingAsync();
            return result;
        }
    }
}
