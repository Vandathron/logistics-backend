using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using logistics_BE.Data;
using logistics_BE.Domain;
using Microsoft.EntityFrameworkCore;

namespace logistics_BE.Repository
{
    public class CityRepo : ICityRepo
    {
        private AppDbContext _dbContext;

        public CityRepo(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<City>> CreateBulkCity(List<City> cities)
        {
            try
            {
                await _dbContext.Cities.AddRangeAsync(cities);
                await _dbContext.SaveChangesAsync();
                List<City> _cities = await _dbContext.Cities.ToListAsync();
                return await GetAllCities();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<City> CreateCity(City city)
        {
            try
            {
                var City = await _dbContext.Cities.AddAsync(city);
                await _dbContext.SaveChangesAsync();
                return City.Entity;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<City>> GetAllCities()
        {
            try
            {
                List<City> _cities = await _dbContext.Cities.FromSqlRaw("SELECT * FROM dbo.Cities").ToListAsync();
                return _cities;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<City> UpdateCity(City city)
        {
            try
            {
                var updatedCity = _dbContext.Cities.Update(city);
                await _dbContext.SaveChangesAsync();
                updatedCity.DetectChanges();
                return updatedCity.Entity;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }


    public interface ICityRepo
    {
        Task<List<City>> CreateBulkCity(List<City> cities);

        Task<List<City>> GetAllCities();

        Task<City> CreateCity(City city);

        Task<City> UpdateCity(City city);
    }
}
