/// <reference path="../../../scripts/angular.js" />
/// <reference path="../../app-module.js" />


app.constant("vehicleMakeWebApiUrl", "http://localhost:58986/api/VehicleMakeAPI")
app.service("makeService", function ($http, $stateParams, vehicleMakeWebApiUrl) {
   
    //get
    this.getPagedList = function (newPageNumber, sorting, filter) {
        return $http({
            url: vehicleMakeWebApiUrl,
            method: "get",
            params: {
                pageNumber: newPageNumber, sortOrder: sorting,
                search: filter
            }
        });

    }

    //getById
    this.getById = function () {
        return $http({
            url: vehicleMakeWebApiUrl,
            params: { Id: $stateParams.Id },
            method: "get"
        });
    }
    //post
    this.create = function (Vehicle) {
        return $http({
            url: vehicleMakeWebApiUrl,
            data: Vehicle ,
            method: "post",
        });
    };
    //put
    this.update = function (Vehicle) {
        return $http({
            method: "put",
            url: vehicleMakeWebApiUrl,
            data: Vehicle,
        });

    }

    //delete
    this.delete = function (Id) {
        return $http({
            method: "delete",
            url: vehicleMakeWebApiUrl+"/"+Id
        })

    }
});
