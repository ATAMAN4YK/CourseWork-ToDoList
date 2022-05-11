using ToDoListData.Providers;
using MSSQLDataBase;
using XMLDataBase;
using ToDoList.DataBaseProvider;
using ToDoList.enums;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddTransient<IDataProvider>(provider =>
{
    switch (DataBaseStatus.ChoosedDataBase)
    {
        case (int)DataBaseList.MSSQLDataBase:
            return new MSDataProvider();
        
        case (int)DataBaseList.XMLDataBase:
            return new XMLDataProvider();
        
        default:
            return new MSDataProvider();
    }
});


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
    pattern: "{controller=Home}/{action=TasksList}/{id?}");

app.Run();
