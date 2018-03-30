using System;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using Project.Model;
using Project.Repository.Common;
using Project.Service.Common;

namespace MVC.Controllers
{
    public class VehicleModelAPIController : ApiController
    {
        IVehicleModelService vehiclemodelService;
        IFilter filter;

        public VehicleModelAPIController(IVehicleModelService _vehiclemodelService, IFilter _filter)
        {
            this.vehiclemodelService = _vehiclemodelService;
            this.filter = _filter;
        }
       
        [HttpGet]
        [Route("api/VehicleModelAPI")]
        public async Task<IHttpActionResult> Get(int? pageNumber = null, bool isAscending = false, string search = null, int? pageSize = null)
        {
           
            try
            {

                filter.PageNumber = pageNumber ?? 1;
                filter.Search = search;
                filter.IsAscending = isAscending;


                var pagedList = await vehiclemodelService.PagedList(filter);
                return Ok(pagedList);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
        
        [HttpGet]
        [Route("api/VehicleModelAPI")]
        public async Task<IHttpActionResult> Get(int id)
        {
            try
            {
            
                var vehicleModel = await vehiclemodelService.GetByIdAsync(id);
                if (vehicleModel == null)
                {
                    return NotFound();
                }
       
                return Ok(vehicleModel);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
        [HttpPost]
        [Route("api/VehicleModelAPI", Name = "VehicleModelRoute")]
        public async Task<IHttpActionResult> Post([FromBody] VehicleModel model)
        {
            try
            {
                await vehiclemodelService.CreateAsync(model);
                return CreatedAtRoute("VehicleModelRoute", new { Id = model.Id }, model);
                

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpPut]
        [Route("api/VehicleModelAPI")]
        public async Task<IHttpActionResult> Put([FromBody] VehicleModel model)
        {
            try
            {
          
                if (model== null)
                {
                    return NotFound();

                }
                else
                {
                    await vehiclemodelService.UpdateAsync(model);
                    return Content(HttpStatusCode.Accepted, model);
                }

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpDelete]
        [Route("api/VehicleModelAPI")]
        public async Task<IHttpActionResult> Delete(int id)
        {
            try
            {
                
              await vehiclemodelService.DeleteAsync(id);
              return Ok();
                
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
