﻿

(function () {

    var application = angular.module('Application');

    var controller = application.controller('ActsController', ActsController);

    ActsController.$inject = ['$http'];

    function ActsController($http) {
        var vm = this;

        var count = 0;

        vm.Act = [];
        vm.circus = [];

        var promise = $http.get('/api/circuses/' + vm.circus.id + '/acts');

        promise.then(function (result) {
            vm.Act = result.data;
        }, function (result) {
            console.log(result)
        });

        vm.Add = function (circusId, act) {
            var copy = angular.copy(circusId, act);


            act.name = '';
            
            //var url = '/api/circus/' + circusId + '/acts';
            var promise = $http.post('/api/circuses/'+ circusId + '/acts', copy);
            promise.then(function (result) {
                vm.Act.push(result.data);
            }, function (result) {
            });
        };

        vm.Remove = function (act) {

            var url = '/api/act/{id}'.replace('{id}', act.id);

            var promise = $http.delete(url);
            promise.then(function (result) {


                var index = vm.Act.indexOf(act);
                vm.Act.splice(index, 1);
            });
        };
    }
})();