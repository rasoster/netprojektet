using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;

using DataAccessLayer;
using Models;

namespace netprojektet.Controllers
{
    public class MessageController : Controller
    {
        private LinkedoutDbContext _DbContext;

        public MessageController(LinkedoutDbContext linkedoutDbContext)
        {
            _DbContext = linkedoutDbContext;
        }
        public IActionResult Message()
        {
            
            int profileid = (from p in _DbContext.Profiles
                           where p.UserName == User.Identity.Name
                           select p.Id).FirstOrDefault();
            
            List<Message> messages = _DbContext.Messages.Where(m => m.Reciever == profileid).ToList();
            return View(messages);

        }
    }
}
