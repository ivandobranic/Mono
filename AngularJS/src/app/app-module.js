/// <reference path="../scripts/angular.js" />


var app = angular.module("vehicleModule", ["ui.router", 'angularUtils.directives.dirPagination','ngMessages'])
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
                    .state("Home.Filter", {
                        url: "/filter?search",
                        templateUrl: "vehiclemake-templates/vehiclemake-list.html",
                        controller: "vehiclemakeController",
                        controllerAs: "makeCtrl"

                    })
                     .state("Home.Sort", {
                         url: "/filter?",
                         templateUrl: "vehiclemake-templates/vehiclemake-list.html",
                         controller: "vehiclemakeController",
                         controllerAs: "makeCtrl",
                         params: {
                             sortOrder: null
                         }

                     })
                    
                  
                });
