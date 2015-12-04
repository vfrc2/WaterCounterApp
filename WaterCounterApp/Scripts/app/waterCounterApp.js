
var app = angular.module("WaterCounterApp", ["ngRoute","ngDialog"])
    .config(["$routeProvider", "$locationProvider", function ($routeProvider, $locationProvider) {

        $routeProvider
            .when('/', {
                templateUrl: 'web/ListHomes',
                controller: "HomesListController"
            })
            .when('/home/:id', {
                templateUrl: 'web/EditHome',
                controller: "HomeEditController"
            })
            .when('/home/new', {
                templateUrl: 'web/EditHome',
                controller: "HomeEditController"
            })

        //$locationProvide.html5mode(true);
    }]);
    