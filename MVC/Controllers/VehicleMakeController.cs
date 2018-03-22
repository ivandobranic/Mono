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
using Project.Repository.Common;
using Project.Service.Common;

namespace MVC.Controllers
{
    public class VehicleMakeController : Controller
    {
        IVehicleMakeService vehiclemakeService;
        IFilter filter;
        
        public VehicleMakeController(IVehicleMakeService _vehiclemakeService, IFilter _filter)
        {
            this.vehiclemakeService = _vehiclemakeService;
            this.filter = _filter;
        }

        [HttpGet]
        public async Task<ActionResult> Index(string search, int? pageNumber, bool isAscending = false)
        {
            filter.Search = search;
            filter.IsAscending = isAscending;
            filter.PageNumber = pageNumber ?? 1;
            filter.PageSize = 3;

            List<VehicleMakeViewModel> model = new List<VehicleMakeViewModel>();
            var pagedList = await vehiclemakeService.PagedList(filter);
            var newPagedList = pagedList.ToList();
            ViewBag.sortOrder = isAscending ? false : true;
            model = Mapper.Map<List<VehicleMake>, List<VehicleMakeViewModel>>(newPagedList);
            Mapper.AssertConfigurationIsValid();
            var paged = new StaticPagedList<VehicleMakeViewModel>(model, pageNumber ?? 1, 3, filter.TotalCount);
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
                await vehiclemakeService.CreateAsync(vehicle);
                return RedirectToAction("Index");
            }
            return View(model);
        }

        public async Task<ActionResult> Details(int id)
        {
          
            VehicleMakeViewModel model = new VehicleMakeViewModel();
            var vehicleMake = await vehiclemakeService.GetByIdAsync(id);
            if (vehicleMake == null)
            {
                return HttpNotFound();
            }
            model = Mapper.Map<VehicleMake, VehicleMakeViewModel>(vehicleMake);
            Mapper.AssertConfigurationIsValid();

            return View(model);
        }

        [HttpGet]
        public async Task<ActionResult> Edit(int id)
        {
      
            VehicleMakeViewModel model = new VehicleMakeViewModel();
            var vehicle = await vehiclemakeService.GetByIdAsync(id);
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

                await vehiclemakeService.UpdateAsync(vehicle);
                return RedirectToAction("Index");
            }
            return View(model);
        }
        [HttpGet]
        public async Task<ActionResult> Delete(int id)
        {
  
            VehicleMakeViewModel model = new VehicleMakeViewModel();
            var vehicle = await vehiclemakeService.GetByIdAsync(id);
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
                await vehiclemakeService.DeleteAsync(vehicle);
                return RedirectToAction("Index");
            }
            return View(model);
        }
    }
}