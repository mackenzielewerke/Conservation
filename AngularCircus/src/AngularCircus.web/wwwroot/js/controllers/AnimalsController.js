

(function () {

    var application = angular.module('Application');

    var controller = application.controller('AnimalsController', AnimalsController);

    AnimalsController.$inject = ['$http'];

    function AnimalsController($http) {
        var vm = this;

        var count = 0;

        vm.Animal = [];
        vm.exhibit = [];

        var promise = $http.get('api/groups/' + vm.exhibit.id + '/animals');

        promise.then(function (result) {
            vm.Animal = result.data;
        }, function (result) {
            console.log(result);
        });

        vm.Add = function (exhibitId, animal) {
            var copy = angular.copy(animal);
            animal.name = '';
            

            var promise = $http.post('api/groups/'+ groupId + '/animals', copy);
            promise.then(function (result) {
                vm.Animal.push(result.data);
            }, function (result) {
            });
        };

        vm.Remove = function (animal) {

            var url = 'api/animals/{id}'.replace('{id}', animal.id);

            var promise = $http.delete(url);
            promise.then(function (result) {


                var index = vm.Animal.indexOf(animal);
                vm.Animal.splice(index, 1);
            });
        };
    }
})();