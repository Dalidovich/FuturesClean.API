using FuturesClean.API.Code.Comunication.Enum;
using FuturesClean.API.Code.Comunication.InnerResponse;
using FuturesClean.API.Code.Constants;
using FuturesClean.API.Domain.Entities;
using FuturesClean.API.Domain.Repositories;
using FuturesClean.API.Domain.Services;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace FuturesClean.API.Code.Services
{
    public class FuturesDifferenceService : IFuturesDifferenceService
    {
        private readonly IFuturesDifferenceRepository _futuresDifferenceRepository;
        private readonly IFuturesDifferenceFetcherService _futuresDifferenceFetcherService;

        public FuturesDifferenceService(IFuturesDifferenceRepository futuresDifferenceRepository, IFuturesDifferenceFetcherService futuresDifferenceFetcherService)
        {
            _futuresDifferenceRepository = futuresDifferenceRepository;
            _futuresDifferenceFetcherService = futuresDifferenceFetcherService;
        }

        public async Task<BaseResponse<FuturesDifference>> CalculateFuturesDifferenceAsync(string intervalType, DateTime utcTime, string symbols)
        {
            //можно накинуть на Interval валидаторы
            var currentQuarter = await _futuresDifferenceFetcherService.GetQuarterSymbolsAsync(QuarterType.CurrentQuarter, symbols);
            var nextQuarter = await _futuresDifferenceFetcherService.GetQuarterSymbolsAsync(QuarterType.NextQuarter, symbols);

            if (currentQuarter.InnerStatusCode != InnerStatusCode.OK || nextQuarter.InnerStatusCode != InnerStatusCode.OK)
            {
                return new StandartResponse<FuturesDifference>()
                {
                    Data = null,
                    InnerStatusCode = InnerStatusCode.EntityNotFound,
                    Message = "currentQuarter or nextQuarter not found"
                };
            }

            var currentQuarterPriceResponse = await _futuresDifferenceFetcherService.GetClosePriceAsync(currentQuarter.Data, intervalType, utcTime);
            var nextQuarterPriceResponse = await _futuresDifferenceFetcherService.GetClosePriceAsync(nextQuarter.Data, intervalType, utcTime);

            if (currentQuarterPriceResponse.InnerStatusCode != InnerStatusCode.OK || nextQuarterPriceResponse.InnerStatusCode != InnerStatusCode.OK)
            {
                return new StandartResponse<FuturesDifference>()
                {
                    Data = null,
                    InnerStatusCode = InnerStatusCode.EntityNotFound,
                    Message = "currentQuarterPrice or nextQuarterPrice not found"
                };
            }

            var entity = new FuturesDifference(utcTime, intervalType, currentQuarter.Data, nextQuarter.Data,
                currentQuarterPriceResponse.Data - nextQuarterPriceResponse.Data);

            var addedEntity = await _futuresDifferenceRepository.AddAsync(entity);
            await _futuresDifferenceRepository.SaveAsync();

            return new StandartResponse<FuturesDifference>()
            {
                Data = addedEntity,
                InnerStatusCode = InnerStatusCode.FuturesDifferenceCreate,
                Message = "FuturesDifference successfully calculated and added"
            };
        }

        public async Task<BaseResponse<bool>> DeleteFuturesDifferenceAsync(Guid deleteId)
        {
            var entity = await _futuresDifferenceRepository
                .GetAll()
                .Where(x => x.Id == deleteId)
                .SingleOrDefaultAsync();

            if (entity == null)
            {
                return new StandartResponse<bool>()
                {
                    Data = false,
                    InnerStatusCode = InnerStatusCode.EntityNotFound,
                    Message = $"FuturesDifference with id \'{deleteId}\' not found"
                };
            }

            var response = await _futuresDifferenceRepository.Delete(deleteId);

            return new StandartResponse<bool>()
            {
                Data = response,
                InnerStatusCode = InnerStatusCode.FuturesDifferenceDelete,
                Message = "FuturesDifference successfully deleted"
            };
        }

        public async Task<BaseResponse<FuturesDifference>> GetFuturesDifferenceAsync(Expression<Func<FuturesDifference, bool>> expression)
        {
            var entity = await _futuresDifferenceRepository.GetAll().SingleOrDefaultAsync(expression);
            if (entity == null)
            {
                return new StandartResponse<FuturesDifference>()
                {
                    InnerStatusCode = InnerStatusCode.EntityNotFound,
                    Message = $"FuturesDifference not found"
                };
            }

            return new StandartResponse<FuturesDifference>()
            {
                Data = entity,
                InnerStatusCode = InnerStatusCode.FuturesDifferenceRead,
                Message = $"FuturesDifference found"
            };
        }

        public async Task<BaseResponse<IEnumerable<FuturesDifference>>> GetFuturesDifferencesAsync(Expression<Func<FuturesDifference, bool>> expression)
        {
            var entities = await _futuresDifferenceRepository.GetAll().Where(expression).ToListAsync();

            return new StandartResponse<IEnumerable<FuturesDifference>>()
            {
                Data = entities,
                InnerStatusCode = InnerStatusCode.FuturesDifferenceRead,
                Message = $"FuturesDifference list found"
            };
        }
    }
}
