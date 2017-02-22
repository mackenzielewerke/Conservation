(function () {
    'use strict';

    angular
        .module('Application')
        .controller('PossessionsController', PossessionsController);

    PossessionsController.$inject = ['$http'];

    function PossessionsController($http) {
        /* jshint validthis:true */
        var vm = this;

        vm.Possessions = [];

        activate();

        function activate() {
            var promise = $http.get('/api/possessions');
            promise.then(function (result) {
                vm.Possessions = result.data;
            });
        }

        vm.Add = function (possession) {
            var promise = $http.post('/api/possessions', possession);
            promise.then(function (result) {
                vm.Possessions.push(result.data);
            });
        };

        vm.Remove = function (possession) {
            var promise = $http.delete('/api/possessions/{id}'.replace('{id}', possession.id));
            promise.then(function (result) {
                var index = vm.Possessions.indexOf();
            });
        };
    }
})();
