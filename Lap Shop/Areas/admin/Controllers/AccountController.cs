using Lap_Shop.BL;
using Lap_Shop.Controllers;
using Lap_Shop.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.IdentityModel.Tokens;
using System.Data;

namespace Lap_Shop.Areas.admin.Controllers
{
    [Area("admin")]
    public class AccountController : Controller
    {
        IUser User;
        UserManager<ApplicationUser> _userManager;
        RoleManager<IdentityRole> _roleManager;


        public AccountController(IUser user, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            User = user;
        }
        public  async Task<ActionResult> Add()
        {
            var userRoles = await _roleManager.Roles.ToListAsync();

            VmEditUser vm = new VmEditUser() {
                roles = userRoles.Select(x => new SelectListItem
                {
                    Value = x.Name,
                    Text = x.Name,
             
                }).ToList(),
        
            };
            return View(vm);
        }
        [HttpPost]
        public async Task<ActionResult> Add(VmEditUser model,string password)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            ApplicationUser user = new ApplicationUser()
            {

                UserName = model.username,
                Firstname = model.FirstName,
                Lastname = model.LastName,
                Email = model.Email,

            };
            try
            {
               
                var result = await _userManager.CreateAsync(user,password);
                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, model.selectedRole);
                    return Redirect("List");
                                  }
             
            }
            catch { }
            return View(new VmEditUser());
        }
    
    
        public async Task<ActionResult> Edit(string userid)
        {
            if (userid.IsNullOrEmpty())
            {
                return NotFound();
            }
          
            var role = await _roleManager.Roles.ToListAsync();
                var user =await _userManager.FindByIdAsync(userid);
            var userRoles =await _userManager.GetRolesAsync(user);
            var model = new VmEditUser()
            {
                username =user.UserName,
                userid = user.Id,
                Email = user.Email,
                FirstName = user.Firstname,
                LastName = user.Lastname,
                roles = role.Select(x => new SelectListItem
                {
                    Value = x.Name,
                    Text = x.Name,
                    Selected = userRoles.Contains(x.Name)
                }).ToList(),
                selectedRole = userRoles.FirstOrDefault()
            };
            
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Save(VmEditUser model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = await _userManager.FindByIdAsync(model.userid);
            if (user == null)
            {
                return NotFound();
            }

            user.UserName = model.username;
            user.Email = model.Email;
            user.Firstname = model.FirstName;
            user.Lastname = model.LastName;
            

            var currentRoles = await _userManager.GetRolesAsync(user);
            var selectedRole = model.selectedRole;

            foreach (var role in currentRoles)
            {
                await _userManager.RemoveFromRoleAsync(user, role);
            }

            if (!string.IsNullOrEmpty(selectedRole))
            {
                await _userManager.AddToRoleAsync(user, selectedRole);
            }

            var result = await _userManager.UpdateAsync(user);
            if (result.Succeeded)
            {
                return RedirectToAction("List");  
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }

            return View("Edit",model);
        }
        public async Task<IActionResult> List()
        {
            var users = _userManager.Users.ToList();
            var userRolesViewModel = new List<VmUserRoles>();

            foreach (var user in users)
            {
                var roles = await _userManager.GetRolesAsync(user);
                userRolesViewModel.Add(new VmUserRoles
                {
                    userid = user.Id,
                    username = user.UserName,
                    roles = roles,
                    FirstName= user.Firstname,
                    LastName= user.Lastname,
                    Email=user.Email
                    
                });
            }

            return View(userRolesViewModel);
        }
        public async Task<IActionResult> Delete(string userid)
        {
            var user = await _userManager.FindByIdAsync(userid);
            var DeleteUser =await _userManager.DeleteAsync(user);
            return Redirect("List");
        }
    }

}

