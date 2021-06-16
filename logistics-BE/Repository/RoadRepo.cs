using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using logistics_BE.Data;
using logistics_BE.Domain;
using Microsoft.EntityFrameworkCore;

namespace logistics_BE.Repository
{
    public class RoadRepo : IRoadRepo
    {
        private AppDbContext _dbContext;

        public RoadRepo(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Road>> CreateBulkRoad(List<Road> roads)
        {
            try
            {
                await _dbContext.Roads.AddRangeAsync(roads);
                await _dbContext.SaveChangesAsync();
                List<Road> _roads = await _dbContext.Roads.FromSqlRaw("SELECT * FROM dbo.Roads").ToListAsync();
                return await GetAllRoads();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Road> CreateRoad(Road road)
        {
            try
            {
                var Road = await _dbContext.Roads.AddAsync(road);
                await _dbContext.SaveChangesAsync();
                return Road.Entity;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Road> UpdateRoad(Road road)
        {
            try
            {
                var updatedRoad =  _dbContext.Roads.Update(road);
                await _dbContext.SaveChangesAsync();
                updatedRoad.DetectChanges();
                return updatedRoad.Entity;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<Road>> GetAllRoads()
        {
            try
            {
                List<Road> roads = await _dbContext.Roads.FromSqlRaw("SELECT * FROM dbo.Roads").ToListAsync();
                return roads;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }


    public interface IRoadRepo
    {
        Task<List<Road>> CreateBulkRoad(List<Road> roads);


        Task<Road> CreateRoad(Road road);


        Task<Road> UpdateRoad(Road road);

        Task<List<Road>> GetAllRoads();
    }
}
