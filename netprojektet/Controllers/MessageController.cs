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


            return View("Message");

        }
    }
}
