using Microsoft.EntityFrameworkCore;
using SecondTask.Model;
using SecondTask.BusinessLogic.Interfaces;
using SecondTask.BusinessLogic.Implementations;
using SecondTask.Parser;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

//adds the services and dbcontext
builder.Services.AddDbContext<ApplicationContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddTransient<Parser>();
builder.Services.AddTransient<IAccountingService, AccountingService>();
builder.Services.AddTransient<IFileService, FileService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
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
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
