using FuturesClean.API.Domain.Entities;

namespace FuturesClean.API.Domain.Repositories
{
    public interface IFuturesDifferenceRepository
    {
        public Task<FuturesDifference> AddAsync(FuturesDifference entity);
        public FuturesDifference Update(FuturesDifference entity);
        public Task<bool> Delete(Guid deleteId);
        public IQueryable<FuturesDifference> GetAll();
        public Task<bool> SaveAsync();
    }
}
