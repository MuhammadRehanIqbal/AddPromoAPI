using Application.Abstraction.Auth.Interfaces;
using Application.Abstraction.Auth.Services;
using Application.Abstraction.Persistence.Users; 
using Infrastructure.DbContext; 
using Infrastructure.Persistence.Auth.Repositories;
using Infrastructure.Persistence.Auth.Services; 
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options; 
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure
{
    public static class ConfigureServices
    {

        // Extension method to add infrastructure services
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            // Bind DapperSettings
            var dapperSettings = new DapperSettings();
            configuration.GetSection(DapperSettings.SectionName).Bind(dapperSettings);
            services.AddSingleton(Options.Create(dapperSettings));
            services.AddSingleton<DapperContext>();

            // Register repositories and services
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();
            services.AddScoped<IJwtTokenService, JwtTokenService>();

            return services;
        }
    }
}
