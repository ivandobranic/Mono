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
    
    vm.GetPaged = function (newPageNumber) {
        var GetVehicleMake = makeService.getpagedList(newPageNumber);
        GetVehicleMake.then(function (response) {
            vm.vehiclemakedata = response.data.Model;
            vm.totalcount = response.data.TotalCount;
            $log.info(response);
        }, function (reason) {
            vm.error = reason.data;
            $log.info(reason);
        });
    };
    vm.GetPaged(vm.pageNumber);
   
    vm.descending = "";
    vm.sort = function () {
       vm.sorting = vm.descending == "" ? vm.descending = "name_desc" : vm.descending = "";
       $state.go("Home.Sort", { sortOrder: vm.sorting });
        
    }
   
  

    //get by id

    var MakeGetById = makeService.getById($stateParams.Id)
      .then(function (response) {
          vm.vehiclemakebyid = response.data;
          $log.info(response);
      }, function (reason) {
          vm.error = reason.data;
          $log.info(reason);
      });

    //post
    vm.VehicleMake = {
        Name: "",
        Abrv: ""
    }
    vm.createMake = function () {
        var result = makeService.create(vm.VehicleMake);
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
        var result = makeService.update(vm.vehiclemakebyid)
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
        var result = makeService.delete(vm.vehiclemakebyid)
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
