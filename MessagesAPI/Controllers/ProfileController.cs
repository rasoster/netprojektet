
using Microsoft.AspNetCore.Mvc;
using Models;

namespace MessagesAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    
    public class ProfileController : ControllerBase
    {
        private LinkedoutDbContext linkedoutDbContext;

        public ProfileController(LinkedoutDbContext linkedoutDbContext)
        {
            this.linkedoutDbContext = linkedoutDbContext;
        }

        [HttpGet]
        public List<Profile> GetProfiles()
        {
            return linkedoutDbContext.Profiles.ToList();
        }
        [HttpGet("{id}")]
        public Profile Get(int id)
        {
            
            return linkedoutDbContext.Profiles.Find(id);
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
            
                linkedoutDbContext.Messages.Update(message);
                linkedoutDbContext.SaveChanges();
            
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