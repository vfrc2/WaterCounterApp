var app = angular.module("WaterCounterApp")
    .controller("HomeEditController", ['$scope', '$location', 'ngDialog', 'WaterCounterService','$routeParams',
        function ($scope, $location, ngDialog, wcs, routeParams) {

            $scope.home = {}
            var id = routeParams.id;

            wcs.getHome(id).then(function (result) {
                $scope.home = result;
            })


        wcs.getHome()


    }]);