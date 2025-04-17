using DevIONETEC.App.Configurations;
using DevIONETEC.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using DevIONETEC.App.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration
  .SetBasePath(builder.Environment.ContentRootPath)
  .AddJsonFile("appsettings.json", true, true)
  .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", true, true)
  .AddEnvironmentVariables();

// ConfigureServices
builder.Services.AddIdentityConfiguration(builder.Configuration);

builder.Services.AddDbContext<IonetecDbContext>(options =>
  options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

//builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true).AddEntityFrameworkStores<ApplicationDbContext>();

 
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddMvcConfiguration();
builder.Services.ResolveDependencies();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    //app.UseMigrationsEndPoint();
}
else
{
    //app.UseExceptionHandler("/Home/Error");
    app.UseExceptionHandler("/erro/500");
    app.UseStatusCodePagesWithRedirects("/erro/{0}");
    app.UseHsts();
}

app.UseHttpsRedirection();  
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();
app.UseGlobalizationConfig();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();
