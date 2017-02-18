

(function () {

    var application = angular.module('Application');

    var controller = application.controller('CircusController', CircusController);

    CircusController.$inject = ['$http'];

    function CircusController($http) {
        var vm = this;

        var count = 0;

        vm.Circus = [];

        var promise = $http.get('api/circus');

        promise.then(function (result) {
            vm.Circus = result.data;
        });

        vm.Add = function (circus) {
            var copy = angular.copy(circus);
            circus.name = '';

            var promise = $http.post('api/circus', copy);
            promise.then(function (result) {
                vm.Circus.push(result.data);
            }, function (result) {
            });
        };

        vm.Remove = function (circus) {

            var url = 'api/circus/{id}'.replace('{id}', circus.id);

            var promise = $http.delete(url);
            promise.then(function (result) {


                var index = vm.Circus.indexOf(circus);
                vm.Circus.splice(index, 1);
            });
        };
    }
})();