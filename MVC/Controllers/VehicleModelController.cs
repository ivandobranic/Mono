using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using MVC.Models;
using PagedList;
using Project.Common.Caching;
using Project.Model;
using Project.Service.Common;

namespace MVC.Controllers
{
    public class VehicleModelController : Controller
    {
        IVehicleModelService vehicleModelService;
        IVehicleMakeService vehiclemakeService;
        public VehicleModelController(IVehicleModelService _vehicleModelService, IVehicleMakeService _vehiclemakeService)
        {
            this.vehicleModelService = _vehicleModelService;
            this.vehiclemakeService = _vehiclemakeService;
        }
        [HttpGet]
        public async Task<ActionResult> Index(string sortOrder, string search, int? pageNumber)
        {
            int rowCount = vehicleModelService.GetVehicleModelCount(search);

            List<VehicleModelViewModel> model = new List<VehicleModelViewModel>();
            List<VehicleModel> pagedList = await vehicleModelService.PagedList(sortOrder, search, pageNumber ?? 1, 5);

            ViewBag.sortOrder = string.IsNullOrEmpty(sortOrder) ? "name_desc" : "";

            model = Mapper.Map<List<VehicleModel>, List<VehicleModelViewModel>>(pagedList);
            Mapper.AssertConfigurationIsValid();
            var paged = new StaticPagedList<VehicleModelViewModel>(model, pageNumber ?? 1, 5, rowCount);
            return View(paged);

        }


        [HttpGet]
        public ActionResult Create()
        {

            var vehicleMake = vehiclemakeService.GetAll();
            ViewBag.MakeId = new SelectList(vehicleMake, "Id", "Name");

            return View();
        }
        [HttpGet]
        public ActionResult Model(int? MakeId)
        {
            if (MakeId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            List<VehicleModelViewModel> model = new List<VehicleModelViewModel>();
            var vehicleModel = vehicleModelService.GetAll().Where(x => x.MakeId == MakeId).ToList();
            if (vehicleModel == null)
            {
                return HttpNotFound();
            }
            model = Mapper.Map<List<VehicleModel>, List<VehicleModelViewModel>>(vehicleModel);
            Mapper.AssertConfigurationIsValid();

            return View(model);
        }

        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VehicleModelViewModel model = new VehicleModelViewModel();
            var vehicleModel = await vehicleModelService.GetById(id);
            if (vehicleModel == null)
            {
                return HttpNotFound();
            }
            model = Mapper.Map<VehicleModel, VehicleModelViewModel>(vehicleModel);
            Mapper.AssertConfigurationIsValid();

            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> Create(VehicleModelViewModel model)
        {
            if (ModelState.IsValid)
            {
                var vehicle = Mapper.Map<VehicleModelViewModel, VehicleModel>(model);
                Mapper.AssertConfigurationIsValid();

                await vehicleModelService.Create(vehicle);
                return RedirectToAction("Index");
            }

            return View(model);
        }

        [HttpGet]
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VehicleModelViewModel model = new VehicleModelViewModel();
            var vehicle = await vehicleModelService.GetById(id);
            if (vehicle == null)
            {
                return HttpNotFound();
            }
            model = Mapper.Map<VehicleModel, VehicleModelViewModel>(vehicle);
            Mapper.AssertConfigurationIsValid();
            var vehicleMake = vehiclemakeService.GetAll().ToList();
            ViewBag.MakeId = new SelectList(vehicleMake, "Id", "Name", model.MakeId);
            return View(model);
        }


        [HttpPost]
        public async Task<ActionResult> Edit(VehicleModelViewModel model)
        {
            if (ModelState.IsValid)
            {
                var vehicle = Mapper.Map<VehicleModelViewModel, VehicleModel>(model);
                Mapper.AssertConfigurationIsValid();

                await vehicleModelService.Update(vehicle);
                return RedirectToAction("Index");
            }
            return View(model);
        }

        [HttpGet]
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VehicleModelViewModel model = new VehicleModelViewModel();
            var vehicle = await vehicleModelService.GetById(id);
            if (vehicle == null)
            {
                return HttpNotFound();
            }
            model = Mapper.Map<VehicleModel, VehicleModelViewModel>(vehicle);
            Mapper.AssertConfigurationIsValid();
            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> Delete(VehicleModelViewModel model)
        {
            var vehicle = Mapper.Map<VehicleModelViewModel, VehicleModel>(model);
            Mapper.AssertConfigurationIsValid();
            if (vehicle != null)
            {
                await vehicleModelService.Delete(vehicle);
                return RedirectToAction("Index");
            }
            return View(model);
        }

    }
}