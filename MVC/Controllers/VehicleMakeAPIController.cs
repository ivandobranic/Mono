using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using AutoMapper;
using MVC.Models;
using Project.Model;
using Project.Service.Common;

namespace MVC.Controllers
{
  
    public class VehicleMakeAPIController : ApiController
    {
        IVehicleMakeService vehiclemakeService;

        public VehicleMakeAPIController(IVehicleMakeService _vehiclemakeService)
        {
            this.vehiclemakeService = _vehiclemakeService;
        }
        // GET: api/VehicleMakeAPI
        [HttpGet]
     
        public async Task<IHttpActionResult> Get(int? pageNumber = null, string sortOrder = null, string search = null)
        {
            int totalRowCount = vehiclemakeService.GetVehicleMakeCount(search);

            List<VehicleMake> pagedList = await vehiclemakeService.PagedList(sortOrder, search, pageNumber ?? 1, 3);
     

            var newModel = new
            {
                Model = pagedList,
                TotalCount = totalRowCount
            };
            return Ok(newModel);
            

        }

        // GET: api/VehicleMakeAPI/5
        [HttpGet]
        public async Task<IHttpActionResult> Get(int id)
        {
            try
            {

                var vehicleMake = await vehiclemakeService.GetById(id);
                if (vehicleMake == null)
                {
                    return NotFound();
                }

                return Ok(vehicleMake);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpPost]
        public async Task<IHttpActionResult> Post([FromUri] VehicleMake model)
        {
            try
            {

               
                var result = await vehiclemakeService.Create(model);
                var message = Created("entity created", result);
                return message;
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        // PUT: api/VehicleMakeAPI/5
        
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
                    await vehiclemakeService.Update(model);
                    return Content(HttpStatusCode.Accepted, model);
                }

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }


        public async Task<IHttpActionResult> Delete([FromBody]VehicleMake model)
        {
            try
            {
                if (model == null)
                {
                    return NotFound();

                }
                else
                {
                    await vehiclemakeService.Delete(model);
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
