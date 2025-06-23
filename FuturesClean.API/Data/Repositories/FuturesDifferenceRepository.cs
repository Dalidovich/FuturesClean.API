using FuturesClean.API.Domain.Entities;
using FuturesClean.API.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace FuturesClean.API.Data.Repositories
{
    public class FuturesDifferenceRepository : IFuturesDifferenceRepository
    {
        private readonly AppDBContext _db;

        public FuturesDifferenceRepository(AppDBContext db)
        {
            _db = db;
        }

        public async Task<FuturesDifference> AddAsync(FuturesDifference entity)
        {
            var createdEntity = await _db.FuturesDifferences.AddAsync(entity);

            return createdEntity.Entity;
        }

        public async Task<bool> Delete(Guid deleteId)
        {
            await _db.FuturesDifferences.Where(x => x.Id == deleteId).ExecuteDeleteAsync();

            return true;
        }

        public IQueryable<FuturesDifference> GetAll()
        {
            return _db.FuturesDifferences;
        }

        public async Task<bool> SaveAsync()
        {
            await _db.SaveChangesAsync();

            return true;
        }

        public FuturesDifference Update(FuturesDifference entity)
        {
            var updatedEntity = _db.FuturesDifferences.Update(entity);

            return updatedEntity.Entity;
        }
    }
}
