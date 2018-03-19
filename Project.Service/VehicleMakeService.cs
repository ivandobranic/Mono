﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using PagedList;
using Project.Common.Caching;
using Project.Model;
using Project.Repository.Common;
using Project.Service.Common;

namespace Project.Service
{
    public class VehicleMakeService : IVehicleMakeService
    {

        IUnitOfWork unitOfWork;
        IPaging paging;
     
        public VehicleMakeService(IUnitOfWork _unitOfWork, IPaging _paging)
        {
            this.unitOfWork = _unitOfWork;
            this.paging = _paging;
        }


        public async Task<VehicleMake> GetByIdAsync(int id)
        {

            return await unitOfWork.GetByIdAsync<VehicleMake>(id);
               
        }

        public async Task<int> CreateAsync(VehicleMake VehicleMake)
        {
            
            await unitOfWork.InsertAsync(VehicleMake);
            return await unitOfWork.CommitAsync();
        }

        public async Task<int> UpdateAsync(VehicleMake VehicleMake)
        {
            
            await unitOfWork.UpdateAsync(VehicleMake);
            return await unitOfWork.CommitAsync();
        }

        public async Task<int> DeleteAsync(VehicleMake VehicleMake)
        {
           await unitOfWork.DeleteAsync(VehicleMake);
           return await unitOfWork.CommitAsync();
        }

        public async Task<StaticPagedList<VehicleMake>> PagedList(string sortOrder, string search, int pageNumber, int pageSize)
        {
            return await paging.GetPagedResultMake(sortOrder, search, pageNumber, pageSize);
        }
    }
}
