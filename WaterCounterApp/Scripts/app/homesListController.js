var app = angular.module("WaterCounterApp")
    .controller("HomesListController", ['$scope', '$location', 'ngDialog', 'WaterCounterService', function ($scope, $location, ngDialog, wcs) {

        $scope.homes = [{ homeId: "1", address: "blablabla" }];

        wcs.getHomes().then(function (result) {
            $scope.homes = result;
        });

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
            })
        }

        $scope.addNew = function () {
            $location.path("/home/new");
        }

    }]);