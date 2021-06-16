using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using logistics_BE.Contract.V1.Request;
using logistics_BE.Contract.V1.Responses;
using logistics_BE.Domain;

namespace logistics_BE.Services.Interface
{
    public interface IRoadService
    {
        Task<BaseResponse<List<RoadResponse>>> GetRoads();

        Task<BaseResponse<RoadResponse>> CreateRoad(RoadRequest road);

        Task<BaseResponse<RoadResponse>> UpdateRoad(int roadId, RoadRequest road);

    }
}
