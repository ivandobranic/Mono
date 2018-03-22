﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PagedList;
using Project.DAL;
using Project.Model;
using Project.Repository.Common;

namespace Project.Repository
{
    public class VehicleModelRepository : GenericRepository<VehicleModel>, IModelRepository
    {
       
        public VehicleModelRepository(VehicleContext _context) : base(_context)
        {

        }
        public VehicleContext Context
        {
            get { return context as VehicleContext;}
        }
        public async Task<IPagedList<VehicleModel>> GetPagedModel(IFilter filter)
        {
            var query = Context.Set<VehicleModel>().AsQueryable();
            query = filter.sortOrder == "name_desc" ? query.OrderByDescending(x => x.Name) : query.OrderBy(x => x.Name);
            if (filter.search != null)
            {
                filter.totalCount = await query.Where(x => x.Name == filter.search).CountAsync();
                query = query.Where(x => x.Name.ToLower() == filter.search.ToLower()).Skip((filter.pageNumber - 1) * filter.pageSize).Take(filter.pageSize);
            }
            else
            {
                filter.totalCount = await query.CountAsync();
                query = query.Skip((filter.pageNumber - 1) * filter.pageSize).Take(filter.pageSize);
            }

            return new StaticPagedList<VehicleModel>(query, filter.pageNumber, filter.pageSize, filter.totalCount);
        }
    }
}
