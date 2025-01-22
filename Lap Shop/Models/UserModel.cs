using System.ComponentModel.DataAnnotations;

namespace Lap_Shop.Models
{
    public class UserModel
    {
        public string Id { get; set; }
        
        public string UserName { get; set; } 
        public string FirstName { get; set; }
      
        public string lastName { get; set; }
        [EmailAddress]
   
        public string Email { get; set; }
        
        public string role {get; set; }
    }
}
