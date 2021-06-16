using System;
using System.Linq;
using System.Threading.Tasks;
using logistics_BE.Data;
using logistics_BE.Domain;
using Microsoft.EntityFrameworkCore;

namespace logistics_BE.Repository
{
    public class LogisticsCenterRepo : ILogisticsCenterRepo
    {
        private AppDbContext _dbContext;

        public LogisticsCenterRepo(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<LogisticsCenter> getCity(int cityId)
        {
            try
            {
                var center = await _dbContext.LogisticsCenters.SingleAsync(x => x.CityId == cityId);
                return center;
            }
            catch (Exception ex)
            {
                return null;
            }

        }

        public async Task<LogisticsCenter> markAsLogisticsCenter(LogisticsCenter logisticsCenter)
        {
            var createdCenter = await _dbContext.LogisticsCenters.AddAsync(logisticsCenter);
            await _dbContext.SaveChangesAsync();

            return new LogisticsCenter
            {
                Id = createdCenter.Entity.Id,
                CityId = createdCenter.Entity.CityId
            };
        }
    }


    public interface ILogisticsCenterRepo
    {
        Task<LogisticsCenter> markAsLogisticsCenter(LogisticsCenter logisticsCenter);

        Task<LogisticsCenter> getCity(int cityId);
    }
}
