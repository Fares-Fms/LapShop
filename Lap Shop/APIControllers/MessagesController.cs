using Lap_Shop.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using System;
using System.Linq;

namespace Lap_Shop.APIControllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MessagesController : ControllerBase
    {
        private readonly LapShopContext _context;
        private readonly ApplicationDbContext Dbcontext;
        private readonly UserManager<ApplicationUser> _userManager;

        public MessagesController(LapShopContext context, ApplicationDbContext dbContext,UserManager<ApplicationUser> userManager)
        {
            _context = context;
            Dbcontext = dbContext;
            _userManager = userManager;
        }

        [HttpGet("{senderId}/{receiverId}")]
        public async Task<IActionResult> GetMessages(string senderId, string receiverId)
        {
            var sender = await _userManager.FindByNameAsync(senderId);
            var receiver = await _userManager.FindByNameAsync(receiverId);
            try
            {
                var messages = await _context.Messages
                    .Where(m => (m.SenderId == sender.Id && m.ReceiverId == receiver.Id) ||
                                (m.SenderId == receiver.Id && m.ReceiverId == sender.Id))
                    .OrderBy(m => m.SentAt)
                    .ToListAsync();

                if (messages == null || !messages.Any())
                {
                    return NotFound("No messages found between the users.");
                }

                return Ok(messages);
            }
            catch (Exception ex)
            {
                // تتبع الأخطاءdsfffds
                Console.Error.WriteLine("Error fetching messages: " + ex.Message);
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
