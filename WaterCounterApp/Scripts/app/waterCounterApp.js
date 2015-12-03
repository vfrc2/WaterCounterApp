
var app = angular.module("WaterCounterApp", ["ngRoute"])
    .config(["$roterProvider", "$locationProvide", function ($roterProvider, $locationProvide) {

        $roterProvider
            .when('/', {
                templateUrl: '/views/list.html',
                controller: "HomesListController"
            })

        //$locationProvide.html5mode(true);
    }]);
    