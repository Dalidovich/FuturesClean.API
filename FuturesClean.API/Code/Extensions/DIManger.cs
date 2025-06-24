using FuturesClean.API.Code.Services;
using FuturesClean.API.Data.Repositories;
using FuturesClean.API.Domain.Repositories;
using FuturesClean.API.Domain.Services;

namespace FuturesClean.API.Code.Extensions
{
    public static class DIManger
    {
        public static void AddRepositores(this WebApplicationBuilder webApplicationBuilder)
        {
            webApplicationBuilder.Services.AddScoped<IFuturesDifferenceRepository, FuturesDifferenceRepository>();
        }

        public static void AddServices(this WebApplicationBuilder webApplicationBuilder)
        {
            webApplicationBuilder.Services.AddScoped<IFuturesDifferenceFetcherService, FuturesDifferenceFetcherService>();
            webApplicationBuilder.Services.AddScoped<IFuturesDifferenceService, FuturesDifferenceService>();
        }

        public static void AddHostedService(this WebApplicationBuilder webApplicationBuilder)
        {
            webApplicationBuilder.Services.AddHostedService<CheckDBHostedService>();
        }
    }
}
