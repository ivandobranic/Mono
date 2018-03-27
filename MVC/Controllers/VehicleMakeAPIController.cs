using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using PagedList;
using Project.Common.Caching;
using Project.Common.Logging;
using Project.Model;
using Project.Repository;
using Project.Repository.Common;
using Project.Service.Common;

namespace MVC.Controllers
{

    public class VehicleMakeAPIController : ApiController
    {
        IVehicleMakeService vehiclemakeService;
        ICaching caching;
        IErrorLogger logError;
        IFilter filter;
        private readonly string[] MasterCacheKeyArray = { "VehicleMakeCache" };
        public VehicleMakeAPIController(IVehicleMakeService _vehiclemakeService, 
            ICaching _caching, IFilter _filter)
        {
            this.vehiclemakeService = _vehiclemakeService;
            this.caching = _caching;
            this.logError = ErrorLogger.GetInstance;
            this.filter = _filter;
        }

        [HttpGet]
        [Route("api/VehicleMakeAPI")]
        public async Task<IHttpActionResult> Get(int? pageNumber = null, bool? isAscending = null, string search = null)
        {
            try
            {

                filter.PageNumber = pageNumber ?? 1;
                filter.PageSize = 3;
                filter.Search = search;
                filter.IsAscending = isAscending ?? false;
              
               
                var pagedList = await vehiclemakeService.PagedList(filter);

                var newModel = new
                {
                    Model = pagedList.ToList(),
                    TotalCount = pagedList.TotalItemCount
                };

                return Ok(newModel);
            }
            catch(Exception ex)
            {
                logError.LogError(ex);
                return BadRequest(ex.Message);
            }

        }




        [HttpGet]
        [Route("api/VehicleMakeAPI/")]
        public async Task<IHttpActionResult> Get(int id)
        {
            try
            {
                string rawKey = "CacheById";
                VehicleMake vehicleMake = caching.GetCacheItem(rawKey, MasterCacheKeyArray) as VehicleMake;
                if (vehicleMake == null)
                {
                    vehicleMake = await vehiclemakeService.GetByIdAsync(id);
                    if (vehicleMake == null)
                    {
                        return NotFound();
                    }
                 caching.AddCacheItem(rawKey, vehicleMake, MasterCacheKeyArray);
                }
                return Ok(vehicleMake);
            }
            catch (Exception ex)
            {
                logError.LogError(ex);
                return BadRequest(ex.Message);
            }

        }

        [HttpPost]
        [Route("api/VehicleMakeAPI", Name = "VehicleMakeRoute")]
        public async Task<IHttpActionResult> Post([FromBody]VehicleMake model)
        {
            try
            {
                caching.InvalidateCache(MasterCacheKeyArray);
                await vehiclemakeService.CreateAsync(model);
                return CreatedAtRoute("VehicleMakeRoute", new { Id = model.Id }, model);
         
            }
            catch (Exception ex)
            {
                logError.LogError(ex);
                return BadRequest(ex.Message);
            }

        }


        [HttpPut]
        [Route("api/VehicleMakeAPI")]
        public async Task<IHttpActionResult> Put([FromBody] VehicleMake model)
        {
            try
            {

                if (model == null)
                {
                    return NotFound();

                }
                else
                {
                    caching.InvalidateCache(MasterCacheKeyArray);
                    await vehiclemakeService.UpdateAsync(model);
                    return Content(HttpStatusCode.Accepted, model);
                }

            }
            catch (Exception ex)
            {
                logError.LogError(ex);
                return BadRequest(ex.Message);
            }

        }

        [HttpDelete]
        [Route("api/VehicleMakeAPI/{Id}")]
        public async Task<IHttpActionResult> Delete(int Id)
        {
            try
            {
                VehicleMake model = await vehiclemakeService.GetByIdAsync(Id);
                if (model == null)
                {
                    return NotFound();

                }
                else
                {
                    caching.InvalidateCache(MasterCacheKeyArray);
                    await vehiclemakeService.DeleteAsync(model);
                    return Ok();
                }
            }
            catch (Exception ex)
            {
                logError.LogError(ex);
                return BadRequest(ex.Message);
            }
        }
    }
}
