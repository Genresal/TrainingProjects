using BlasorServerTest.Repositories.Interfaces;
using BlazorServerTest.Infrastructure;
using BlazorServerTest.Repositories;
using BlazorServerTest.Services;
using BlazorServerTest.Services.Interfaces;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

using Hangfire;
using Microsoft.Extensions.Configuration;
using BackgroundService = BlazorServerTest.Services.BackgroundService;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

builder.Services.AddTransient<WeatherForecastService>();
builder.Services.AddTransient<IWeatherForecastRepository, WeatherForecastRepository>();

builder.Services.AddEntityFrameworkSqlite().AddDbContext<AppDbContext>();
builder.Services.AddHangfire(config =>
{
    config.UseSqlServerStorage(builder.Configuration.GetConnectionString("DBConnection"));
});
builder.Services.AddHangfireServer();

builder.Services.AddTransient<IBackgroundService, BackgroundService>();

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
RecurringJob.AddOrUpdate<IBackgroundService>(x => x.GetAndSaveBackgroundAsync(), Cron.Minutely);

app.Run();
