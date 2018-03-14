/// <reference path="vehiclemake-controller.js" />
/// <reference path="../../scripts/angular.js" />
/// <reference path="../app-module.js" />



app.controller("vehicleModelController", function ($http, $stateParams, $state, $log, modelService) {
    var vm = this;

    //get
    vm.vehiclemodeldata = [];
    vm.pageNumber = 1;
    vm.totalcount = 0;
    vm.pageSize = 3;
    vm.sortBy = "";
    vm.filter = "";
    vm.sort = function () {
        vm.sorting = vm.sortBy == "" ? vm.sortBy = "name_desc" : vm.sortBy = "";
        $state.go("Home.VehicleModel");
        vm.getPaged(vm.pageNumber);
    }
    vm.search = function () {
        $state.go("Home.VehicleModel");
        vm.getPaged(vm.pageNumber);

    }
    vm.getPaged = function (newPageNumber) {
        var GetVehicleModel = modelService.getPagedList(newPageNumber, vm.sorting, vm.filter);
        GetVehicleModel.then(function (response) {
            vm.vehicleModelData = response.data.Model;
            vm.totalCount = response.data.TotalCount;
            $log.info(response);
        }, function (reason) {
            vm.error = reason.data;
            $log.info(reason);
        });

    };
    vm.getPaged(vm.pageNumber);






    //get by id

    var modelGetById = modelService.getById($stateParams.Id)
      .then(function (response) {
          vm.vehicleModelById = response.data;
          $log.info(response);
      }, function (reason) {
          vm.error = reason.data;
          $log.info(reason);
      });

    //post
    vm.vehicleModel = {
        Name: "",
        Abrv: "",
        MakeId:""
    }
    vm.createModel = function () {
        var result = modelService.create(vm.vehicleModel);
        result.then(function (response) {
            $state.go("Home.VehicleModel");
            $log.info(response);
        }, function (reason) {
            vm.error = reason.data;
            $log.info(reason);
        });
    };

    // put
    vm.updateModel = function () {
        var result = modelService.update(vm.vehicleModelById)
        result.then(function (response) {
            $state.go("Home.VehicleModel");
            $log.info(response);
        }, function (reason) {
            vm.error = reason.data;
            $log.info(reason);
        });
    };
    //delete
    vm.deleteModel = function () {
        var result = modelService.delete(vm.vehicleModelById)
        confirm("Do you want to delete record?")
        result.then(function (response) {
            $state.go("Home.VehicleModel");
            $log.info(response);
        }, function (reason) {
            vm.error = reason.data;
            $log.info(reason);
        });
    };

});
