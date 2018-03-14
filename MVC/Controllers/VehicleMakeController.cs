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
    public class VehicleMakeController : Controller
    {
        IVehicleMakeService vehiclemakeService;

        
        public VehicleMakeController(IVehicleMakeService _vehiclemakeService)
        {
            this.vehiclemakeService = _vehiclemakeService;
        }

        [HttpGet]
        public async Task<ActionResult> Index(string sortOrder, string search, int? pageNumber)
        {
            int rowCount = await vehiclemakeService.GetVehicleMakeCount(search);

            List<VehicleMakeViewModel> model = new List<VehicleMakeViewModel>();
            List<VehicleMake> pagedList = await vehiclemakeService.PagedList(sortOrder, search, pageNumber ?? 1, 5);
            ViewBag.sortOrder = string.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            model = Mapper.Map<List<VehicleMake>, List<VehicleMakeViewModel>>(pagedList);
            Mapper.AssertConfigurationIsValid();
            var paged = new StaticPagedList<VehicleMakeViewModel>(model, pageNumber ?? 1, 5, rowCount);
            return View(paged);

        }

        [HttpGet]
        public ActionResult Create()
        {

            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Create(VehicleMakeViewModel model)
        {
            if (ModelState.IsValid)
            {
                var vehicle = Mapper.Map<VehicleMakeViewModel, VehicleMake>(model);
                Mapper.AssertConfigurationIsValid();
                await vehiclemakeService.Create(vehicle);
                return RedirectToAction("Index");
            }
            return View(model);
        }

        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VehicleMakeViewModel model = new VehicleMakeViewModel();
            var vehicleMake = await vehiclemakeService.GetById(id);
            if (vehicleMake == null)
            {
                return HttpNotFound();
            }
            model = Mapper.Map<VehicleMake, VehicleMakeViewModel>(vehicleMake);
            Mapper.AssertConfigurationIsValid();

            return View(model);
        }

        [HttpGet]
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VehicleMakeViewModel model = new VehicleMakeViewModel();
            var vehicle = await vehiclemakeService.GetById(id);
            if (vehicle == null)
            {
                return HttpNotFound();
            }
            model = Mapper.Map<VehicleMake, VehicleMakeViewModel>(vehicle);
            Mapper.AssertConfigurationIsValid();
            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> Edit(VehicleMakeViewModel model)
        {
            if (ModelState.IsValid)
            {
                var vehicle = Mapper.Map<VehicleMakeViewModel, VehicleMake>(model);
                Mapper.AssertConfigurationIsValid();

                await vehiclemakeService.Update(vehicle);
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
            VehicleMakeViewModel model = new VehicleMakeViewModel();
            var vehicle = await vehiclemakeService.GetById(id);
            if (vehicle == null)
            {
                return HttpNotFound();
            }
            model = Mapper.Map<VehicleMake, VehicleMakeViewModel>(vehicle);
            Mapper.AssertConfigurationIsValid();
            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> Delete(VehicleMakeViewModel model)
        {
            var vehicle = Mapper.Map<VehicleMakeViewModel, VehicleMake>(model);
            Mapper.AssertConfigurationIsValid();
            if (vehicle != null)
            {
                await vehiclemakeService.Delete(vehicle);
                return RedirectToAction("Index");
            }
            return View(model);
        }
    }
}