using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using Project.Common.Caching;
using Project.Common.Logging;
using Project.Model;
using Project.Service.Common;

namespace MVC.Controllers
{

    public class VehicleMakeAPIController : ApiController
    {
        IVehicleMakeService vehiclemakeService;
        ICaching caching;
        IErrorLogger logError;
        private readonly string[] MasterCacheKeyArray = { "VehicleMakeCache" };
        public VehicleMakeAPIController(IVehicleMakeService _vehiclemakeService, 
            ICaching _caching, IErrorLogger _logError)
        {
            this.vehiclemakeService = _vehiclemakeService;
            this.caching = _caching;
            this.logError = _logError;
        }

        [HttpGet]
        [Route("api/VehicleMakeAPI/{pageNumber?}/{sortOrder?}/{search?}")]
        public async Task<IHttpActionResult> Get(int? pageNumber = null, string sortOrder = null, string search = null)
        {
            try
            {
                int totalRowCount = await vehiclemakeService.GetVehicleMakeCount(search);
                List<VehicleMake> pagedList = await vehiclemakeService.PagedList(sortOrder, search, pageNumber ?? 1, 3);       

                var newModel = new
                {
                    Model = pagedList,
                    TotalCount = totalRowCount
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
        [Route("api/VehicleMakeAPI/{id?}")]
        public async Task<IHttpActionResult> Get(int id)
        {
            try
            {
                string rawKey = "CacheById";
                VehicleMake vehicleMake = caching.GetCacheItem(rawKey, MasterCacheKeyArray) as VehicleMake;
                if (vehicleMake == null)
                {
                    vehicleMake = await vehiclemakeService.GetById(id);
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
        [Route("api/VehicleMakeAPI/{model?}")]
        public async Task<IHttpActionResult> Post([FromBody]VehicleMake model)
        {
            try
            {
                caching.InvalidateCache(MasterCacheKeyArray);
                var result = await vehiclemakeService.Create(model);
                var message = Created("entity created", result);
                return message;
            }
            catch (Exception ex)
            {
                logError.LogError(ex);
                return BadRequest(ex.Message);
            }

        }


        [HttpPut]
        [Route("api/VehicleMakeAPI/{model?}")]
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
                    await vehiclemakeService.Update(model);
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
        [Route("api/VehicleMakeAPI/{model?}")]
        public async Task<IHttpActionResult> Delete([FromBody] VehicleMake model)
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
                    await vehiclemakeService.Delete(model);
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
