

(function () {

    var application = angular.module('Application');

    var controller = application.controller('ActController', ActController);

    ActController.$inject = ['$http'];

    function ActController($http) {
        var vm = this;

        var count = 0;

        vm.Act = [];

        var promise = $http.get('api/act');

        promise.then(function (result) {
            vm.Act = result.data;
        }, function (result) {
            console.log(result)
        });

        vm.Add = function (act) {
            var copy = angular.copy(act);
            act.name = '';
            

            var promise = $http.post('api/act', copy);
            promise.then(function (result) {
                vm.Act.push(result.data);
            }, function (result) {
            });
        };

        vm.Remove = function (act) {

            var url = 'api/act/{id}'.replace('{id}', act.id);

            var promise = $http.delete(url);
            promise.then(function (result) {


                var index = vm.Act.indexOf(act);
                vm.Act.splice(index, 1);
            });
        };
    }
})();