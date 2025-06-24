using FuturesClean.API.Code.Comunication.InnerResponse;
using FuturesClean.API.Domain.Entities;
using System.Linq.Expressions;

namespace FuturesClean.API.Domain.Services
{
    public interface IFuturesDifferenceService
    {
        public Task<BaseResponse<FuturesDifference>> GetFuturesDifferenceAsync(Expression<Func<FuturesDifference, bool>> expression);
        public Task<BaseResponse<IEnumerable<FuturesDifference>>> GetFuturesDifferencesAsync(Expression<Func<FuturesDifference, bool>> expression);
        public Task<BaseResponse<FuturesDifference>> CalculateFuturesDifferenceAsync(string intervalType, DateTime utcTime, string symbols);
        public Task<BaseResponse<bool>> DeleteFuturesDifferenceAsync(Guid deleteId);
    }
}
