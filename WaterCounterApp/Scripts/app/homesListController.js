var app = angular.module("WaterCounterApp")
    .controller("HomesListController", ['$scope', '$location', 'ngDialog', function ($scope, $location, ngDialog) {
        $scope.Homes = [
            { id: 1, address: "dfsldkf;lk", isHideDelete: true },
            { id: 2, address: "dfsldkf;lk" },
            { id: 3, address: "dfsldkf;lk" }
        ];

        $scope.edit = function (home) {
            $location.path("/home/" + home.id);
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
                var index = $scope.Homes.indexOf(home);
                if (index > -1)
                    $scope.Homes.splice(index, 1);
            })
        }

        $scope.addNew = function () {
            $location.path("/home/new");
        }

    }]);