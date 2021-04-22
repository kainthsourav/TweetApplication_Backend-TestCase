using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace TA.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HomeController : ControllerBase
    {
  
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public JsonResult Get()
        {
            //var message = new Message(new string[] { "shivammanawat@gmail.com" }, "Test email", "This is the content from our email.");
            //_emailSender.SendEmail(message);
            return new JsonResult("Tweet App API Started");
        }
    }
}
