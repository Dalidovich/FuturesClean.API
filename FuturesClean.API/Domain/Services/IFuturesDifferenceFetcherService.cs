using FuturesClean.API.Code.Comunication.InnerResponse;

namespace FuturesClean.API.Domain.Services
{
    public interface IFuturesDifferenceFetcherService
    {
        public Task<BaseResponse<string>> GetQuarterSymbolsAsync(string quarterType, string symbols);
        public Task<BaseResponse<decimal>> GetClosePriceAsync(string symbol, string intervalType, DateTime utcTime);
    }
}
