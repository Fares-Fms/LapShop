using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Lap_Shop.Models;
using System.Linq;
using System.Threading.Tasks;

namespace Lap_Shop.Controllers
{
    public class ChatController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _context;

        public ChatController(UserManager<ApplicationUser> userManager, ApplicationDbContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            // Get the current logged-in user
            var currentUser = await _userManager.GetUserAsync(User);
            if (currentUser == null)
            {
                return RedirectToAction("Login", "Account"); // Redirect if user is not logged in
            }

            // Retrieve all users except the current user for selection
            var users = _context.AspNetUsers
                .Where(u => u.Id != currentUser.Id)
                .OrderBy(u => u.UserName)
                .ToList();

            ViewBag.CurrentUserId = currentUser.Id;
            ViewBag.CurrentUserName = currentUser.UserName;
            ViewBag.Users = users;

            return View();
        }
    }
}