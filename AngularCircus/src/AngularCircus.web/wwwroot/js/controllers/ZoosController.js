

(function () {

    var application = angular.module('Application');

    var controller = application.controller('ZoosController', ZoosController);

    ZoosController.$inject = ['$http'];

    function ZoosController($http) {
        var vm = this;

        var count = 0;

        vm.Zoo = [];

        var promise = $http.get('/api/zoos');

        promise.then(function (result) {
            vm.Zoo = result.data;
        }, function (result) {
            console.log(result)
        });

        vm.Add = function (zoo) {
            var copy = angular.copy(zoo);
            zoo.name = '';

            var promise = $http.post('/api/zoos', copy);
            promise.then(function (result) {
                vm.Zoo.push(result.data)
            });
        };

        vm.Remove = function (zoo) {

            var url = '/api/zoos/{id}'.replace('{id}', zoo.id);

            var promise = $http.delete(url);
            promise.then(function (result) {


                var index = vm.Zoo.indexOf(zoo);
                vm.Zoo.splice(index, 1);
            });
        };
    }
})();