/// <reference path="../../scripts/angular.js" />
/// <reference path="../app-module.js" />


app.controller("vehiclemakeController", function ($http, $stateParams, $log, makeService) {
    var vm = this;

    //get
    vm.vehiclemakedata = [];
    vm.pageNumber = 1;
    vm.totalcount = 0;
    vm.pageSize = 3;
    vm.sort = "name_desc";

    vm.GetPaged = function (newPageNumber) {
      var GetVehicleMake =  makeService.getpagedList(newPageNumber);
       GetVehicleMake.then(function (response) {
            vm.vehiclemakedata = response.data.Model;
            vm.totalcount = response.data.TotalCount;
       }, function (reason) {
           $log.info(reason);
       });
    }
    vm.GetPaged(vm.pageNumber);
 
   

     //get by id
    var MakeGetById = makeService.getById($stateParams.Id)
      .then(function (response) {
          vm.vehiclemakebyid = response.data;
      }, function (reason) {
          $log.info(reason);
      })
  
    //post
    vm.VehicleMake = {
        Name: "",
        Abrv: ""
    };
    vm.createMake = function () {
       return makeService.create(vm.VehicleMake)
    };
    // put
    vm.updateMake = function () {
        return makeService.update(vm.vehiclemakebyid);
    };
    //delete
    vm.deleteMake = function () {
        return makeService.delete(vm.vehiclemakebyid);
    }

});
