namespace Lap_Shop.Models
{
    public class VmUsers
    {
        public List<AspNetRole> aspNetRoles { get; set; } =new List<AspNetRole>();
        public List<AspNetUser> aspNetUsers { get; set; } = new List<AspNetUser>();
    }
}
