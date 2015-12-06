var app = angular.module("WaterCounterApp")
    .controller("HomeEditController", ['$scope', '$location', 'ngDialog', 'WaterCounterService','$routeParams',
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
                result.counters.forEach(prepareCounter)
            });

            $scope.updateCounter = function (counter) {

                var res = null;

                if (counter.isNew)
                    res = wcs.addCounter($scope.home.homeId, counter);
                else
                    res = wcs.updateCounter($scope.home.homeId, counter);

                res.then(function (result) {
                    prepareCounter(result);
                    counter = result;
                });
            }

            $scope.cancelEditing = function (counter) {
                counter.readings = counter.prevReadings;
                counter.serialNum = counter.prevSerialNum;
                counter.isEditing = false;

                if (counter.isNew) {
                    var index = $scope.home.counters.indexOf(counter);
                    if (index > -1)
                        $scope.home.counters.splice(index, 1);
                }

            }

            $scope.newCounter = function () {
                var counter = {
                    waterCounterid: '-1',
                    serialNum: "",
                    readings: 0
                };

                prepareCounter(counter);

                counter.isNew = true;
                counter.isEditing = true;

                $scope.home.counters.push(counter);
            }

            function prepareCounter(counter) {
                counter.isNew = false;
                counter.isEditing = false;
                counter.prevSerialNum = counter.serialNum;
                counter.prevReadings = counter.readings;
            }


    }]);