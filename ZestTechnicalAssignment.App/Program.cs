using Blazored.FluentValidation;
using Blazored.LocalStorage;
using FluentValidation;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;
using ZestTechnicalAssignment.App;
using ZestTechnicalAssignment.App.Authentication;
using ZestTechnicalAssignment.App.InterFacess;
using ZestTechnicalAssignment.App.Servicess;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");
builder.Services.AddScoped<IAuthServices,AuthServices>();
builder.Services.AddScoped<IApiServices, ApiServices>();
builder.Services.AddAuthorizationCore();
builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthenticationState>();
builder.Services.AddMudServices();
builder.Services.AddBlazoredLocalStorage();

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:7073/api/") });

await builder.Build().RunAsync();
