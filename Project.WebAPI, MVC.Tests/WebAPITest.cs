using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Results;
using FluentAssertions;
using Moq;
using MVC.Controllers;
using PagedList;
using Project.Model;
using Project.Repository;
using Project.Repository.Common;
using Project.Service.Common;
using Xunit;

namespace Project.WebAPI__MVC.Tests
{
    public class WebAPITest
    {

        VehicleModel model = new VehicleModel { Id = 2, MakeId = 2, Name = "Test", Abrv = "test" };
        Mock<IVehicleModelService> mockService = new Mock<IVehicleModelService>();
        IFilter filter = new Filter
        {
            Search = null,
            IsAscending = false,
            PageNumber = 1,
            TotalCount = 2
        };
        [Fact]
        public async Task GetPagedList_Success()
        {
            List<VehicleModel> vehicleModelList = new List<VehicleModel>
            {
               new VehicleModel{ Id = 2, MakeId = 1, Name = "AA", Abrv = "2" },
               new VehicleModel {Id = 3, MakeId = 2, Name = "BB", Abrv = "x1" }
            };

            mockService.Setup(x => x.PagedList(filter))
            .ReturnsAsync(new StaticPagedList<VehicleModel>(vehicleModelList, 1,
             2, 2));
            var controller = new VehicleModelAPIController(mockService.Object, filter);
            IHttpActionResult actionResult = await controller.Get(1, false, "", 2);
            var contentResult = actionResult as OkNegotiatedContentResult<IPagedList<VehicleModel>>;
            contentResult.Should().NotBeNull();
            contentResult.Content.TotalItemCount.Should().Be(2);
            contentResult.Content.PageNumber.Should().Be(1);
            contentResult.Content.PageSize.Should().Be(2);
        }


        [Fact]
        public async Task GetById_Success()
        {
            mockService.Setup(x => x.GetById(2)).ReturnsAsync(model);
            var controller = new VehicleModelAPIController(mockService.Object, filter);
            IHttpActionResult actionResult = await controller.Get(2);
            var contentResult = actionResult as OkNegotiatedContentResult<VehicleModel>;
            contentResult.Should().NotBeNull();
            contentResult.Content.Id.Should().Be(2);
            Assert.NotNull(contentResult.Content);


        }


        [Fact]
        public async Task GetById_Fail()
        {

            var controller = new VehicleModelAPIController(mockService.Object, filter);
            IHttpActionResult actionResult = await controller.Get(25);
            actionResult.Should().BeOfType(typeof(NotFoundResult));

        }
        [Fact]
        public async Task Post()
        {

            var controller = new VehicleModelAPIController(mockService.Object, filter);

            IHttpActionResult actionResult = await controller.Post(model);
            var contentResult = actionResult as CreatedAtRouteNegotiatedContentResult<VehicleModel>;
            contentResult.Should().NotBeNull();
            contentResult.RouteName.Should().BeEquivalentTo("VehicleModelRoute");



        }
        [Fact]
        public async Task Put_Success()
        {

            var controller = new VehicleModelAPIController(mockService.Object, filter);

            IHttpActionResult actionResult = await controller.Put(model);
            var contentResult = actionResult as NegotiatedContentResult<VehicleModel>;

            contentResult.Should().NotBeNull();
            contentResult.StatusCode.Should().Be(HttpStatusCode.Accepted);
            contentResult.Content.Id.Should().Be(2);
            Assert.NotNull(contentResult.Content);

        }

        [Fact]
        public async Task Put_Fail()
        {

            var controller = new VehicleModelAPIController(mockService.Object, filter);

            IHttpActionResult actionResult = await controller.Put(null);
            var contentResult = actionResult as NegotiatedContentResult<VehicleModel>;

            contentResult.Should().BeNull();
            actionResult.Should().BeOfType(typeof(NotFoundResult));


        }

        [Fact]
        public async Task Delete_Succes()
        {

            var controller = new VehicleModelAPIController(mockService.Object, filter);

            IHttpActionResult actionResult = await controller.Delete(model.Id);
            actionResult.Should().BeOfType(typeof(OkResult));
        }
     
    }

}

