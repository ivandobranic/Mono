/// <reference path="../../../scripts/angular.js" />
/// <reference path="../../app-module.js" />


app.service("makeService", function ($http, $stateParams) {

    //get
    this.getpagedList = function (newPageNumber) {
        return $http({
            url: 'http://localhost:58986/api/VehicleMakeAPI',
            method: "get",
            params: {
                pageNumber: newPageNumber, sortOrder: $stateParams.sortOrder,
                search: $stateParams.search
            }
        });

    }

    //getById
    this.getById = function () {
        return $http({
            url: 'http://localhost:58986/api/VehicleMakeAPI',
            params: { Id: $stateParams.Id },
            method: "get"
        });
    }
    //post
    this.create = function (Vehicle) {
        return $http({
            url: 'http://localhost:58986/api/VehicleMakeAPI',
            data: Vehicle ,
            method: "POST",
        });
    };
    //put
    this.update = function (Vehicle) {
        return $http({
            method: "PUT",
            url: 'http://localhost:58986/api/VehicleMakeAPI',
            data: Vehicle,
        });

    }

    //delete
    this.delete = function (Vehicle) {
        return $http({
            method: "DELETE",
            url: 'http://localhost:58986/api/VehicleMakeAPI',
            data: Vehicle,
            headers: {
                'Content-Type': 'application/json'
            }
        })

    }
});
