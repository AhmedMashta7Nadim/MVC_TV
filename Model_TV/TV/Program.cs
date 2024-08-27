using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Options;
using Model_TV.Models;
using Model_TV.VM;
using Serilog;
using System.Globalization;
using TV.Data;
using TV.Models;
using TV.Repositry.RepoModels;
using TV.Repositry.Serves;

namespace TV
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.Console()
                .WriteTo.File("loggers/jog.txt", rollingInterval: RollingInterval.Day)
                .CreateLogger();



            builder.Host.UseSerilog();


            builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");


            builder.Services.AddControllersWithViews(options => { })
                .AddViewLocalization(Microsoft.AspNetCore.Mvc.Razor.LanguageViewLocationExpanderFormat.Suffix)
                .AddDataAnnotationsLocalization();



            builder.Services.Configure<RequestLocalizationOptions>(options =>
            {
                var supportedCultures = new List<CultureInfo>
                {
                    new CultureInfo("ar-SY"),
                    new CultureInfo("en-US"),
                };

                options.DefaultRequestCulture = new RequestCulture("en-US");
                options.SupportedCultures = supportedCultures;
                options.SupportedUICultures = supportedCultures;
                options.RequestCultureProviders = new List<IRequestCultureProvider>
                {
                    new QueryStringRequestCultureProvider(),
                    new CookieRequestCultureProvider(),
                    //new AcceptLanguageHeaderRequestCultureProvider(),
                };
            });

            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
                ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(connectionString));

            builder.Services.AddDatabaseDeveloperPageExceptionFilter();

            builder.Services.AddIdentity<UserName, IdentityRole>(options =>
            {
                options.SignIn.RequireConfirmedAccount = true;
                options.Password.RequireDigit = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
                options.Password.RequiredLength = 1;
            }


            )
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultUI()
                .AddDefaultTokenProviders();

            builder.Services.AddScoped<IRepositryAllModel<TV_Show, TV_ShowSummary>, RepositryAllModel<TV_Show, TV_ShowSummary>>();
            builder.Services.AddScoped<IRepositryAllModel<Attachment, AttachmentSummary>, RepositryAllModel<Attachment, AttachmentSummary>>();
            builder.Services.AddScoped<IRepositryAllModel<Languages, LanguagesSummary>, RepositryAllModel<Languages, LanguagesSummary>>();
            builder.Services.AddScoped<IRepositryAllModel<TV_ShowLanguages, TV_ShowLanguagesSummary>, RepositryAllModel< TV_ShowLanguages, TV_ShowLanguagesSummary>> ();
       
            
            
            builder.Services.AddScoped<RepoAttachment>();

            builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            builder.Services.AddSingleton<IStringLocalizerFactory, ResourceManagerStringLocalizerFactory>();
            builder.Services.AddTransient(typeof(IStringLocalizer<>), typeof(StringLocalizer<>));

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            var options = app.Services.GetService<IOptions<RequestLocalizationOptions>>();
            app.UseRequestLocalization(
                options.Value);

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Show}/{id?}");

            app.MapRazorPages();

            app.Run();
        }
    }
}
