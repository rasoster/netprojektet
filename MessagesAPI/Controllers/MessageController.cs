
using Microsoft.AspNetCore.Mvc;
using Models;

namespace MessagesAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MessageController : ControllerBase
    {
        private LinkedoutDbContext linkedoutDbContext;
    
    public MessageController(LinkedoutDbContext linkedoutDbContext)
    {
        this.linkedoutDbContext = linkedoutDbContext;
    }
    [HttpGet]
    public List<Message> getMessages()
        {
            return linkedoutDbContext.Messages.ToList();
        }


    }
}