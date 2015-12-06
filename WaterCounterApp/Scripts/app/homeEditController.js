var app = angular.module("WaterCounterApp")
    .controller("HomeEditController", ['$scope', '$location', 'ngDialog', 'WaterCounterService', '$routeParams',
        function ($scope, $location, ngDialog, wcs, routeParams) {

            $scope.home = {
                homeId: "-1",
                address: "no address",
                counters: [
                    {
                        waterCounterid: '-1',
                        serialNum: "dkfjkld",
                        readings: 100
                    }
                ]
            }

            var id = routeParams.id;

            $scope.isNew = routeParams.id === 'new';

            wcs.getHome(id).then(function (result) {
                $scope.home = result;
                //result.counters.forEach(prepareCounter)
            });

            $scope.newCounter = function () {
                var counter = {
                    isNew: true,
                    waterCounterid: '-1',
                    serialNum: "",
                    readings: 0
                };

                $scope.home.counters.push(counter);
            }

            $scope.cancelNewCounter=function(counter){
                var index = $scope.home.counters.indexOf(counter);
                if (index > -1)
                    $scope.home.counters.splice(index, 1);
            }

        }])

    .controller("EditCounterController", ['$scope', '$location', 'ngDialog', 'WaterCounterService', '$routeParams',
        function ($scope, $location, ngDialog, wcs, routeParams) {

            $scope.isNew = $scope.counter.isNew;
            $scope.isEditing = $scope.counter.isNew;

            $scope.prevCounter = $scope.counter;

            $scope.updateCounter = function (counter) {

                var res = null;

                if ($scope.isNew)
                    res = wcs.addCounter($scope.home.homeId, counter).then(function (result) {
                        $scope.counter = result;
                    });
                else
                    res = wcs.updateCounter($scope.home.homeId, counter);

                return res.then(function (result) {
                    $scope.isNew = false;
                    $scope.isEditing = false;
                });
            }

            $scope.cancelEditing = function (counter) {
                $scope.counter = $scope.prevCounter;
                $scope.isEditing = false;

                if ($scope.isNew)
                    $scope.$parent.cancelNewCounter(counter);               

            }

            $scope.deleteCounter = function (counter) {
                var dialog = ngDialog.openConfirm(
                {
                    template: "deleteCounterDialog",
                    data: {
                        counter: counter
                    }
                });

                dialog
                    .then(function () {
                        return wcs.removeCounter($scope.$parent.home.homeId, counter.waterCounterId);
                    })
                    .then(function () {
                        var index = $scope.$parent.home.counters.indexOf(counter);
                        if (index > -1)
                            $scope.$parent.home.counters.splice(index, 1);
                    });
            }


        }]);