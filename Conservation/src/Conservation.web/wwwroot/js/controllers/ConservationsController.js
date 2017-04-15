(function () {
    var application = angular.module('Application');

    var controller = application.controller('ConservationsController', ConservationsController);

    ConservationsController.$inject = ['$http'];

    function ConservationsController($http) {
        var vm = this;

        var count = 0;

        vm.Conservation = [];

        var promise = $http.get('/api/conservations');

        promise.then(function (result) {
            vm.Conservation = result.data;
        }, function (result) {
            console.log(result);
        });

        vm.Add = function (conservation) {
            var copy = angular.copy(conservation);
            conservation.name = '';

            var promise = $http.post('/api/conservations', copy);
            promise.then(function (result) {
                vm.Conservation.push(result.data);
            });
        };

        vm.Remove = function (conservation) {
            var url = '/api/conservations/{id}'.replace('{id}', conservation.id);

            var promise = $http.delete(url);
            promise.then(function (result) {
                var index = vm.Conservation.indexOf(conservation);
                vm.Conservation.splice(index, 1);
            });
        };
    }
})();