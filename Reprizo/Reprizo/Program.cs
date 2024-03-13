using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using Reprizo.Data;
using Reprizo.Helpers;
using Reprizo.Models;
using Reprizo.Services;
using Reprizo.Services.Interfaces;
using System.Globalization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAutoMapper(typeof(Program));

// map emailconfig to emailsettings
builder.Services.Configure<EmailSettings>(builder.Configuration.GetSection("EmailConfig"));

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<AppDbContext>(options =>
	options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
		   //.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking)
);


builder.Services.AddIdentity<AppUser, IdentityRole>()
                .AddEntityFrameworkStores<AppDbContext>()
                .AddDefaultTokenProviders();

builder.Services.Configure<IdentityOptions>(option =>
{
	option.Password.RequireNonAlphanumeric = true; //simvol olab biler
	option.Password.RequireDigit = true; //reqem olmalidir
	option.Password.RequireLowercase = true; //balaca herf olmalidir
	option.Password.RequireUppercase = true; //boyuk olmalidir
	option.Password.RequiredLength = 6; //minimum 6 

	option.User.RequireUniqueEmail = true;

	option.SignIn.RequireConfirmedEmail = true;
	//Default lockout  settings

	option.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
	option.Lockout.MaxFailedAccessAttempts = 5;
	option.Lockout.AllowedForNewUsers = true;

});

builder.Services.AddScoped<ISliderService, SliderService>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<ICollectionService, CollectionService>();
builder.Services.AddScoped<IEssenceService, EssenceService>();
builder.Services.AddScoped<IFeatureService, FeatureService>();
builder.Services.AddScoped<ILayoutService, LayoutService>();
builder.Services.AddScoped<ISettingService, SettingService>();
builder.Services.AddScoped<IBlogService, BlogService>();
builder.Services.AddScoped<IRepairService, RepairService>();
builder.Services.AddScoped<ITeamService, TeamService>();
builder.Services.AddScoped<IBestWorkerService, BestWorkerService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<ISubscribeService, SubscribeService>();
builder.Services.AddScoped<IContactService, ContactService>();
builder.Services.AddScoped<IBasketService, BasketService>();
builder.Services.AddScoped<IWishlistService, WishlistService>();
builder.Services.AddScoped<IEmailService, EmailService>();
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

var supportedCultures = new[] { new CultureInfo("en-US") }; // Adjust as needed
app.UseRequestLocalization(new RequestLocalizationOptions
{
	DefaultRequestCulture = new RequestCulture("en-US"),
	SupportedCultures = supportedCultures,
	SupportedUICultures = supportedCultures
});

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();


app.UseAuthentication();
app.UseAuthorization();



app.MapControllerRoute(
            name: "areas",
            pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
          );

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");


app.Run();
