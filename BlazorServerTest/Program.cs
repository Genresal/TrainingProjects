using BlazorServerTest.Services;
using BlazorServerTest.Services.Interfaces;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

using Hangfire;
using Microsoft.Extensions.Configuration;
using BackgroundService = BlazorServerTest.Services.BackgroundService;
using BlazorServerTest.Data.Repositories.Interfaces;
using BlazorServerTest.Data.Repositories;
using BlazorServerTest.Data.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddTransient<WeatherForecastService>();
builder.Services.AddTransient<IWeatherForecastRepository, WeatherForecastRepository>();
builder.Services.AddTransient<IBackgroundService, BackgroundService>();

builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();



builder.Services.AddEntityFrameworkSqlite().AddDbContext<AppDbContext>();
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

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.UseHangfireDashboard();

//var serv = app.Services.GetRequiredService<IBackgroundService>();
//RecurringJob.AddOrUpdate(() => serv.GetAndSaveBackgroundAsync(), Cron.Minutely);
//RecurringJob.AddOrUpdate<IBackgroundService>(x => x.GetAndSaveBackgroundAsync(), Cron.Minutely);

app.Run();
