using Microsoft.Identity.Client;
using System.Net.Http.Headers;

// 1. Get token from Azure AD
var app = PublicClientApplicationBuilder
    .Create("your-client-id")                    // Client ID for your client app (not the API)
    .WithAuthority(AzureCloudInstance.AzurePublic, "your-tenant-id")
    .WithRedirectUri("http://localhost")
    .Build();

var scopes = new[] { "api://0c423b46-5bab-477b-933a-fb550695433f/access_as_user" };
var result = await app.AcquireTokenInteractive(scopes).ExecuteAsync();

// 2. Use token to call API
using var client = new HttpClient();
client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", result.AccessToken);

// 3. Make API request
var response = await client.GetAsync("https://localhost:7xxx/api/your-endpoint");
var content = await response.Content.ReadAsStringAsync();
