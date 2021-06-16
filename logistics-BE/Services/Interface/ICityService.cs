using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using logistics_BE.Contract.V1.Request;
using logistics_BE.Contract.V1.Responses;

namespace logistics_BE.Services.Interface
{
    public interface ICityService
    {
        public Task<BaseResponse<List<CityResponse>>> GetCities();

        public Task<BaseResponse<CityResponse>> CreateCity(CityRequest cityRequest);

        public Task<BaseResponse<List<CityResponse>>> CreateBulkCities(List<CityRequest> cityRequests);

        public Task<BaseResponse<CityResponse>> UpdateCity(int cityId, CityRequest cityRequest);

    }
}
