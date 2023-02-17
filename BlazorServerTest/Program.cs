using BlazorServerTest.Core;
using BlazorServerTest.Core.Data;
using BlazorServerTest.Services;
using Hangfire;
using InMemoryCachingLibrary;
using MudBlazor.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddServices();

builder.Services.AddTransient<RecipeViewService>();
// InMemory service
builder.Services.AddInMemoryCachingSevice(builder.Configuration);

builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
/*
builder.Services.AddMudBlazorSnackbar();
builder.Services.AddMudBlazorDialog();
builder.Services.AddMudBlazorResizeListener();
builder.Services.AddMudBlazorScrollListener();
*/
builder.Services.AddMudServices();

builder.Services.AddEntityFrameworkSetup();

builder.Services.AddCors(opt =>
{
    opt.AddPolicy(name: "client", builder =>
    {
        builder.WithOrigins("http://localhost/3000")
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

builder.Services.AddHangfire(config =>
{
    config.UseSqlServerStorage(builder.Configuration.GetConnectionString("DBConnection"));
});
builder.Services.AddHangfireServer();

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

app.UseHangfireDashboard();

app.Run();
