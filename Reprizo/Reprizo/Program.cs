using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Reprizo.Data;
using Reprizo.Models;
using Reprizo.Services.Interfaces;
using Reprizo.Services;
using Reprizo.Areas.Admin.ViewModels.BestWorker;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAutoMapper(typeof(Program));

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<AppDbContext>(options =>
        options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddIdentity<AppUser, IdentityRole>()
                .AddEntityFrameworkStores<AppDbContext>()
                .AddDefaultTokenProviders();

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
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();


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
