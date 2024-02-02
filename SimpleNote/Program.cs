using Microsoft.EntityFrameworkCore;
using SimpleNote.Data;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("ContextConnection") ?? throw new InvalidOperationException("Connection string 'ContextConnection' not found.");
builder.Services.AddDbContext<Context>(options => options.UseSqlServer(connectionString));
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
    pattern: "{controller=Notes}/{action=Index}/{id?}");
app.Run();