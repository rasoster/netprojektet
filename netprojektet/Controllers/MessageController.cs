﻿using Microsoft.AspNetCore.Mvc;
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
        public IActionResult MessageSeen (int itemid)
        {
            Message message = _DbContext.Messages.Find(itemid);
            message.Seen = true;
            _DbContext.Messages.Update(message);
            _DbContext.SaveChanges();
            
            return RedirectToAction("Message");
        }
    
        public IActionResult MessageUnSeen (int itemid) 
        {
            Message message = _DbContext.Messages.Find(itemid);
            message.Seen = false;
            _DbContext.Messages.Update(message);
            _DbContext.SaveChanges();

            return RedirectToAction("Message");
        }
        
        public IActionResult NewMessage (int profileid) 
        {
            Message message = new Message(); 

            message.SenderName = (from p in _DbContext.Profiles
                                 where p.UserName == User.Identity.Name
                                 select p.FirstName+ " " + p.LastName).FirstOrDefault();


            message.Reciever = profileid;
            //message.RecieverNavigation = _DbContext.Profiles.Find(profileid);
            message.Seen = false;
            message.Times = DateTime.Now;
            
            return View(message);
        }

        
        public IActionResult SendMessage (Message message)
        {
           
            _DbContext.Add(message);
            _DbContext.SaveChanges();



            return RedirectToAction ("Index", "Home");

        }



	}
}
