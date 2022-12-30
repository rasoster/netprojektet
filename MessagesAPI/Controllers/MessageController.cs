
using Microsoft.AspNetCore.Mvc;
using Models;

namespace MessagesAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    
    public class MessageController : ControllerBase
    {
        private LinkedoutDbContext linkedoutDbContext;

        public MessageController(LinkedoutDbContext linkedoutDbContext)
        {
            this.linkedoutDbContext = linkedoutDbContext;
        }

        [HttpGet]
        public List<Message> GetMessages()
        {
            return linkedoutDbContext.Messages.ToList();
        }
        [HttpGet("{id}")]
        public Message Get(int id)
        {
            return linkedoutDbContext.Messages.Find(id);
        }
        [HttpPost]
        public void Post(Message message)
        {
            
                linkedoutDbContext.Messages.Add(message);
                linkedoutDbContext.SaveChanges();
            
        }
        [HttpPut("{id}")]
        public void Put([FromBody] Message message) 
        {
            if(ModelState.IsValid)
            {
                linkedoutDbContext.Messages.Update(message);
                linkedoutDbContext.SaveChanges();
            }
        }
        [HttpDelete]
        public void Delete(int id) 
        {
            Message message = linkedoutDbContext.Messages.Find(id);
            linkedoutDbContext.Messages.Remove(message);
            linkedoutDbContext.SaveChanges();
        }




    }
}