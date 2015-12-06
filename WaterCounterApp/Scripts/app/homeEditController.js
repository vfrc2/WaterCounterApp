var app = angular.module("WaterCounterApp")
    .controller("HomeEditController", ['$scope', '$location', 'ngDialog', 'WaterCounterService', '$routeParams', 'toastr',
        function ($scope, $location, ngDialog, wcs, routeParams, toastr) {

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
            }).catch(toastError);

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

            $scope.saveHome = function (home) {
                var res = null;
                if ($scope.isNew)
                    res = wcs.addHome(home).then(function (res) {
                        $scope.home = res;
                        $location.path("/home/" + home.homeId);
                    });
                else
                    res = wcs.updateHome(home.homeId, home);

                res.then(function (res) {
                    toastr.info("Home #" + home.homeId + " svaed!");
                }).catch(toastError);
            }

            function toastError(err) {
                toastr.error(err.message, "Error");
            }

        }])

    .controller("EditCounterController", ['$scope', '$location', 'ngDialog', 'WaterCounterService', '$routeParams', 'toastr',
        function ($scope, $location, ngDialog, wcs, routeParams, toastr) {

            $scope.isNew = $scope.counter.isNew;
            $scope.isEditing = $scope.counter.isNew;

            $scope.prevCounter = {};
            $scope.prevCounter.serialNum = $scope.counter.serialNum;
            $scope.prevCounter.readings = $scope.counter.readings;

            $scope.updateCounter = function (counter) {

                var res = null;

                if ($scope.isNew)
                    res = wcs.addCounter($scope.home.homeId, counter).then(function (result) {
                        $scope.counter = result;
                    });
                else
                    res = wcs.updateCounter($scope.home.homeId, counter);

                return res.then(function () {
                    $scope.isNew = false;
                    $scope.isEditing = false;
                    toastr.info("Water counter #" + counter.serialNum + " saved!");
                    
                }).catch(toastError);
            }

            $scope.cancelEditing = function (counter) {
                $scope.counter.serialNum = $scope.prevCounter.serialNum;
                $scope.counter.readings = $scope.prevCounter.readings;
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
                    .catch(toastError)
                    .then(function () {
                        var index = $scope.$parent.home.counters.indexOf(counter);
                        if (index > -1)
                            $scope.$parent.home.counters.splice(index, 1);
                        toastr.info("Water counter #" + result.serialNum + " deleted!");
                    })
            }

            function toastError(err) {
                toastr.error(err.message, "Error");
            }

        }])
    .directive('readingsValidator', function () {
    return {

        // limit usage to argument only
        restrict: 'A',

        // require NgModelController, i.e. require a controller of ngModel directive
        require: 'ngModel',

        // create linking function and pass in our NgModelController as a 4th argument
        link: function (scope, element, attr, ctrl) {
            function validateIsIntAndGreaterWhenZero(n) {

                ctrl.$setValidity('readingsValidator', (n == "0" || ((n | 0) > 0 && n % 1 == 0)));

                return n;
            }

            ctrl.$parsers.push(validateIsIntAndGreaterWhenZero);
        }
    };
});;

