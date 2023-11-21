using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System.Configuration;
using WebSalesMvc.Data;
var builder = WebApplication.CreateBuilder(args);

/*builder.Services.AddDbContext<WebSalesMvcContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("WebSalesMvcContext") ?? throw new InvalidOperationException("Connection string 'WebSalesMvcContext' not found.")));
*/

builder.Services.AddDbContext<WebSalesMvcContext>(options =>
        options.UseNpgsql(builder.Configuration.GetConnectionString("WebSalesMvcContext"), x => x.MigrationsAssembly("WebSalesMvc")));

/*options.UseNpgsql(Configuration.GetConnectionString("WebSalesMvcContext"), builder =>
    builder.MigrationsAssembly("WebSalesMvc" ?? throw new InvalidOperationException("Connection string 'WebSalesMvcContext' not found.")));*/

// Add services to the container.
builder.Services.AddControllersWithViews();

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
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
