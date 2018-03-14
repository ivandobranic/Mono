/// <reference path="../../../scripts/angular.js" />
/// <reference path="../../app-module.js" />

app.constant("vehicleModelWebApiUrl", "http://localhost:58986/api/VehicleModelAPI")
app.service("modelService", function ($http, $stateParams, vehicleModelWebApiUrl) {

    //get
    this.getPagedList = function (newPageNumber, sorting, filter) {
        return $http({
            url: vehicleModelWebApiUrl,
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
            url: vehicleModelWebApiUrl,
            params: { Id: $stateParams.Id },
            method: "get"
        });
    }
    //post
    this.create = function (Vehicle) {
        return $http({
            url: vehicleModelWebApiUrl,
            data: Vehicle,
            method: "post",
        });
    };
    //put
    this.update = function (Vehicle) {
        return $http({
            method: "put",
            url: vehicleModelWebApiUrl,
            data: Vehicle,
        });

    }

    //delete
    this.delete = function (Vehicle) {
        return $http({
            method: "delete",
            url: vehicleModelWebApiUrl,
            data: Vehicle,
            headers: {
                'Content-Type': 'application/json'
            }
        })

    }
});
