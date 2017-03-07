﻿

(function () {

    var application = angular.module('Application');

    var controller = application.controller('PerformersController', PerformersController);

    PerformersController.$inject = ['$http'];

    function PerformersController($http) {
        var vm = this;

        var count = 0;

        vm.Performer = [];
        vm.act = [];

        var promise = $http.get('api/acts/' + vm.act.id + '/performers');

        promise.then(function (result) {
            vm.Performer = result.data;
        }, function (result) {
            console.log(result);
        });

        vm.Add = function (actId, performer) {
            var copy = angular.copy(performer);
            performer.name = '';
            

            var promise = $http.post('api/acts/'+ actId + '/performers', copy);
            promise.then(function (result) {
                vm.Performer.push(result.data);
            }, function (result) {
            });
        };

        vm.Remove = function (performer) {

            var url = 'api/performer/{id}'.replace('{id}', performer.id);

            var promise = $http.delete(url);
            promise.then(function (result) {


                var index = vm.Performer.indexOf(performer);
                vm.Performer.splice(index, 1);
            });
        };
    }
})();