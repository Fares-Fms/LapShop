using Lap_Shop.Models;

public class MessageService
{
    private readonly LapShopContext _context;

    public MessageService(LapShopContext context)
    {
        _context = context;
    }

    public async Task SaveMessageAsync(string senderId, string receiverId, string content)
    {
        var message = new TbMessages
        {
            SenderId = senderId,
            ReceiverId = receiverId,
            Content = content
        };
        _context.Messages.Add(message);
        await _context.SaveChangesAsync();
    }
}