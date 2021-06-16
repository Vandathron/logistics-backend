using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using logistics_BE.Contract.V1.Request;
using logistics_BE.Contract.V1.Responses;
using logistics_BE.Domain;
using logistics_BE.Repository;
using logistics_BE.Services.Interface;

namespace logistics_BE.Services.Implementation
{
    public class RoadService : IRoadService
    {
        private IRoadRepo _roadRepo;
        private IResponseCacheService _responseCacheService;

        public RoadService(IRoadRepo roadRepo, IResponseCacheService responseCacheService)
        {
            _roadRepo = roadRepo;
            _responseCacheService = responseCacheService;
        }

        public async Task<BaseResponse<RoadResponse>> CreateRoad(RoadRequest road)
        {
            try
            {
                var createdRoad = await _roadRepo.CreateRoad(mapRequestToDomain(road));
                await EmptyLogisticCenterCache();
                return new BaseResponse<RoadResponse>
                {
                    data = new RoadResponse
                    {
                        Distance = createdRoad.Distance,
                        StartCityId = createdRoad.StartCityId,
                        EndCityId = createdRoad.EndCityId,
                        Id = createdRoad.Id
                    },

                    status = true,
                    ErrorMessage = null
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<RoadResponse> { data = null, ErrorMessage = ex.Message, status = false };
            }
            throw new NotImplementedException();
        }

        public async Task<BaseResponse<List<RoadResponse>>> GetRoads()
        {

            try
            {
                List<Road> roads = await _roadRepo.GetAllRoads();
                return new BaseResponse<List<RoadResponse>>
                {
                    data = roads.Select(road => new RoadResponse
                    {
                        Distance = road.Distance,
                        StartCityId = road.StartCityId,
                        EndCityId = road.EndCityId,
                        Id = road.Id
                    }).ToList(),
                    status = true,
                    ErrorMessage = null
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<List<RoadResponse>> { data = null, ErrorMessage = ex.Message, status = false };
            }
        }

        public async Task<BaseResponse<RoadResponse>> UpdateRoad(int roadId, RoadRequest road)
        {
            try
            {
                var updatedRoad = await _roadRepo.UpdateRoad(new Road { Distance = road.Distance, EndCityId = road.EndCityId, StartCityId = road.StartCityid, Id = roadId });
                await EmptyLogisticCenterCache();

                return new BaseResponse<RoadResponse> {
                    data = new RoadResponse {
                        Distance = updatedRoad.Distance,
                        StartCityId = updatedRoad.StartCityId,
                        EndCityId = updatedRoad.EndCityId,
                        Id = updatedRoad.Id
                    }, ErrorMessage = null, status = true };
            }
            catch (Exception ex)
            {
                return new BaseResponse<RoadResponse> { data = null, ErrorMessage = ex.Message, status = false };
            }
        }

        private Road mapRequestToDomain(RoadRequest road)
        {
            return new Road
            {
                Distance = road.Distance,
                StartCityId = road.StartCityid,
                EndCityId = road.EndCityId
            };
        }

        private async Task EmptyLogisticCenterCache()
        {
            await _responseCacheService.EmptyCache("LATEST-RESULT");
        }
    }
}
