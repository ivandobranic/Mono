/// <reference path="../scripts/angular.js" />


var app = angular.module("vehicleModule", ["ui.router", 'angularUtils.directives.dirPagination', 'ngMessages'])
              
                .config(function ($stateProvider, $urlRouterProvider, $urlMatcherFactoryProvider) {
                    $urlRouterProvider.otherwise("");
                    $urlMatcherFactoryProvider.caseInsensitive(true);
                    $stateProvider
                      .state("Home", {
                            url: "",
                            templateUrl: "Home.html",
                            controller: "vehiclemakeController",
                            controllerAs: "makeCtrl"
                        })
                     .state("Home.Vehiclemake", {
                         url: "/Vehiclemake?pageNumber&sortOrder&search",
                         templateUrl: "vehiclemake-templates/vehiclemake-list.html",
                         controller: "vehiclemakeController",
                         controllerAs: "makeCtrl"

                     })
                     .state("Home.Details", {
                         url: "/Details/:Id",
                         templateUrl: "vehiclemake-templates/details.html",
                         controller: "vehiclemakeController",
                         controllerAs: "makeCtrl"

                     })
                    .state("Home.CreateNew", {
                        url: "/Create",
                        templateUrl: "vehiclemake-templates/create.html",
                        controller: "vehiclemakeController",
                        controllerAs: "makeCtrl"
                    })
                     .state("Home.Edit", {
                         url: "/Edit/:Id",
                         templateUrl: "vehiclemake-templates/edit.html",
                         controller: "vehiclemakeController",
                         controllerAs: "makeCtrl"

                     })
                    .state("Home.Delete", {
                        url: "/Delete/:Id",
                        templateUrl: "vehiclemake-templates/delete.html",
                        controller: "vehiclemakeController",
                        controllerAs: "makeCtrl"

                    })
                     .state("Home.VehicleModel", {
                         url: "/VehicleModel?pageNumber&sortOrder&search",
                         templateUrl: "vehiclemodel-templates/vehiclemodel-list.html",
                         controller: "vehicleModelController",
                         controllerAs: "modelCtrl"

                     })
                     .state("Home.DetailsModel", {
                         url: "/Details/:Id",
                         templateUrl: "vehiclemodel-templates/details.html",
                         controller: "vehicleModelController",
                         controllerAs: "modelCtrl"

                     })
                    .state("Home.CreateNewModel", {
                        url: "/Create",
                        templateUrl: "vehiclemodel-templates/create.html",
                        controller: "vehicleModelController",
                        controllerAs: "modelCtrl"
                    })
                     .state("Home.EditModel", {
                         url: "/Edit/:Id",
                         templateUrl: "vehiclemodel-templates/edit.html",
                         controller: "vehicleModelController",
                         controllerAs: "modelCtrl"

                     })
                    .state("Home.DeleteModel", {
                        url: "/Delete/:Id",
                        templateUrl: "vehiclemodel-templates/delete.html",
                        controller: "vehicleModelController",
                        controllerAs: "modelCtrl"

                    })
                    
                  
                });
