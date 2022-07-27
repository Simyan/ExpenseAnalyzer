using ExpenseAnalyzer.BLL.Interfaces;
using ExpenseAnalyzer.BLL.ServiceLayer;
using ExpenseAnalyzer.DAL.Entities;
using ExpenseAnalyzer.DAL.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication;
using ExpenseAnalyzer.BLL.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllersWithViews();
builder.Services.AddTransient<IVendorRepository, VendorRepository>();
builder.Services.AddTransient<ITransactionRepository, TransactionRepository>();
builder.Services.AddTransient<ICategoryMasterRepository, CategoryMasterRepository>();
builder.Services.AddTransient<IReportMetadataMasterRepository, ReportMetadataMasterRepository>();
builder.Services.AddTransient<ITransactionService, TransactionService>();
builder.Services.AddDbContext<ExpenseAnalyzerContext>(
                   options => 
                            {
                                options.UseSqlServer("Server=MSI;Database=ExpenseAnalyzer;Trusted_Connection=True;MultipleActiveResultSets=true")
                                .LogTo(Console.WriteLine, LogLevel.Information);
                             });
builder.Services.AddDatabaseDeveloperPageExceptionFilter();
builder.Services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ExpenseAnalyzerContext>();

builder.Services.AddIdentityServer()
    .AddApiAuthorization<ApplicationUser, ExpenseAnalyzerContext>();

builder.Services.AddAuthentication()
    .AddIdentityServerJwt();

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.UseAuthentication();
app.UseIdentityServer();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller}/{action=Index}/{id?}");
app.MapRazorPages();

app.MapFallbackToFile("index.html"); ;

app.Run();


