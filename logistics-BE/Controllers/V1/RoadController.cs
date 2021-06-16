using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using logistics_BE.Contract;
using logistics_BE.Contract.V1.Request;
using logistics_BE.Services.Interface;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace logistics_BE.Controllers.V1
{
    [ApiController]
    public class RoadController : ControllerBase
    {
        private IRoadService _roadService;

        public RoadController(IRoadService roadService)
        {
            _roadService = roadService;
        }


        [HttpGet(ApiRoutes.RoadApi.GetAll)]
        public async Task<IActionResult> GetAllRoadsAsync()
        {
            try
            {
                var response = await _roadService.GetRoads();
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(Response);
            }
        }

        [HttpPost(ApiRoutes.RoadApi.Post)]
        public async Task<IActionResult> CreateRoadAsync([FromBody] RoadRequest road)
        {
            try
            {
                var response = await _roadService.CreateRoad(road);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(Response);
            }
        }

        [HttpPatch(ApiRoutes.RoadApi.Update)]
        public async Task<IActionResult> UpdateRoadAsync([FromBody] RoadRequest road, [FromQuery] int Id)
        {
            try
            {
                var response = await _roadService.UpdateRoad(Id, road);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(Response);
            }
        }

    }
}
