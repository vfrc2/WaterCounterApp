
var app = angular.module("WaterCounterApp", ["ngRoute"])
    .config(["$routeProvider", "$locationProvider", function ($routeProvider, $locationProvider) {

        $routeProvider
            .when('/', {
                templateUrl: 'web/ListHomes',
                controller: "HomesListController"
            })

        //$locationProvide.html5mode(true);
    }]);
    