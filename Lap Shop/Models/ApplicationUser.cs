﻿using Microsoft.AspNetCore.Identity;

namespace Lap_Shop.Models
{
    public class ApplicationUser :IdentityUser
    {
        public string Firstname { get; set; }
        public string Lastname { get; set; }
    }
}
