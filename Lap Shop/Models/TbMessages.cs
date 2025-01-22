using System;
using System.Collections.Generic;

namespace Lap_Shop.Models;

public partial class TbMessages
{
    
    public int Id { get; set; }
        public string SenderId { get; set; }
        public string ReceiverId { get; set; }
        public string Content { get; set; }
        public DateTime SentAt { get; set; } = DateTime.Now;
        public bool IsRead { get; set; } = false;
    }
