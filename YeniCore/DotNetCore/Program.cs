using DataAccessLayer.Concrete;
using EntityLayer.Concrete;
using FluentValidation;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace CoreDemo
{
    public class Program
    {
        public static void Main(string[] args)
        {

            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddDbContext<Context>();
            builder.Services.AddIdentity<AppUser, AppRole>(x =>
            {
                x.Password.RequireUppercase = false;
                x.Password.RequireNonAlphanumeric = false;
            })

                .AddEntityFrameworkStores<Context>();
            builder.Services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = "/Login/Index";
                options.AccessDeniedPath = "/ErrorPage/Error1";
                options.AccessDeniedPath = new PathString("/Login/AccessDenied/");
            });
            
            builder.Services.AddMvc(config =>
            {
                var policy = new AuthorizationPolicyBuilder()
                                .RequireAuthenticatedUser()
                                .Build();
                config.Filters.Add(new AuthorizeFilter(policy));
            });



            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {

                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();

            }

            app.UseStatusCodePagesWithReExecute("/ErrorPage/Error1", "?code={0}");
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            //app.MapControllerRoute(
            //name: "areas",
            //pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");
            app.MapControllerRoute(
            name: "areas",
            pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
