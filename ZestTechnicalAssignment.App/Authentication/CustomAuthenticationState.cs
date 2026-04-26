using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using ZestTechnicalAssignment.App.ResponseModel;

namespace ZestTechnicalAssignment.App.Authentication
{
    public class CustomAuthenticationState(ILocalStorageService localStorageService) : AuthenticationStateProvider
    {
        private readonly ClaimsPrincipal anonymous = new(new ClaimsIdentity());
        public async override Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            try
            {
                string? stringToken = await localStorageService.GetItemAsStringAsync("token");

                if (string.IsNullOrWhiteSpace(stringToken))
                    return await Task.FromResult(new AuthenticationState(anonymous));

                var claims = GetClaimsFromToken(stringToken);

                var claimsPrinciple = SetClaimPrinciple(claims);

                return await Task.FromResult(new AuthenticationState(claimsPrinciple));
            }
            catch
            {
                return await Task.FromResult(new AuthenticationState(anonymous));
            }
        }

        public async Task UpdateAuthenticationState(string? token)
        {
            ClaimsPrincipal claimsPrincipal = new();
            if (!string.IsNullOrWhiteSpace(token))
            {
                var userSession = GetClaimsFromToken(token);
                claimsPrincipal = SetClaimPrinciple(userSession);
                await localStorageService.SetItemAsStringAsync("token", token);
            }
            else
            {
                claimsPrincipal = anonymous;
                await localStorageService.RemoveItemAsync("token");
            }
            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(claimsPrincipal)));
        }
        public async Task<string?> GetCurrentUserIdAsync()
        {
            var authState = await GetAuthenticationStateAsync();
            var user = authState.User;

            if (user.Identity!.IsAuthenticated)
            {
                return user.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            }
            return null;
        }

        public async Task<string?> GetCurrentUserRoleAsync()
        {
            var authState = await GetAuthenticationStateAsync();
            var user = authState.User;

            if (user.Identity!.IsAuthenticated)
            {
                return user.FindFirst(ClaimTypes.Role)!.Value;
            }

            return null;
        }
        public static ClaimsPrincipal SetClaimPrinciple(UserSession model)
        {
            return new ClaimsPrincipal(new ClaimsIdentity(
                new List<Claim>
                {
                    new(ClaimTypes.NameIdentifier, model.Id!),
                    new(ClaimTypes.Name, model.Name!),
                    new(ClaimTypes.Email, model.Email!),
                    new(ClaimTypes.Role, model.Role!),
                }, "JwtAuth"));
        }

        public static UserSession GetClaimsFromToken(string jwtToken)
        {
            var handler = new JwtSecurityTokenHandler();
            var token = handler.ReadJwtToken(jwtToken);
            var claims = token.Claims;

            string Id = claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;
            string Name = claims.First(c => c.Type == ClaimTypes.Name).Value;
            string Email = claims.First(c => c.Type == ClaimTypes.Email).Value;
            string Role = claims.First(c => c.Type == ClaimTypes.Role).Value;

            return new UserSession(Id, Name, Email, Role);

        }
    }
}
