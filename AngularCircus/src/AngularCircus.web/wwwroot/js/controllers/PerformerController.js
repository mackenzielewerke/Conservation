

(function () {

    var application = angular.module('Application');

    var controller = application.controller('PerformerController', PerformerController);

    PerformerController.$inject = ['$http'];

    function PerformerController($http) {
        var vm = this;

        var count = 0;

        vm.Performer = [];

        var promise = $http.get('api/performer');

        promise.then(function (result) {
            vm.Performer = result.data;
        });

        vm.Add = function (performer) {
            var copy = angular.copy(performer);
            performer.name = '';

            var promise = $http.post('api/performer', copy);
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