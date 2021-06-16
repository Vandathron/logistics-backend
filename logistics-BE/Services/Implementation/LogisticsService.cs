using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using logistics_BE.Contract.V1.Responses;
using logistics_BE.Domain;
using logistics_BE.Repository;
using logistics_BE.Services.Interface;

namespace logistics_BE.Services
{
    public class LogisticsService : ILogisticsService
    {
        private ICityService _cityService;
        private IRoadService _roadService;
        private ILogisticsCenterRepo _logisticsCenterRepo;

        public LogisticsService(ICityService cityService, IRoadService roadService, ILogisticsCenterRepo logisticsCenterRepo)
        {
            _cityService = cityService;
            _roadService = roadService;
            _logisticsCenterRepo = logisticsCenterRepo;
        }

        public async Task<BaseResponse<int>> GetLogisticsCenter()
        {
            var cityRes = await _cityService.GetCities();
            var roadRes = await _roadService.GetRoads();

            int closestCityid = GetCityIdAsLogisticsCenter(cit: cityRes.data, roads: roadRes.data);

            if(closestCityid == 0)
            {
                return new BaseResponse<int> { data = 0, status = true, ErrorMessage = null };
            }


            var center = await _logisticsCenterRepo.getCity(closestCityid);

            //Do not create a logistics center if already present
            if(center != null)
            {
                return new BaseResponse<int> { ErrorMessage = "Already exists", data = closestCityid, status = false };
            }

            var logisticsCenter = await _logisticsCenterRepo.markAsLogisticsCenter(new LogisticsCenter { Id = 0, CityId = closestCityid });


            return new BaseResponse<int>
            {
                data = logisticsCenter.CityId,
                status = true,
                ErrorMessage = null
            };

        }


        public int GetCityIdAsLogisticsCenter(List<CityResponse> cit, List<RoadResponse> roads)
        {
            List<CityWithTotalDistance> cities = cit.Select(c => new CityWithTotalDistance { Distance = 0, Id = c.Id, Name = c.Name }).ToList();
            // Sum up all the distance between immediate cities of each city
            CityWithTotalDistance farthestCity = new CityWithTotalDistance();

            for (int i = 0; i < cities.Count; i++)
            {
                var city = cities[i];
                city.Distance = roads.FindAll(road => road.EndCityId == city.Id).Sum(x => x.Distance);
                if (farthestCity.Distance <= city.Distance)
                {
                    farthestCity = city;
                }
            }

            // Find the closest city id to the largest city

            var farthestCityRoads = roads.FindAll(x => x.EndCityId == farthestCity.Id);

            int closestCityId = 0;
            if (farthestCityRoads.Count <= 0) return closestCityId;
            double distance = farthestCityRoads[0].Distance;

            foreach (var road in farthestCityRoads)
            {
                if(road.EndCityId == farthestCity.Id)
                {
                    if( road.Distance <= distance)
                    {
                        distance = road.Distance;
                        closestCityId = road.StartCityId;
                    }
                }
            }

            return closestCityId;
        }
    }



    struct CityWithTotalDistance
    {
        public int Id;
        public double Distance;
        public string Name;
    }

}
