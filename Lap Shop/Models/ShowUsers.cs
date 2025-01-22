namespace Lap_Shop.Models
{
    public class ShowUsers
    {
        public string Id { get; set; } = null!;

        public string? UserName { get; set; }

        public string? NormalizedUserName { get; set; }

        public string? Email { get; set; }
        public string? FirstName { get; set; }

        public string? LastName { get; set; }
        public string? Role { get; set;}
    }
}
