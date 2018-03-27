using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using AutoMapper;
using MVC.Models;
using Project.Common.Logging;
using Project.Model;
using Project.Repository;
using Project.Repository.Common;
using Project.Service.Common;

namespace MVC.Controllers
{
    public class VehicleModelAPIController : ApiController
    {
        IVehicleModelService vehiclemodelService;
        

        public VehicleModelAPIController(IVehicleModelService _vehiclemodelService)
        {
            this.vehiclemodelService = _vehiclemodelService;
            
        }
       
        [HttpGet]
        [Route("api/VehicleModelAPI")]
        public async Task<IHttpActionResult> Get(int? pageNumber = null, bool? isAscending = null, string search = null, int? pageSize = null)
        {
            IFilter filter = new Filter();
            try
            {

                filter.PageNumber = pageNumber ?? 1;
                filter.PageSize = 3;
                filter.Search = search;
                filter.IsAscending = isAscending ?? false;


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
            
                var vehicleModel = await vehiclemodelService.GetById(id);
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
                await vehiclemodelService.Create(model);
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
                    await vehiclemodelService.Update(model);
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
        public async Task<IHttpActionResult> Delete([FromBody] VehicleModel model)
        {
            try
            {
                if (model == null)
                {
                    return NotFound();

                }
                else
                {
                    await vehiclemodelService.Delete(model);
                    return Ok();
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
