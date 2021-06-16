using System;
using System.Threading.Tasks;
using logistics_BE.Contract.V1.Responses;

namespace logistics_BE.Services.Interface
{
    public interface ILogisticsService
    {
        Task<BaseResponse<int>> GetLogisticsCenter();
    }
}
