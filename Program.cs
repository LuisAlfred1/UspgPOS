using CloudinaryDotNet;
using dotenv.net;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using UspgPOS.Data;
using UspgPOS.Models;

namespace UspgPOS
{
    public class Program
    {
        public static void Main(string[] args)
        {
            DotEnv.Load(options: new DotEnvOptions(probeForEnv: true));
            var builder = WebApplication.CreateBuilder(args);

			// Configuración de la cadena de conexión desde secrets.json
			var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
			builder.Services.AddDbContext<AppDbContext>(options =>
				options.UseSqlServer(connectionString));

			// Agregar Identity
			builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
			{
				options.Password.RequireDigit = true;
				options.Password.RequiredLength = 6;
				options.Password.RequireNonAlphanumeric = false;
				options.Password.RequireUppercase = true;
				options.Password.RequireLowercase = true;
			})
			.AddEntityFrameworkStores<AppDbContext>()
			.AddDefaultTokenProviders();

			var cloudinaryAccount = new Account(
				Environment.GetEnvironmentVariable("CLOUDINARY_NAME"),
				Environment.GetEnvironmentVariable("CLOUDINARY_API_KEY"),
				Environment.GetEnvironmentVariable("CLOUDINARY_API_SECRET"));

			builder.Services.AddSingleton(new Cloudinary(cloudinaryAccount));

			// Add services to the container.
			builder.Services.AddControllersWithViews();

			builder.Services.AddSession();

			var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Auth}/{action=Login}/{id?}");


			app.UseSession();
			app.UseAuthentication();
			app.UseAuthorization();

			app.Run();
        }
    }
}
