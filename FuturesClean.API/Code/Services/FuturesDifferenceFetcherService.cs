using FuturesClean.API.Code.Builders;
using FuturesClean.API.Code.Comunication.Enum;
using FuturesClean.API.Code.Comunication.InnerResponse;
using FuturesClean.API.Code.Constants;
using FuturesClean.API.Domain.Services;
using Newtonsoft.Json.Linq;
using System.Globalization;

namespace FuturesClean.API.Code.Services
{
    public class FuturesDifferenceFetcherService : IFuturesDifferenceFetcherService
    {
        private readonly HttpClient _httpClient;

        public FuturesDifferenceFetcherService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<BaseResponse<decimal>> GetClosePriceAsync(string symbol, string intervalType, DateTime utcTime)
        {
            long endTime = new DateTimeOffset(utcTime).ToUnixTimeMilliseconds();
            var baseUrl = new BaseRequestBuilder(StandartConst.BaseURL)
                .BuildSymbol(symbol)
                .BuildInterval(intervalType)
                .BuildLimit(1).Build();
            var url = new BaseRequestBuilder(baseUrl)
                        .BuildEndTime(endTime)
                        .Build();
            var response = await _httpClient.GetStringAsync(url);
            var arr = JArray.Parse(response);

            if (arr.Count == 0)
            {
                for (int i = 1; i <= 7; i++)
                {
                    var fallbackTime = utcTime.AddDays(-i);
                    endTime = new DateTimeOffset(fallbackTime).ToUnixTimeMilliseconds();
                    url = new BaseRequestBuilder(baseUrl)
                        .BuildEndTime(endTime)
                        .Build();
                    response = await _httpClient.GetStringAsync(url);
                    arr = JArray.Parse(response);

                    if (arr.Count > 0)
                    {
                        break;
                    }
                }
            }

            if (arr.Count > 0)
            {
                decimal closePrice = decimal.Parse(arr[0][4].ToString(), CultureInfo.InvariantCulture);
                return new StandartResponse<decimal>()
                {
                    Data = closePrice,
                    InnerStatusCode = InnerStatusCode.OK,
                    Message = $"Get close price of {symbol} with interval {intervalType} and end time {endTime}"
                };
            }

            return new StandartResponse<decimal>()
            {
                InnerStatusCode = InnerStatusCode.EntityNotFound,
                Message = $"no close price of {symbol} with interval {intervalType} and end time {endTime}"
            };
        }

        public async Task<BaseResponse<string>> GetQuarterSymbolsAsync(string quarterType, string symbols)
        {
            string url = "https://dapi.binance.com/dapi/v1/exchangeInfo";
            var response = await _httpClient.GetStringAsync(url);
            var json = JObject.Parse(response);

            var symbolsList = json["symbols"]!
                .Where(s => s["symbol"]!.ToString().StartsWith(symbols))
                .ToList();

            if (symbolsList.Count == 0)
            {
                return new StandartResponse<string>()
                {
                    Data = symbols,
                    InnerStatusCode = InnerStatusCode.EntityNotFound,
                    Message = $"not found {symbols} {quarterType} symbols"
                };
            }
            string quarterSymbols = symbolsList.First(s => s["contractType"]!.ToString() == quarterType)["symbol"]!.ToString();

            return new StandartResponse<string>()
            {
                Data = quarterSymbols,
                InnerStatusCode = InnerStatusCode.OK,
                Message = $"found {symbols} {quarterType} symbols"
            };
        }
    }
}
