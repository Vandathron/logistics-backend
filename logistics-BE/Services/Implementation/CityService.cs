using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using logistics_BE.Contract.V1.Request;
using logistics_BE.Contract.V1.Responses;
using logistics_BE.Domain;
using logistics_BE.Repository;
using logistics_BE.Services.Interface;
using Microsoft.AspNetCore.Http;

namespace logistics_BE.Services.Implementation
{
    public class CityService : ICityService
    {
        private ICityRepo _cityRepo;
        private IResponseCacheService _responseCacheService;

        public CityService(ICityRepo cityRepo, IResponseCacheService responseCacheService)
        {
            _cityRepo = cityRepo;
            _responseCacheService = responseCacheService;
        }

        public async Task<BaseResponse<List<CityResponse>>> CreateBulkCities(List<CityRequest> cityRequests)
        {
            try
            {
                var createdCities = await _cityRepo.CreateBulkCity(cityRequests.Select(city => mapRequestToDomain(city)).ToList());
                await EmptyLogisticCenterCache();
                return new BaseResponse<List<CityResponse>>
                {
                    data = createdCities.Select(city => new CityResponse { Name = city.Name, Id = city.Id }).ToList(),
                    status = true,
                    ErrorMessage = null
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<List<CityResponse>>
                {
                    data = null,
                    status = false,
                    ErrorMessage = ex.Message
                };
            }
        }

        public async Task<BaseResponse<CityResponse>> CreateCity(CityRequest cityRequest)
        {
            try
            {
                var createdCity = await _cityRepo.CreateCity(mapRequestToDomain(cityRequest));
                await EmptyLogisticCenterCache();
                return new BaseResponse<CityResponse>
                {
                    data = new CityResponse { Name = createdCity.Name, Id = createdCity.Id },
                    status = true,
                    ErrorMessage = null
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<CityResponse> { data = null, ErrorMessage = ex.Message, status = false };
            }
        }

        public async Task<BaseResponse<List<CityResponse>>> GetCities()
        {
            try
            {
                var cities = await _cityRepo.GetAllCities();
                return new BaseResponse<List<CityResponse>>
                {
                    data = cities.Select(city => new CityResponse { Name = city.Name, Id = city.Id }).ToList(),
                    status = true,
                    ErrorMessage = null
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<List<CityResponse>>
                {
                    data = null,
                    status = false,
                    ErrorMessage = ex.Message
                };
            }

        }

        public async Task<BaseResponse<CityResponse>> UpdateCity(int cityId, CityRequest cityRequest)
        {
            try
            {
                var updatedCity = await _cityRepo.UpdateCity(new City { Id = cityId, Name = cityRequest.Name });

                await EmptyLogisticCenterCache();
                return new BaseResponse<CityResponse>
                {
                    data = new CityResponse
                    {
                        Name = updatedCity.Name,
                        Id = updatedCity.Id
                    },
                    ErrorMessage = null,
                    status = true
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<CityResponse> { data = null, ErrorMessage = ex.Message, status = false };
            }
        }

        private City mapRequestToDomain(CityRequest city)
        {
            return new City
            {
                Name = city.Name,
                Id = 0
            };
        }

        private async Task EmptyLogisticCenterCache()
        {
            await _responseCacheService.EmptyCache("LATEST-RESULT");
        }
    }
}
