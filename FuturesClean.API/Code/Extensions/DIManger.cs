using FuturesClean.API.Code.Services;
using FuturesClean.API.Data.Repositories;
using FuturesClean.API.Domain.Repositories;

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
        }

        public static void AddHostedService(this WebApplicationBuilder webApplicationBuilder)
        {
            webApplicationBuilder.Services.AddHostedService<CheckDBHostedService>();
        }
    }
}
