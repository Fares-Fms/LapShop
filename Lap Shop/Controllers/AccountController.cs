using Lap_Shop.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Lap_Shop.Controllers
{
    public class AccountController : Controller
    {
        UserManager<ApplicationUser> _userManager;
        SignInManager<ApplicationUser> _signInManager;
        LapShopContext CTX;
        public AccountController(UserManager<ApplicationUser> userManager,SignInManager<ApplicationUser> signInManager,LapShopContext context)
        {
            CTX = context;
            _userManager = userManager;
            _signInManager = signInManager;
        }
        public IActionResult Login(string? returnUrl)
        {
            if (User.Identity.IsAuthenticated)
            {
                return Redirect("~/");
            }
            else { 
            ViewData["ReturnUrl"]= returnUrl;
            return View(new LogInUser());
            }
        }
        [HttpPost]
        public async Task<IActionResult> Login(string? returnUrl,LogInUser model)
        {


            if (!ModelState.IsValid)
            {
                return View("Login", model);
            }
            ApplicationUser user = new ApplicationUser()
            {
              
                UserName = model.UserName,
          
            };
            try
            {
               

                var loginresult = await _userManager.FindByNameAsync(user.UserName);
                if (loginresult==null)
                {
                    ModelState.AddModelError(string.Empty, "the username does not exist");
                    return View(model);
                }
                var signInResult = await _signInManager.PasswordSignInAsync(model.UserName, model.Password, true, true);

                if (signInResult.Succeeded)
                {
                    if (string.IsNullOrEmpty(returnUrl))
                    {
                        return Redirect("~/");
                    }
                    else
                    {
                        return Redirect(returnUrl);
                    }
                }
                if(!await _userManager.CheckPasswordAsync(user, model.Password))
                {

                ModelState.AddModelError(string.Empty, "Incorrect password");
                    return View(model);
                }
                ModelState.AddModelError(string.Empty, "there is something wrong, please try again");



            }
            catch { }
            return View(new LogInUser());
        }
        public async Task<IActionResult >LogOut()
        {
            await _signInManager.SignOutAsync();
            return Redirect("~/");
        }
        public IActionResult Register(string returnUrl)
        {
            if (User.Identity.IsAuthenticated)
            {
                return Redirect("~/");
            }
            else
            {
                ViewData["ReturnUrl"] = returnUrl;
                return View(new RegisterModel());
            }
        }
        [HttpPost]
        public async Task< IActionResult> Register(string returnUrl, RegisterModel model)
        {
           /* if (!ModelState.IsValid) {
                return View("Register",model); }*/
            ApplicationUser user=new ApplicationUser()
            {
               
                UserName = model.UserName,
                Firstname = model.FirstName,
                Lastname = model.lastName,
                Email = model.Email,
               
            };
            try
            {
                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                var loginresult= await _signInManager.PasswordSignInAsync(user, model.Password, true, true);
                    UserModel userModel = new UserModel()
                    {
                        Id=user.Id,
                        UserName = model.UserName,
                        Email = model.Email,
                        FirstName = model.FirstName,
                        lastName =model.lastName,
                        role = "Customer"
                    };
                    CTX.TbUserModel.Add(userModel);
                   
                    var C_user = await _userManager.FindByEmailAsync(user.Email);
                    await _userManager.AddToRoleAsync(C_user, "Customer");
                    if (string.IsNullOrEmpty(returnUrl))
                    {
                        return Redirect("~/");
                    }
                    else
                    {
                        return Redirect(returnUrl);
                    }
                }
                else
                {

                }
            }
            catch { }
            return View(new RegisterModel());
        }

    }
}
