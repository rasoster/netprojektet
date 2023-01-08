using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using DataAccessLayer;
using Models;
using System.Text.Json;
using Microsoft.EntityFrameworkCore.Query.Internal;
using System.Text;


namespace netprojektet.Controllers
{
    public class MessageController : Controller
    {
        private LinkedoutDbContext linkedoutDbContext;
        private HttpClient httpClient;
       

        public MessageController(LinkedoutDbContext linkedoutDbContext,HttpClient httpClient)
        {
            this.linkedoutDbContext = linkedoutDbContext;
            this.httpClient = httpClient;
        }
        //"inkorg" för inloggad användare
        public async Task<IActionResult> Message()
        {
            ViewBag.Meddelanden = "Inkorg (" + linkedoutDbContext.Messages.Where(m => m.RecieverNavigation.UserName == User.Identity.Name && m.Seen == false).Count() + ")";

            //hämtar Json från API
            HttpResponseMessage response = await httpClient.GetAsync("Message");
            string data = await response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            
            List<Message> allMessages = JsonSerializer.Deserialize<List<Message>>(data, options);


            //tar fram användarens profilID och visar meddelanden tillhörande denne.
            int profileid = (from p in linkedoutDbContext.Profiles
                           where p.UserName == User.Identity.Name
                           select p.Id).FirstOrDefault();
            
            List<Message> messages = allMessages.Where(m => m.Reciever == profileid).OrderBy(m => m.Seen).ToList();
            return View(messages);

        }
        //markerar ett meddelande som läst/oläst
        public async Task<IActionResult> MessageRead(int itemid)
        {   //hämtar rätt meddelande objekt
            HttpResponseMessage response = await httpClient.GetAsync($"Message/{itemid}");

            string data = await response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };
            Message message = JsonSerializer.Deserialize<Message>(data,options);
            //ändrar .Seen
            if(message.Seen == true)
            {
                message.Seen = false;

            }
            else
            {
                message.Seen = true;
                
            }
            //Uppdaterar meddelandet med put.
            string putData = JsonSerializer.Serialize(message);
            var contentData = new StringContent(putData, System.Text.Encoding.UTF8, "application/json");

            HttpResponseMessage putResponse = await
                httpClient.PutAsync($"Message/{message.Id}", contentData);


            return RedirectToAction("Message");
        }
        //startar ett nytt meddelande till en användare
        public IActionResult NewMessage (int profileid) 
        {
            Message message = new Message(); 

            message.SenderName = (from p in linkedoutDbContext.Profiles
                                 where p.UserName == User.Identity.Name
                                 select p.FirstName+ " " + p.LastName).FirstOrDefault();


            message.Reciever = profileid;
            
            
            return View(message);
        }

        //skickar meddelandet.
        public async Task<IActionResult> SendMessage (Message message)
        {
            message.Seen = false;
            message.Times = DateTime.Now;

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };
            string data = JsonSerializer.Serialize(message, options);
            var contentData = new StringContent(data, System.Text.Encoding.UTF8,"application/json");

            HttpResponseMessage response = await
                httpClient.PostAsync($"Message", contentData);


            return RedirectToAction ("Index", "Home");

        }
        public async Task<IActionResult> DeleteMessage(int itemid)
        {
            var response = await httpClient.DeleteAsync($"Message/{itemid}");
          
            return RedirectToAction("Message");
         

        }



	}
}
