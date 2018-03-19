/// <reference path="vehiclemake-controller.js" />
/// <reference path="../../scripts/angular.js" />
/// <reference path="../app-module.js" />



app.controller("vehiclemakeController", function ($http, $stateParams, $state, $log, makeService) {
    var vm = this;

    //get
    vm.vehiclemakedata = [];
    vm.pageNumber = 1;
    vm.totalcount = 0;
    vm.pageSize = 3;
    vm.sortBy = "";
    vm.filter = "";
    vm.sort = function () {
        vm.sorting = vm.sortBy == "" ? vm.sortBy = "name_desc" : vm.sortBy = "";
        $state.go("Home.Vehiclemake");
        vm.getPaged(vm.pageNumber);  
    }
    vm.search = function () {
        $state.go("Home.Vehiclemake");
        vm.getPaged(vm.pageNumber);
        
    }
    vm.getPaged = function (newPageNumber) {
        var GetVehicleMake = makeService.getPagedList(newPageNumber,vm.sorting, vm.filter);
        GetVehicleMake.then(function (response) {
            vm.vehicleMakeData = response.data.Model;
            vm.totalCount = response.data.TotalCount;
            $log.info(response);
        }, function (reason) {
            vm.error = reason.data;
            $log.info(reason);
        });
        
    };
    vm.getPaged(vm.pageNumber);
     
  

    //get by id

    var makeGetById = makeService.getById($stateParams.Id)
      .then(function (response) {
          vm.vehicleMakeById = response.data;
          $log.info(response);
      }, function (reason) {
          vm.error = reason.data;
          $log.info(reason);
      });

    //post
    vm.vehicleMake = {
        Name: "",
        Abrv: ""
    }
    vm.createMake = function () {
        var result = makeService.create(vm.vehicleMake);
        result.then(function (response) {
            $state.go("Home.Vehiclemake");
            $log.info(response);
        }, function (reason) {
            vm.error = reason.data;
            $log.info(reason);
        });
    };

    // put
    vm.updateMake = function () {
        var result = makeService.update(vm.vehicleMakeById)
        result.then(function (response) {
            $state.go("Home.Vehiclemake");
            $log.info(response);
        }, function (reason) {
            vm.error = reason.data;
            $log.info(reason);
        });
    };
    //delete
    vm.deleteMake = function () {
        var result = makeService.delete(vm.vehicleMakeById.Id)
        confirm("Do you want to delete record?")
        result.then(function (response) {
            $state.go("Home.Vehiclemake");
            $log.info(response);
        }, function (reason) {
            vm.error = reason.data;
            $log.info(reason);
        });
    };

});
