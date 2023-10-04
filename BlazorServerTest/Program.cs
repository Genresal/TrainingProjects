using BlazorServerTest.Core;
using InMemoryCachingLibrary;
using MudBlazor.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCoreServices();

// InMemory service
builder.Services.AddInMemoryCachingService(builder.Configuration);

builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddMudServices();

builder.Services.AddCors(opt =>
{
    opt.AddPolicy(name: "client", builder =>
    {
        builder.WithOrigins("http://localhost/3000")
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

// Not use hangfire for that azure deployment
/*
builder.Services.AddHangfire(config =>
{
    config.UseSqlServerStorage(builder.Configuration.GetConnectionString("DBConnection"));
});
builder.Services.AddHangfireServer();
*/
var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseCors("client");

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

//app.UseHangfireDashboard();

app.Run();
