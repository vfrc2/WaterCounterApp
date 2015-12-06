﻿var app = angular.module("WaterCounterApp")
    .service("WaterCounterService", ['$http', function ($http) {

        // get /api/homes
        this.getHomes = function () {
            var req = {
                method: "GET",
                url: "/api/homes",
                headers: {
                    'Accept': 'application/json'
                }
            }
            return _proceedResponse($http(req));
        }

        this.getHome = function (id) {

            var req = {
                method: "GET",
                url: "/api/homes/"+id,
                headers: {
                    'Accept': 'application/json'
                }
            }
            return _proceedResponse($http(req));

        }

        this.getLessHome = function () {

            var req = {
                method: "GET",
                url: "/api/homes/less",
                headers: {
                    'Accept': 'application/json'
                }
            }
            return _proceedResponse($http(req));

        }

        this.getMostHome = function () {

            var req = {
                method: "GET",
                url: "/api/homes/most",
                headers: {
                    'Accept': 'application/json'
                }
            }
            return _proceedResponse($http(req));

        }

        this.addCounter = function (homeId, data) {

            var newCounter = {
                serialNum: data.serialNum,
                readings: data.readings
            };

            var req = {
                method: "POST",
                url: "api/homes/" + homeId + "/counters",
                headers: {
                    "Content-Type":"application/json"
                },
                data: newCounter
            }

            return _proceedResponse($http(req));
        }

        this.updateCounter = function (homeId, counter) {

            var proceedCounter = {
                waterCounterId: counter.waterCounterId,
                serialNum: counter.serialNum,
                readings: counter.readings
            }

            var req = {
                method: "PUT",
                url: "api/homes/" + homeId + "/counters/"+counter.waterCounterId,
                headers: {
                    "Content-Type":"application/json"
                },
                data: proceedCounter
            }
            return _proceedResponse($http(req));
        }

        this.removeCounter = function (homeId, counterId) {
            var req = {
                method: "DELETE",
                url: "api/homes/" + homeId + "/counters/" + counterId,
            }
            return _proceedResponse($http(req));
        }


        function _proceedResponse(httpPromise) {
            return httpPromise.then(function (result) {
                return result.data;
            }, function (err) {
                throw new Error("Api call exception!");
            });
        }

    }]);