using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using logistics_BE.Cache;
using logistics_BE.Contract;
using logistics_BE.Services.Interface;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace logistics_BE.Controllers.V1
{
    [ApiController]
    public class LogisticsController : ControllerBase
    {
        private ILogisticsService _logisticsService;

        public LogisticsController(ILogisticsService logisticsService)
        {
            _logisticsService = logisticsService;
        }


        [HttpGet(ApiRoutes.LogisticsCenterApi.GetLogisticsCenter)]
        [Cached()]
        public async Task<IActionResult> GetLogisticsCenter()
        {
            var response = await _logisticsService.GetLogisticsCenter();
            return Ok(response);
        }
    }

   
}
