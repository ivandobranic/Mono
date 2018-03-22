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
        public async Task CreateVehicleModel()
        {
            
            
            var mockSet = new Mock<DbSet<VehicleModel>>();
            var mockContext = new Mock<VehicleContext>();
            mockContext.Setup(x => x.Set<VehicleModel>()).Returns(mockSet.Object);
            var modelrepository = new GenericRepository<VehicleModel>(mockContext.Object);
            var repositoryResult = await modelrepository.InsertAsync(vehicleModel);
            mockSet.Verify(x => x.Add(vehicleModel), Times.Once());
            mockContext.Verify(x => x.SaveChangesAsync(), Times.Once());
            vehicleModel.ShouldBeEquivalentTo(repositoryResult);

        }

     
 
        [Fact]
        public void GetAll()
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
            var modelrepository = new GenericRepository<VehicleModel>(mockContext.Object);
            var repositoryResult = await modelrepository.GetByIdAsync(2);
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
            var modelrepository = new GenericRepository<VehicleModel>(mockContext.Object);
            var repositoryResult = await modelrepository.GetByIdAsync(5);
            repositoryResult.ShouldBeEquivalentTo(null);

        }


        [Fact]
        public async Task Delete_VehicleModel_Succes()
        {



            var mockContext = new Mock<VehicleContext>();
            var mockSet = new Mock<DbSet<VehicleModel>>();
            mockContext.Setup(x => x.VehicleModel).Returns(mockSet.Object);
            var modelrepository = new Mock<GenericRepository<VehicleModel>>(mockContext.Object);
            modelrepository.Setup(x => x.DeleteAsync(vehicleModel));
            var result = await modelrepository.Object.DeleteAsync(vehicleModel);
    

        }



        [Fact]

        public async Task Update_VehicleModel_Succes()
        {

            var mockContext = new Mock<VehicleContext>();
            var mockSet = new Mock<DbSet<VehicleModel>>();
            mockContext.Setup(x => x.VehicleModel).Returns(mockSet.Object);
            var modelrepository = new Mock<GenericRepository<VehicleModel>>(mockContext.Object);
            modelrepository.Setup(x => x.DeleteAsync(vehicleModel));
            var result = await modelrepository.Object.UpdateAsync(vehicleModel);
            
           
        }
    }
}
