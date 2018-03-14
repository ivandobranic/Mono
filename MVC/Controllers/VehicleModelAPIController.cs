﻿using System;
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
using Project.Service.Common;

namespace MVC.Controllers
{
    public class VehicleModelAPIController : ApiController
    {
        IVehicleModelService vehiclemodelService;
        IErrorLogger errorlogger;

        public VehicleModelAPIController(IVehicleModelService _vehiclemodelService, IErrorLogger _errorlogger)
        {
            this.vehiclemodelService = _vehiclemodelService;
            this.errorlogger = _errorlogger;
        }
       
        [HttpGet]
        [Route("api/VehicleModelAPI/{pageNumber?}/{sortOrder?}/{search?}")]
        public async Task<IHttpActionResult> Get(int? pageNumber = null, string sortOrder = null, string search = null, int? pageSize = null)
        {
            try
            {
                int totalRowCount = await vehiclemodelService.GetVehicleModelCount(search);
                List<VehicleModel> pagedList = await vehiclemodelService.PagedList(sortOrder, search, pageNumber ?? 1, pageSize ?? 3);
                var newModel = new
                {
                    Model = pagedList,
                    TotalCount = totalRowCount
                };
                return Ok(newModel);
            }
            catch (Exception ex)
            {
                errorlogger.LogError(ex);
                return BadRequest(ex.Message);
            }

        }
        
        [HttpGet]
        [Route("api/VehicleModelAPI/{id?}")]
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
        [Route("api/VehicleModelAPI/{model?}")]
        public async Task<IHttpActionResult> Post([FromBody] VehicleModel model)
        {
            try
            {
                var result = await vehiclemodelService.Create(model);
                var message = Created("entity created", result);
                return message;

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpPut]
        [Route("api/VehicleModelAPI/{model?}")]
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
        [Route("api/VehicleModelAPI/{model?}")]
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
