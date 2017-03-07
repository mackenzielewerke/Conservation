

(function () {

    var application = angular.module('Application');

    var controller = application.controller('CircusesController', CircusesController);

    CircusesController.$inject = ['$http'];

    function CircusesController($http) {
        var vm = this;

        var count = 0;

        vm.Circus = [];

        var promise = $http.get('/api/circuses');

        promise.then(function (result) {
            vm.Circus = result.data;
        }, function (result) {
            console.log(result);
        });

        vm.Add = function (circus) {
            var copy = angular.copy(circus);
            circus.name = '';

            var promise = $http.post('/api/circuses', copy);
            promise.then(function (result) {
                vm.Circus.push(result.data);
            });
        };

        vm.Remove = function (circus) {

            var url = '/api/circuses/{id}'.replace('{id}', circus.id);

            var promise = $http.delete(url);
            promise.then(function (result) {


                var index = vm.Circus.indexOf(circus);
                vm.Circus.splice(index, 1);
            });
        };
    }
})();