using Microsoft.AspNetCore.Builder;

namespace Test_System
{
    public class Program
    {
        public static void Main(string[] args)
        {

            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllersWithViews();

            var app = builder.Build();

            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

           

            app.MapControllerRoute(
                name: "default",
               pattern: "{area=Customer}/{controller=Home}/{action=Index}/{id?}");
            

            app.Run();






        }
    }
}




























//var builder = WebApplication.CreateBuilder(args);


//builder.Services.AddControllersWithViews();

//var app = builder.Build();


//if (!app.Environment.IsDevelopment())
//{
//    app.UseExceptionHandler("/Home/Error");
//    app.UseHsts();
//}

//app.UseHttpsRedirection();


//app.UseStaticFiles();

//app.UseRouting();

//app.UseAuthorization();



//app.MapControllerRoute(
//    name: "default",
//    pattern: "{area=Customer}/{controller=Home}/{action=Index}/{id?}"
//);

//app.Run();