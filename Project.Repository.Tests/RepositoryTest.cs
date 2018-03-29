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
using Project.DAL.Entities;
using Project.Model;
using Project.Repository.Common;
using Xunit;

namespace Project.Repository.Tests
{
    public class RepositoryTest
    {
        List<VehicleModelEntity> VehicleModelEntityList = new List<VehicleModelEntity>
            {
               new VehicleModelEntity { Id = 2, MakeId = 1, Name = "X2", Abrv = "2" },
               new VehicleModelEntity {Id = 3, MakeId = 2, Name = "X1", Abrv = "x1" }
            };

        VehicleModelEntity VehicleModelEntity = new VehicleModelEntity { Id = 2, MakeId = 1, Name = "X5", Abrv = "x5" };

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
        public async Task CreateVehicleModelEntity_Success()
        {


            var mockSet = new Mock<DbSet<VehicleModelEntity>>();
            var mockContext = new Mock<VehicleContext>();
            mockContext.Setup(x => x.Set<VehicleModelEntity>()).Returns(mockSet.Object);
            var modelRepository = new GenericRepository<VehicleModelEntity>(mockContext.Object);
            var insertResult = await modelRepository.InsertAsync(VehicleModelEntity);
            insertResult.ShouldBeEquivalentTo(1);


        }



        [Fact]
        public void GetAll_Success()
        {


            var mockContext = new Mock<VehicleContext>();
            var mockSet = GetQueryableMockDbSet(VehicleModelEntityList);
            mockContext.Setup(x => x.Set<VehicleModelEntity>()).Returns(mockSet.Object);
            mockContext.Setup(x => x.VehicleModel).Returns(mockSet.Object);
            var modelrepository = new GenericRepository<VehicleModelEntity>(mockContext.Object);
            var repositoryResult = modelrepository.Get();
            var result = mockContext.Object.VehicleModel.Count();
            result.ShouldBeEquivalentTo(repositoryResult.Count());

        }



        [Fact]
        public async Task GetVehicleModelEntityById_Success()
        {
            var mockContext = new Mock<VehicleContext>();
            var mockSet = GetQueryableMockDbSet(VehicleModelEntityList);

            mockSet.Setup(x => x.FindAsync(It.IsAny<object[]>()))
            .Returns<object[]>(ids => Task.FromResult(VehicleModelEntityList.Find(t => t.Id == (int)ids[0])));
            mockContext.Setup(x => x.VehicleModel).Returns(mockSet.Object);
            mockContext.Setup(x => x.Set<VehicleModelEntity>()).Returns(mockSet.Object);
            var result = await mockContext.Object.VehicleModel.FindAsync((2));
            var modelrepository = new GenericRepository<VehicleModelEntity>(mockContext.Object);
            var repositoryResult = await modelrepository.GetByIdAsync(2);
            repositoryResult.ShouldBeEquivalentTo(result);

        }

        [Fact]
        public async Task GetVehicleModelEntityById_Fail()
        {
            var mockContext = new Mock<VehicleContext>();
            var mockSet = GetQueryableMockDbSet(VehicleModelEntityList);

            mockSet.Setup(x => x.FindAsync(It.IsAny<object[]>()))
            .Returns<object[]>(ids => Task.FromResult(VehicleModelEntityList.Find(t => t.Id == (int)ids[0])));
            mockContext.Setup(x => x.VehicleModel).Returns(mockSet.Object);
            mockContext.Setup(x => x.Set<VehicleModelEntity>()).Returns(mockSet.Object);
            var result = await mockContext.Object.VehicleModel.FindAsync((5));
            var modelrepository = new GenericRepository<VehicleModelEntity>(mockContext.Object);
            var repositoryResult = await modelrepository.GetByIdAsync(5);
            repositoryResult.ShouldBeEquivalentTo(null);

        }


        [Fact]
        public async Task Delete_VehicleModelEntity_Succes()
        {


            var mockContext = new Mock<VehicleContext>();
            var mockSet = new Mock<DbSet<VehicleModelEntity>>();
            mockContext.Setup(x => x.VehicleModel).Returns(mockSet.Object);
            var modelrepository = new GenericRepository<VehicleModelEntity>(mockContext.Object);
            var deleteResult = await modelrepository.DeleteAsync(1);
            deleteResult.ShouldBeEquivalentTo(1);

        }



        [Fact]

        public async Task Update_VehicleModelEntity_Succes()
        {

            var mockContext = new Mock<VehicleContext>();
            var mockSet = new Mock<DbSet<VehicleModelEntity>>();
            mockContext.Setup(x => x.VehicleModel).Returns(mockSet.Object);
            var modelrepository = new GenericRepository<VehicleModelEntity>(mockContext.Object);
            var updateResult = await modelrepository.UpdateAsync(VehicleModelEntity);
            updateResult.ShouldBeEquivalentTo(1);

        }
    }
}
