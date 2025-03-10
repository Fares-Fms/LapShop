﻿using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Lap_Shop.Models
{
    public class VmEditUser
    {
        public string userid { get; set; }
        [Required]
        public string username { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string selectedRole { get; set; }
        [PasswordPropertyText]
        public string password { get; set; }
        public List<SelectListItem>? roles { get; set; }
    }
}
