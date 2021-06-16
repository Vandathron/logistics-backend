using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using logistics_BE.Contract;
using logistics_BE.Contract.V1.Request;
using logistics_BE.Services.Interface;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace logistics_BE.Controllers.V1
{
    public class CityController : ControllerBase
    {

        private ICityService _cityService;

        public CityController(ICityService cityService)
        {
            _cityService = cityService;
        }


        [HttpGet(ApiRoutes.CityAPI.GetAll)]
        public async Task<IActionResult> GetAllCitiesAsync()
        {
            try
            {
                var response = await _cityService.GetCities();
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(Response);
            }
        }

        [HttpPost(ApiRoutes.CityAPI.Post)]
        public async Task<IActionResult> CreateCityAsync([FromBody] CityRequest city)
        {
            try
            {
                var response = await _cityService.CreateCity(city);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(Response);
            }
        }

        [HttpPost(ApiRoutes.CityAPI.PostBulk)]
        public async Task<IActionResult> CreateBulkCityAsync([FromBody] List<CityRequest> cities)
        {
            try
            {
                var response = await _cityService.CreateBulkCities(cities);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(Response);
            }
        }


        [HttpPatch(ApiRoutes.CityAPI.Update)]
        public async Task<IActionResult> UpdateCityAsync([FromBody] CityRequest city, [FromQuery] int Id)
        {
            try
            {
                var response = await _cityService.UpdateCity(Id, city);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(Response);
            }
        }

    }
}
