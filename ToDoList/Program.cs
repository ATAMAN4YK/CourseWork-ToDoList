using ToDoListData.Providers;
using MSSQLDataBase;
using XMLDataBase;
using ToDoList.DataBaseProvider;
using ToDoList.enums;
using ToDoList;

using ToDoList.GraphQL;
using GraphQL.SystemTextJson;
using GraphQL;
using GraphiQl;
using GraphQL.Server;
using GraphQL.Server.Ui.Altair;
using ToDoList.Extensions;
using Microsoft.AspNetCore.Server.Kestrel.Core;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddGraphQLApi();
builder.Services.AddAutoMapper(typeof(MapperProfile));

builder.Services.Configure<IISServerOptions>(options => options.AllowSynchronousIO = true);
builder.Services.Configure<KestrelServerOptions>(options => options.AllowSynchronousIO = true);

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

builder.Services.AddCors(opt =>
{
    opt.AddDefaultPolicy(builder => 
        builder.AllowAnyOrigin()
            .AllowAnyHeader()
            .AllowAnyMethod());
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

app.UseCors(options =>
{
    options.AllowAnyHeader();
    options.AllowAnyMethod();
    options.AllowAnyOrigin();
});


app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=TasksList}/{id?}");

app.UseGraphQLPlayground();
app.UseGraphQL<ToDoListSchema>();
app.UseGraphQLAltair();

app.Run();
