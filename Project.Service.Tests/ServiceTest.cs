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
using Project.Repository;
using PagedList;

namespace Service.Tests
{
    public class ServiceTest
    {
    
        Mock<IUnitOfWork> mockUnitOfWork = new Mock<IUnitOfWork>();
    
   
        [Fact]
        public async Task GetByIdAsync_Success()
        {
            List<VehicleModel> vehicleModelList = new List<VehicleModel>
            {
               new VehicleModel { Id = 2, MakeId = 1, Name = "X2", Abrv = "x2" },
               new VehicleModel {Id = 3, MakeId = 2, Name = "X1", Abrv = "x1" }
            };
            mockUnitOfWork.Setup(x => x.ModelRepository.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync((int id) => vehicleModelList.Where(v => v.Id == id).Single());
            var vehicleModelService = new VehicleModelService(mockUnitOfWork.Object);
            var result = await vehicleModelService.GetById(2);
            result.ShouldBeEquivalentTo(vehicleModelList.First());
        }

        [Fact]
        public async Task Create_Success()
        {
            var newVehicleModel = new VehicleModel { Id = 5, MakeId = 3, Name = "306", Abrv = "306" };
            mockUnitOfWork.Setup(x => x.ModelRepository.InsertAsync(It.IsAny<VehicleModel>())).ReturnsAsync(1);
            mockUnitOfWork.Setup(x => x.CommitAsync()).ReturnsAsync(1);
            var vehicleModelService = new VehicleModelService(mockUnitOfWork.Object);
            var result = await vehicleModelService.Create(newVehicleModel);
            result.ShouldBeEquivalentTo(1);
       
        }

        
        [Fact]
        public async Task Delete_Success()
        {
            var vehicleModel = new VehicleModel { Id = 5, MakeId = 3, Name = "306", Abrv = "306" };

            mockUnitOfWork.Setup(x => x.ModelRepository.DeleteAsync(It.IsAny<VehicleModel>()))
                .ReturnsAsync(1);
            mockUnitOfWork.Setup(x => x.CommitAsync()).ReturnsAsync(1);
            var vehicleModelService = new VehicleModelService(mockUnitOfWork.Object);
            var result = await vehicleModelService.Delete(vehicleModel);
            result.ShouldBeEquivalentTo(1);
        }

        [Fact]
        public async Task Update_Succes()
        {

            
            var vehicleModel = new VehicleModel { Id = 5, MakeId = 3, Name = "306", Abrv = "306" };
            mockUnitOfWork.Setup(x => x.ModelRepository.UpdateAsync(It.IsAny<VehicleModel>()))
                .ReturnsAsync(1);
            mockUnitOfWork.Setup(x => x.CommitAsync()).ReturnsAsync(1);
            var vehicleModelService = new VehicleModelService(mockUnitOfWork.Object);
            var result = await vehicleModelService.Update(vehicleModel);
            result.ShouldBeEquivalentTo(1);
        
        }

        [Fact]
        public async Task GetPagedList_Success()
        {
            List<VehicleModel> vehicleModelList = new List<VehicleModel>
            {
               new VehicleModel { Id = 2, MakeId = 1, Name = "X2", Abrv = "x2" },
               new VehicleModel {Id = 3, MakeId = 2, Name = "X1", Abrv = "x1" }
            };
            var filter = new Filter
            {
             Search = null,
             IsAscending = false,
             PageNumber = 1,
             PageSize = 2,
             TotalCount = 2
             };
            mockUnitOfWork.Setup(x => x.ModelRepository.GetPagedModel(filter))
                .ReturnsAsync(new StaticPagedList<VehicleModel>(vehicleModelList,
                filter.PageNumber, filter.PageSize, filter.TotalCount));
            var vehicleModelService = new VehicleModelService(mockUnitOfWork.Object);
            var result = await vehicleModelService.PagedList(filter);
            result.Should().NotBeNull();
            result.PageNumber.ShouldBeEquivalentTo(1);
            result.TotalItemCount.ShouldBeEquivalentTo(2);
            result.ToList().Count().ShouldBeEquivalentTo(2);

        }


    }
}
