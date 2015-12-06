var app = angular.module("WaterCounterApp")
    .controller("HomesListController", ['$scope', '$location', 'ngDialog', 'WaterCounterService', 'toastr',
        function ($scope, $location, ngDialog, wcs, toastr) {

        $scope.homes = [{ homeId: "1", address: "blablabla" }];

        $scope.mostHome = {};
        $scope.lessHome = {};

        wcs.getHomes().then(function (result) {
            $scope.homes = result;
        }, toastError).catch(toastError);

        wcs.getMostHome().then(function (result) {
            $scope.mostHome = result;
            $scope.mostHome.readings = result.counters.reduce(function(prev, current){
                return (prev.readings > current.readings) ? prev : current;
            }).readings;
        }).catch(toastError);;

        wcs.getLessHome().then(function (result) {
            $scope.lessHome = result;
            $scope.lessHome.readings = result.counters.reduce(function(prev, current){
                return (prev.readings < current.readings) ? prev : current;
            }).readings;
        }).catch(toastError);;

        $scope.edit = function (home) {
            $location.path("/home/" + home.homeId);
        }
        $scope.delete = function (home) {
            var dialog = ngDialog.openConfirm(
                {
                    template: "deleteDialog",
                    data: {
                        home: home
                    }
                })

            dialog.then(function () {
                var index = $scope.homes.indexOf(home);
                if (index > -1)
                    $scope.homes.splice(index, 1);
                toastr.info("Home #" + home.homeId + " deleted!");
            }).catch(toastError);
        }

        $scope.addNew = function () {
            $location.path("/home/new");
        }

        function toastError(err) {
            toastr.error(err.message, "Error");
        }

    }]);