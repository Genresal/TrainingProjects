using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Identity.Web;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddMicrosoftIdentityWebApi(options =>
    {
        builder.Configuration.Bind("AzureAd", options);
        options.TokenValidationParameters.NameClaimType = "preferred_username";
    },
    options => { builder.Configuration.Bind("AzureAd", options); });

builder.Services.AddAuthorization(options =>
{
    // Create a default policy requiring authentication
    options.FallbackPolicy = new AuthorizationPolicyBuilder()
        .RequireAuthenticatedUser()
        .Build();
});

builder.Services.AddControllers();

// Add Swagger/OpenAPI support
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo { Title = "Protected API", Version = "v1" });

    options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
    {
        Type = SecuritySchemeType.OAuth2,
        Flows = new OpenApiOAuthFlows
        {
            AuthorizationCode = new OpenApiOAuthFlow
            {
                AuthorizationUrl = new Uri($"{builder.Configuration["AzureAd:Instance"]}{builder.Configuration["AzureAd:TenantId"]}/oauth2/v2.0/authorize"),
                TokenUrl = new Uri($"{builder.Configuration["AzureAd:Instance"]}{builder.Configuration["AzureAd:TenantId"]}/oauth2/v2.0/token"),
                Scopes = new Dictionary<string, string>
                {
                    { $"api://{builder.Configuration["AzureAd:ClientId"]}/access_as_user", "Access as user" }
                }
            }
        }
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "oauth2" }
            },
            new[] { $"api://{builder.Configuration["AzureAd:ClientId"]}/access_as_user" }
        }
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "Protected API v1");
        options.OAuthClientId(builder.Configuration["AzureAd:ClientId"]);
        options.OAuthScopeSeparator(" ");
        options.OAuthUsePkce();
    });
}

app.UseHttpsRedirection();

// Important: Authentication middleware must come before Authorization
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
