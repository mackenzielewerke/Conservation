(function () {

    var application = angular.module('Application');

    var controller = application.controller('AnimalController', AnimalController);

    AnimalController.$inject = ['$http'];

    function AnimalController($http) {
        var vm = this;

        var count = 0;

        vm.Animal = [];

        var promise = $http.get('api/animals');

        promise.then(function (result) {
            vm.Animal = result.data;
        }, function (result) {
            console.log(result);
        });

        vm.Add = function (animal) {
            var copy = angular.copy(animal);
            animal.name = '';

            var promise = $http.post('api/animal', copy);
            promise.then(function (result) {
                vm.Animal.push(result.data);
            }, function (result) {
            });
        };

        vm.Remove = function (animal) {

            var url = 'api/animal/{id}'.replace('{id}', performer.id);

            var promise = $http.delete(url);
            promise.then(function (result) {


                var index = vm.Animal.indexOf(animal);
                vm.Animal.splice(index, 1);
            });
        };
    }
})();