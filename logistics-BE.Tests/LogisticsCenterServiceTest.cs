using System;
using System.Collections.Generic;
using logistics_BE.Contract.V1.Responses;
using logistics_BE.Services;
using logistics_BE.Services.Implementation;
using Xunit;
using Moq;
using logistics_BE.Services.Interface;
using logistics_BE.Repository;
using logistics_BE.Domain;

namespace logistics_BE.Tests
{
    public class LogisticsCenterServiceTest
    {
        private readonly LogisticsService _sub;
        private readonly Mock<ICityService> _cityServiceMock = new Mock<ICityService>();
        private readonly Mock<IRoadService> _roadServiceMock = new Mock<IRoadService>();

        private readonly Mock<ILogisticsCenterRepo> _logisticsRepoMock = new Mock<ILogisticsCenterRepo>();

        public LogisticsCenterServiceTest()
        {
            _sub = new LogisticsService(_cityServiceMock.Object, _roadServiceMock.Object, _logisticsRepoMock.Object);
        }

        [Fact]
        public async void GetLogisticsCenter_ShouldNotCreateCenter_WhenThereIsNoChangeInCityOrRoads()
        {
            List<CityResponse> cities = new List<CityResponse>()
            {
                new CityResponse { Id = 1, Name = "City A", Tag = ""},
                new CityResponse { Id = 2, Name = "City B", Tag = ""},
                new CityResponse { Id = 3, Name = "City C", Tag = ""},
                new CityResponse { Id = 4, Name = "City D", Tag = ""}
            };


            List<RoadResponse> roads = new List<RoadResponse>()
            {
                new RoadResponse { Id = 1, StartCityId = 1, EndCityId = 2, Distance = 3},
                new RoadResponse { Id = 2, StartCityId = 1, EndCityId = 3, Distance = 5},
                new RoadResponse { Id = 3, StartCityId = 4, EndCityId = 1, Distance = 6},
                new RoadResponse { Id = 4, StartCityId = 4, EndCityId = 3, Distance = 5},
            };

            var closestCityId = _sub.GetCityIdAsLogisticsCenter(cities, roads);

            _logisticsRepoMock.Setup(x => x.getCity(closestCityId)).ReturnsAsync(new LogisticsCenter { CityId = closestCityId });
            _cityServiceMock.Setup(x => x.GetCities()).ReturnsAsync(new BaseResponse<List<CityResponse>>() { data = cities });
            _roadServiceMock.Setup(b => b.GetRoads()).ReturnsAsync(new BaseResponse<List<RoadResponse>>() { data = roads });

            var result = await _sub.GetLogisticsCenter();
            _logisticsRepoMock.Verify(x => x.markAsLogisticsCenter(It.IsAny<LogisticsCenter>()), Times.Never());

        }
    }
}
