using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using Moq;
using Project.DAL;
using Project.Model;
using Project.Repository.Common;
using Xunit;

namespace Project.Repository.Tests
{
    public class RepositoryTest
    {
            List<VehicleModel> vehicleModelList = new List<VehicleModel>
            {
               new VehicleModel { Id = 2, MakeId = 1, Name = "X2", Abrv = "2" },
               new VehicleModel {Id = 3, MakeId = 2, Name = "X1", Abrv = "x1" }
            };

       VehicleModel vehicleModel = new VehicleModel { Id = 2, MakeId = 1, Name = "X5", Abrv = "x5" };

        private static Mock<DbSet<T>> GetQueryableMockDbSet<T>(List<T> sourceList) where T : class
        {
            var queryable = sourceList.AsQueryable();
            var dbSet = new Mock<DbSet<T>>();

            dbSet.As<IQueryable<T>>().Setup(m => m.Provider).Returns(queryable.Provider);
            dbSet.As<IQueryable<T>>().Setup(m => m.Expression).Returns(queryable.Expression);
            dbSet.As<IQueryable<T>>().Setup(m => m.ElementType).Returns(queryable.ElementType);
            dbSet.As<IQueryable<T>>().Setup(m => m.GetEnumerator()).Returns(() => queryable.GetEnumerator());
            dbSet.Setup(d => d.Add(It.IsAny<T>())).Callback<T>((s) => sourceList.Add(s));
            dbSet.As<IDbAsyncEnumerable<T>>().Setup(m => m.GetAsyncEnumerator())
                .Returns(new TestDbAsyncEnumerator<T>(sourceList.GetEnumerator()));
            dbSet.As<IQueryable<T>>().Setup(m => m.Provider)
                .Returns(new TestDbAsyncQueryProvider<T>(queryable.Provider));

            return dbSet;
        }

        [Fact]
        public async Task CreateVehicleModel_Success()
        {
            
            
            var mockSet = new Mock<DbSet<VehicleModel>>();
            var mockContext = new Mock<VehicleContext>();
            mockContext.Setup(x => x.Set<VehicleModel>()).Returns(mockSet.Object);
            var UnitOfWork = new Mock<UnitOfWork>(mockContext.Object);
            var insertResult = await UnitOfWork.Object.ModelRepository.InsertAsync(vehicleModel);
            insertResult.ShouldBeEquivalentTo(1);


        }

     
 
        [Fact]
        public void GetAll_Success()
        {
           
            
            var mockContext = new Mock<VehicleContext>();
            var mockSet = GetQueryableMockDbSet(vehicleModelList);
            mockContext.Setup(x => x.Set<VehicleModel>()).Returns(mockSet.Object);
            mockContext.Setup(x => x.VehicleModel).Returns(mockSet.Object);
            var modelrepository = new GenericRepository<VehicleModel>(mockContext.Object);
            var repositoryResult = modelrepository.Get();
            var result = mockContext.Object.VehicleModel.Count();
            result.ShouldBeEquivalentTo(repositoryResult.Count());
            
        }



        [Fact]
        public async Task GetVehicleModelById_Success()
        {
            var mockContext = new Mock<VehicleContext>();
            var mockSet = GetQueryableMockDbSet(vehicleModelList);
       
            mockSet.Setup(x => x.FindAsync(It.IsAny<object[]>()))
            .Returns<object[]>(ids => Task.FromResult(vehicleModelList.Find(t => t.Id == (int)ids[0])));
            mockContext.Setup(x => x.VehicleModel).Returns(mockSet.Object);
            mockContext.Setup(x => x.Set<VehicleModel>()).Returns(mockSet.Object);
            var result = await mockContext.Object.VehicleModel.FindAsync((2));
            var UnitOfWork = new Mock<UnitOfWork>(mockContext.Object);
            var repositoryResult = await UnitOfWork.Object.ModelRepository.GetByIdAsync(2);
            repositoryResult.ShouldBeEquivalentTo(result);

        }

        [Fact]
        public async Task GetVehicleModelById_Fail()
        {
            var mockContext = new Mock<VehicleContext>();
            var mockSet = GetQueryableMockDbSet(vehicleModelList);

            mockSet.Setup(x => x.FindAsync(It.IsAny<object[]>()))
            .Returns<object[]>(ids => Task.FromResult(vehicleModelList.Find(t => t.Id == (int)ids[0])));
            mockContext.Setup(x => x.VehicleModel).Returns(mockSet.Object);
            mockContext.Setup(x => x.Set<VehicleModel>()).Returns(mockSet.Object);
            var result = await mockContext.Object.VehicleModel.FindAsync((5));
            var UnitOfWork = new Mock<UnitOfWork>(mockContext.Object);
            var repositoryResult = await UnitOfWork.Object.ModelRepository.GetByIdAsync(5);
            repositoryResult.ShouldBeEquivalentTo(null);

        }


        [Fact]
        public async Task Delete_VehicleModel_Succes()
        {


            var mockContext = new Mock<VehicleContext>();
            var mockSet = new Mock<DbSet<VehicleModel>>();
            mockContext.Setup(x => x.VehicleModel).Returns(mockSet.Object);
            var UnitOfWork = new Mock<UnitOfWork>(mockContext.Object);
            var deleteResult = await UnitOfWork.Object.ModelRepository.DeleteAsync(vehicleModel);
            deleteResult.ShouldBeEquivalentTo(1);

        }



        [Fact]

        public async Task Update_VehicleModel_Succes()
        {

            var mockContext = new Mock<VehicleContext>();
            var mockSet = new Mock<DbSet<VehicleModel>>();
            mockContext.Setup(x => x.VehicleModel).Returns(mockSet.Object);
            var UnitOfWork = new Mock<UnitOfWork>(mockContext.Object);
            var updateResult = await UnitOfWork.Object.ModelRepository.UpdateAsync(vehicleModel);
            updateResult.ShouldBeEquivalentTo(1);
                 
        }
    }
}
