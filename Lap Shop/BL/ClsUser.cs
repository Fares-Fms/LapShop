using Lap_Shop.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace Lap_Shop.BL
{
        public interface IUser
        {
            public List<UserModel> GetAll();
            public  Task< bool> Save(UserModel item);
        public  Task<bool> Save(RegisterModel user);
            public bool Delete(int Id);
        public UserModel GetById(string? Id);
        public int UserCount();

        }
    public class ClsUser : IUser
    {
        LapShopContext CTX;
        ApplicationDbContext appContext;
        UserManager<ApplicationUser> _userManager;
        public ClsUser(LapShopContext ctx,UserManager<ApplicationUser> manager,ApplicationDbContext applicationDbContext)
        {
         _userManager = manager;
            CTX = ctx;
            appContext = applicationDbContext;
        }

        public bool Delete(int Id)
        {
            throw new NotImplementedException();
        }

        public List<UserModel> GetAll()
        {
         
            try
            {
                var LstItems = CTX.TbUserModel.ToList();
            
                return LstItems;
            }
            catch
            {
                return new List<UserModel>();
            }
        }

        public UserModel GetById(string? Id)
        {

            try
            {
                if (!Id.IsNullOrEmpty())
                {
                    
                var Items = CTX.TbUserModel.FirstOrDefault(a => a.Id == Id);
                return Items;
                }
                else
                {
                    return new UserModel();
                }
            }
            catch
            {
                return new UserModel();
            }
        }



        public  async Task<bool> Save(UserModel user)
        {
           
            try
            {



                    ApplicationUser applicationUser = new ApplicationUser()
                    {

                        UserName = user.UserName,
                        Firstname = user.FirstName,
                        Lastname = user.lastName,
                        Email = user.Email,

                    };
             
                    var updateResult = await _userManager.UpdateAsync(applicationUser);
                    if (updateResult.Succeeded)
                    {

                        CTX.Entry(user).State = EntityState.Modified;

                CTX.SaveChanges();
                    }
                else
                {
                    return false;
                }


                return true;
            }
            catch
            {
                return false;
            }

        }
        public async Task<bool> Save(RegisterModel user)
        {
            try
            {
                ApplicationUser applicationUser = new ApplicationUser()
                {

                    UserName = user.UserName,
                    Firstname = user.FirstName,
                    Lastname = user.lastName,
                    Email = user.Email,

                };
                UserModel userModel = new UserModel()
                {
                    Id = applicationUser.Id,
                    UserName = user.UserName,
                    Email = user.Email,
                    FirstName = user.FirstName,
                    lastName = user.lastName,
                    role = "Customer"
                };


                var result = await _userManager.CreateAsync(applicationUser, user.Password);
                if (result.Succeeded)
                {
                    CTX.TbUserModel.Add(userModel);
                    CTX.SaveChanges();

                    return true;
                }
                else
                {
                    return false;
                }


            
                
                
            }
            catch
            {
                return false;
            }

        }

        public int UserCount()
        {
            var Users=appContext.AspNetUsers.Count();
            return Users;
        }
        /*    public bool Delete(int Id)
   {
       try
       {

           LapShopContext CTX = new LapShopContext();

           var cate = GetById(Id);
           cate.CurrentState = 0;
           CTX.Entry(cate).State = EntityState.Modified;
           CTX.SaveChanges();
           return true;

       }
       catch { return false; }
   }*/



    }
}
