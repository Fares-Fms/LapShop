using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;
using Lap_Shop.Models;
using System;

namespace Lap_Shop.Hubs
{
    public class ChatHub : Hub
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly LapShopContext _context;

        public ChatHub(UserManager<ApplicationUser> userManager, LapShopContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        public async Task SendMessage(string receiverUsername, string content)
        {
            try
            {
                // استخراج اسم المستخدم من السياق
                var senderUsername = Context.User.Identity.Name;

                if (string.IsNullOrEmpty(senderUsername) || string.IsNullOrEmpty(receiverUsername))
                {
                    throw new HubException("Sender or receiver username is null or empty.");
                }

                var sender = await _userManager.FindByNameAsync(senderUsername);
                var receiver = await _userManager.FindByNameAsync(receiverUsername);

                if (sender == null || receiver == null)
                {
                    throw new HubException("Invalid sender or receiver.");
                }

                var message = new TbMessages
                {
                    SenderId = sender.Id,
                    ReceiverId = receiver.Id,
                    Content = content,
                    SentAt = DateTime.UtcNow
                };

                _context.Messages.Add(message);
                await _context.SaveChangesAsync();

                await Clients.User(receiver.Id).SendAsync("ReceiveMessage", senderUsername, content);
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error in SendMessage: {ex.Message}");
                throw;
            }
        }
    }
}
