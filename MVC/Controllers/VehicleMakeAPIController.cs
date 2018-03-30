using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using Project.Common.Caching;
using Project.Common.Logging;
using Project.Model;
using Project.Model.Common;
using Project.Repository.Common;
using Project.Service.Common;

namespace MVC.Controllers
{

    public class VehicleMakeAPIController : ApiController
    {
        IVehicleMakeService VehicleMakeService;
        ICaching Caching;
        IErrorLogger LogError;
        IFilter Filter;
        private readonly string[] MasterCacheKeyArray = { "VehicleMakeCache" };
        public VehicleMakeAPIController(IVehicleMakeService vehicleMakeService, 
            ICaching caching, IFilter filter)
        {
            this.VehicleMakeService = vehicleMakeService;
            this.Caching = caching;
            this.LogError = ErrorLogger.GetInstance;
            this.Filter = filter;
        }

        [HttpGet]
        [Route("api/VehicleMakeAPI")]
        public async Task<IHttpActionResult> Get(int? pageNumber = null, bool isAscending = false, string search = null)
        {
            try
            {

                Filter.PageNumber = pageNumber ?? 1;
                Filter.Search = search;
                Filter.IsAscending = isAscending;

                var pagedList = await VehicleMakeService.PagedList(Filter);

                var newModel = new
                {
                    Model = pagedList.ToList(),
                    PageNumber = pagedList.PageNumber,
                    PageSize = pagedList.PageSize,
                    TotalCount = pagedList.TotalItemCount,
                    isAscending = Filter.IsAscending
                };

                return Ok(newModel);
            }
            catch(Exception ex)
            {
                LogError.LogError(ex);
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
                IVehicleMake vehicleMake = Caching.GetCacheItem(rawKey, MasterCacheKeyArray) as IVehicleMake;
                if (vehicleMake == null)
                {
                    vehicleMake = await VehicleMakeService.GetByIdAsync(id);
                    if (vehicleMake == null)
                    {
                        return NotFound();
                    }
                 Caching.AddCacheItem(rawKey, vehicleMake, MasterCacheKeyArray);
                }
                return Ok(vehicleMake);
            }
            catch (Exception ex)
            {
                LogError.LogError(ex);
                return BadRequest(ex.Message);
            }

        }

        [HttpPost]
        [Route("api/VehicleMakeAPI", Name = "VehicleMakeRoute")]
        public async Task<IHttpActionResult> Post([FromBody]VehicleMake model)
        {
            try
            {
                Caching.InvalidateCache(MasterCacheKeyArray);
                await VehicleMakeService.CreateAsync(model);
                return CreatedAtRoute("VehicleMakeRoute", new { Id = model.Id }, model);
         
            }
            catch (Exception ex)
            {
                LogError.LogError(ex);
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
                    Caching.InvalidateCache(MasterCacheKeyArray);
                    await VehicleMakeService.UpdateAsync(model);
                    return Content(HttpStatusCode.Accepted, model);
                }

            }
            catch (Exception ex)
            {
                LogError.LogError(ex);
                return BadRequest(ex.Message);
            }

        }

        [HttpDelete]
        [Route("api/VehicleMakeAPI/{Id}")]
        public async Task<IHttpActionResult> Delete(int id)
        {
            try
            {
            
              Caching.InvalidateCache(MasterCacheKeyArray);
              await VehicleMakeService.DeleteAsync(id);
              return Ok();
                
            }
            catch (Exception ex)
            {
                LogError.LogError(ex);
                return BadRequest(ex.Message);
            }
        }
    }
}
