using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using FluentAssertions;
using Moq;
using Project.Model;
using Project.Repository.Common;
using Project.Service;

namespace Service.Tests
{
    public class ServiceTest
    {
        List<VehicleModel> vehicleModelList = new List<VehicleModel>
            {
               new VehicleModel { Id = 2, MakeId = 1, Name = "X2", Abrv = "x2" },
               new VehicleModel {Id = 3, MakeId = 2, Name = "X1", Abrv = "x1" }
            };
        Mock<IRepository<VehicleModel>> repositoryModel = new Mock<IRepository<VehicleModel>>();
        [Fact]
        public void GetAll()
        {
          
            repositoryModel.Setup(x => x.Get()).Returns(vehicleModelList.AsQueryable());
            var vehicleModelService  = new VehicleModelService(repositoryModel.Object);
            var result = vehicleModelService.GetAll();
            result.Count().ShouldBeEquivalentTo(2);
        }

        [Fact]
        public async Task GetByIdAsync()
        {

            repositoryModel.Setup(x => x.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync((int id) => vehicleModelList.Where(v => v.Id == id).Single());
            var vehicleModelService = new VehicleModelService(repositoryModel.Object);
            var result = await vehicleModelService.GetById(2);
            result.ShouldBeEquivalentTo(vehicleModelList.First());
        }

        [Fact]
        public async Task Create()
        {
            var newVehicleModel = new VehicleModel { Id = 5, MakeId = 3, Name = "306", Abrv = "306" };
            repositoryModel.Setup(x => x.InsertAsync(It.IsAny<VehicleModel>())).ReturnsAsync(newVehicleModel);
            var vehicleModelService = new VehicleModelService(repositoryModel.Object);
            var result = await vehicleModelService.Create(newVehicleModel);
            result.ShouldBeEquivalentTo(newVehicleModel);
       
        }

        
        [Fact]
        public async Task Delete()
        {
            var vehicleModel = new VehicleModel { Id = 5, MakeId = 3, Name = "306", Abrv = "306" };

            repositoryModel.Setup(x => x.DeleteAsync(It.IsAny<VehicleModel>()))
                .ReturnsAsync(vehicleModel.Id);
            var vehicleModelService = new VehicleModelService(repositoryModel.Object);
            var result = await vehicleModelService.Delete(vehicleModel);
            result.ShouldBeEquivalentTo(5);
        }

        [Fact]
        public async Task Update()
        {

            
            var vehicleModel = new VehicleModel { Id = 5, MakeId = 3, Name = "306", Abrv = "306" };
            repositoryModel.Setup(x => x.UpdateAsync(It.IsAny<VehicleModel>()))
                .ReturnsAsync(vehicleModel.Id);
            var vehicleModelService = new VehicleModelService(repositoryModel.Object);
            var result = await vehicleModelService.Update(vehicleModel);
            result.ShouldBeEquivalentTo(5);
        
        }


    }
}
