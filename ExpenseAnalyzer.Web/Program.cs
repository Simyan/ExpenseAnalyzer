using ExpenseAnalyzer.BLL.Interfaces;
using ExpenseAnalyzer.BLL.ServiceLayer;
using ExpenseAnalyzer.DAL.Entities;
using ExpenseAnalyzer.DAL.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllersWithViews();
builder.Services.AddTransient<IVendorRepository, VendorRepository>();
builder.Services.AddTransient<ITransactionRepository, TransactionRepository>();
builder.Services.AddTransient<ICategoryMasterRepository, CategoryMasterRepository>();
builder.Services.AddTransient<ITransactionService, TransactionService>();
builder.Services.AddDbContext<ExpenseAnalyzerContext>(
                   options => 
                            {
                                options.UseSqlServer("Server=MSI;Database=ExpenseAnalyzer;Trusted_Connection=True;MultipleActiveResultSets=true")
                                .LogTo(Console.WriteLine, LogLevel.Information);
                             });

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


app.MapControllerRoute(
    name: "default",
    pattern: "{controller}/{action=Index}/{id?}");

app.MapFallbackToFile("index.html"); ;

app.Run();
