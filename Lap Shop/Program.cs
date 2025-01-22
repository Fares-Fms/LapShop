using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using Lap_shop.Models;
using Lap_Shop.BL;
using Microsoft.AspNetCore.Identity;
using Lap_Shop.Models;
using Microsoft.EntityFrameworkCore;
using Lap_Shop.Hubs;
namespace Lap_shop
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddControllersWithViews();
            builder.Services.AddSignalR();
            builder.Services.AddDbContext<LapShopContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
            builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
            {
                options.Password.RequiredLength = 10;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequireUppercase = true;
                options.Password.RequireLowercase = true;
                options.User.RequireUniqueEmail = true;
            }).AddEntityFrameworkStores<LapShopContext>().AddDefaultTokenProviders();

            builder.Services.AddScoped<ICategory, ClsCategory>();
            builder.Services.AddScoped<IsalesInvoice, ClsSalesInvoice>();
            builder.Services.AddScoped<ISalesInvoiceItems, ClsSalesInvoiceItems>();
            builder.Services.AddScoped<IItems, ClsItems>();
            builder.Services.AddScoped<IOS, ClsOs>();
            builder.Services.AddScoped<IItemType, ClsItemTypes>();
            builder.Services.AddScoped<IclsSettings, clsSettings>();
            builder.Services.AddScoped<IUser, ClsUser>();
            builder.Services.AddScoped<ISliders, ClsSliders>();
            builder.Services.AddScoped<IRole, CLsRole>();

            builder.Services.AddSession();
            builder.Services.AddHttpContextAccessor();
            builder.Services.AddDistributedMemoryCache(); 
            var app = builder.Build();
            using (var scope = app.Services.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<LapShopContext>();
                try
                {
                    dbContext.Database.CanConnect();
                    Console.WriteLine("Connection successful.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Connection failed: " + ex.Message);
                }
            }
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseSession();
        

            app.MapHub<ChatHub>("/ChatHub");
           


            app.UseEndpoints(endpoint => {

                endpoint.MapControllerRoute(
         name: "admin",
         pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");
            
                endpoint.MapControllerRoute(
     name: "Home",
     pattern: "{controller=Home}/{action=Index}/{id?}");

            }
            );

            app.Run();

        }
    }
}

