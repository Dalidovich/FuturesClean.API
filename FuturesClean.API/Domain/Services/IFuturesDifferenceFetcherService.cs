using FuturesClean.API.Code.Comunication.InnerResponse;

namespace FuturesClean.API.Domain.Services
{
    public interface IFuturesDifferenceFetcherService
    {
        public Task<BaseResponse<string>> GetQuarterSymbolsAsync(string quarterType);
        public Task<BaseResponse<decimal>> GetClosePriceAsync(string symbol, string intervalType, DateTime utcTime);
    }
}
