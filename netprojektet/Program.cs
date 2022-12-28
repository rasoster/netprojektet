using Microsoft.EntityFrameworkCore;
using Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Proxies;
using DataAccessLayer;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
IConfiguration configuration = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json", true, true)
    .Build();
builder.Services.AddDbContext<LinkedoutDbContext>(options => options.UseLazyLoadingProxies().UseSqlServer(configuration.GetConnectionString("LinkedoutDBContext"),b => b.MigrationsAssembly("netprojektet")));
builder.Services.AddIdentity<Anvandare,IdentityRole>()
    .AddEntityFrameworkStores<LinkedoutDbContext>();
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
app.UseAuthentication();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
