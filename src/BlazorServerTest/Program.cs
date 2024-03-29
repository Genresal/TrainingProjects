using BlazorServerTest.Core;
using BlazorServerTest.Core.Business;
using BlazorServerTest.Core.Data.Contexts;
using BlazorServerTest.Core.Models.Recipes;
using FluentAssertions.Common;
using FluentValidation;
using FluentValidation.AspNetCore;
using InMemoryCachingLibrary;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Web;
using MudBlazor;
using MudBlazor.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCoreServices();

// add Fluent validation
builder.Services.AddValidatorsFromAssemblyContaining<Program>().AddFluentValidationAutoValidation();

// InMemory service
builder.Services.AddInMemoryCachingService(builder.Configuration);

builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddMudServices(config =>
{
    config.SnackbarConfiguration.PositionClass = Defaults.Classes.Position.BottomLeft;

    config.SnackbarConfiguration.PreventDuplicates = false;
    config.SnackbarConfiguration.NewestOnTop = false;
    config.SnackbarConfiguration.ShowCloseIcon = true;
    config.SnackbarConfiguration.VisibleStateDuration = 10000;
    config.SnackbarConfiguration.HideTransitionDuration = 500;
    config.SnackbarConfiguration.ShowTransitionDuration = 500;
    config.SnackbarConfiguration.SnackbarVariant = Variant.Filled;
});

//Azure AD
builder.Services.AddAuthentication(OpenIdConnectDefaults.AuthenticationScheme)
    .AddMicrosoftIdentityWebApp(builder.Configuration.GetSection("AzureAd"));

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

// Add initial data to the DB
await app.Services.InitializeDb();

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

// endpoints
app.MapGet("/api/health", () => "Hello World!");
// TODO This one not works
// TODO Docker not works
app.MapGet("/api/recipes", async ([FromQuery] RecipeRequest? request, RecipeManager manager) => await manager.GetRecipesAsync(request, CancellationToken.None));
app.MapPut("/api/recipes", async (UpdateRecipeRequest request, RecipeManager manager) => await manager.UpdateRecipeAsync(request, CancellationToken.None));

app.Run();
