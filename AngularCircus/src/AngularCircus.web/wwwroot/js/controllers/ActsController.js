(function () {

    var application = angular.module('Application');

    var controller = application.controller('ActsController', ActsController);

    ActsController.$inject = ['$http'];

    function ActsController($http) {
        var vm = this;

        var count = 0;

        vm.Act = [];
        vm.circus = []; //this didn't exist

        var promise = $http.get('/api/circuses/' + vm.circus.id + '/acts'); //this was circusId instead of vm.circus

        promise.then(function (result) {
            vm.Act = result.data;
        }, function (result) {
            console.log(result)
        });

        vm.Add = function (circusId, act) { //pass circusId in here maybe if SPencer is retarded
            var copy = angular.copy(act); //act is passing up a number isntead of a name (e.g.,, 4)
            act.name = '';


            var promise = $http.post('/api/circuses/' + circusId + '/acts', copy); //SPencer didn't have .id here for his
            promise.then(function (result) {
                vm.Act.push(result.data);
            }, function (result) {
            });
        };

        vm.Remove = function (act) {
            
            var url = '/api/acts/{id}'.replace('{id}', act.id);



            var promise = $http.delete(url);
            promise.then(function (result) {

                var index = vm.Act.indexOf(act);
                vm.Act.splice(index, 1);
            });
        };
    }
})();