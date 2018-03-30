using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;
using AutoMapper;
using MVC.Models;
using PagedList;
using Project.DAL.Entities;
using Project.Model.Common;
using Project.Repository.Common;
using Project.Service.Common;

namespace MVC.Controllers
{
    public class VehicleModelController : Controller
    {
        IVehicleModelService vehicleModelService;
        IRepository<VehicleMakeEntity> makeRepository;
        IRepository<VehicleModelEntity> modelRepository;
        IFilter filter;
        public VehicleModelController(IVehicleModelService _vehicleModelService, 
            IRepository<VehicleMakeEntity> _makeRepository, IRepository<VehicleModelEntity> _modelRepository,
            IFilter _filter)
        {
            this.vehicleModelService = _vehicleModelService;
            this.makeRepository = _makeRepository;
            this.modelRepository = _modelRepository;
            this.filter = _filter;
        }
        [HttpGet]
        public async Task<ActionResult> Index(string search, int? pageNumber, bool isAscending = false)
        {
            filter.Search = search;
            filter.IsAscending = isAscending;
            filter.PageNumber = pageNumber ?? 1;

            List<VehicleModelViewModel> model = new List<VehicleModelViewModel>();
            var pagedList = await vehicleModelService.PagedList(filter);
            var newPagedList = pagedList.ToList();
            ViewBag.sortOrder = isAscending ? false : true;
            model = Mapper.Map<List<IVehicleModel>, List<VehicleModelViewModel>>(newPagedList);
            Mapper.AssertConfigurationIsValid();
            var paged = new StaticPagedList<VehicleModelViewModel>(model, pageNumber ?? 1, filter.PageSize, filter.TotalCount);
            return View(paged);

        }

        [HttpGet]
        public ActionResult Create()
        {

            var vehicleMake = makeRepository.Get();
            ViewBag.MakeId = new SelectList(vehicleMake, "Id", "Name");

            return View();
        }
        [HttpGet]
        public ActionResult Model(int? makeId)
        {
            if (makeId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            List<VehicleModelViewModel> model = new List<VehicleModelViewModel>();
            var vehicleModel = modelRepository.Get().Where(x => x.MakeId == makeId).ToList();
            if (vehicleModel == null)
            {
                return HttpNotFound();
            }
            model = Mapper.Map<List<VehicleModelEntity>, List<VehicleModelViewModel>>(vehicleModel);
            Mapper.AssertConfigurationIsValid();

            return View(model);
        }

        public async Task<ActionResult> Details(int id)
        {
           
            VehicleModelViewModel model = new VehicleModelViewModel();
            var vehicleModel = await vehicleModelService.GetByIdAsync(id);
            if (vehicleModel == null)
            {
                return HttpNotFound();
            }
            model = Mapper.Map<IVehicleModel, VehicleModelViewModel>(vehicleModel);
            Mapper.AssertConfigurationIsValid();

            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> Create(VehicleModelViewModel model)
        {
            if (ModelState.IsValid)
            {
                var vehicle = Mapper.Map<VehicleModelViewModel, IVehicleModel>(model);
                Mapper.AssertConfigurationIsValid();

                await vehicleModelService.CreateAsync(vehicle);
                return RedirectToAction("Index");
            }

            return View(model);
        }

        [HttpGet]
        public async Task<ActionResult> Edit(int id)
        {
          
            VehicleModelViewModel model = new VehicleModelViewModel();
            var vehicle = await vehicleModelService.GetByIdAsync(id);
            if (vehicle == null)
            {
                return HttpNotFound();
            }
            model = Mapper.Map<IVehicleModel, VehicleModelViewModel>(vehicle);
            Mapper.AssertConfigurationIsValid();
            var vehicleMake = makeRepository.Get().ToList();
            ViewBag.MakeId = new SelectList(vehicleMake, "Id", "Name", model.MakeId);
            return View(model);
        }


        [HttpPost]
        public async Task<ActionResult> Edit(VehicleModelViewModel model)
        {
            if (ModelState.IsValid)
            {
                var vehicle = Mapper.Map<VehicleModelViewModel, IVehicleModel>(model);
                Mapper.AssertConfigurationIsValid();

                await vehicleModelService.UpdateAsync(vehicle);
                return RedirectToAction("Index");
            }
            return View(model);
        }

        [HttpGet]
        public async Task<ActionResult> Delete(int id)
        {
            VehicleModelViewModel model = new VehicleModelViewModel();
            var vehicle = await vehicleModelService.GetByIdAsync(id);
            if (vehicle == null)
            {
                return HttpNotFound();
            }
            model = Mapper.Map<IVehicleModel, VehicleModelViewModel>(vehicle);
            Mapper.AssertConfigurationIsValid();
            return View(model);
        }
        
        [HttpPost]
        [ActionName("Delete")]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
         
               await vehicleModelService.DeleteAsync(id);
               return RedirectToAction("Index");
        
           
        }

    }
}