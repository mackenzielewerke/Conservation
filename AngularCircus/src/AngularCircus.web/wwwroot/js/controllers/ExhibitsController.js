(function () {

    var application = angular.module('Application');

    var controller = application.controller('ExhibitsController', ExhibitsController);

    ExhibitsController.$inject = ['$http'];

    function ExhibitsController($http) {
        var vm = this;

        var count = 0;

        vm.Exhibit = [];
        vm.zoo = []; //this didn't exist

        var promise = $http.get('/api/zoos/' + vm.zoo.id + '/exhibits'); //this was circusId instead of vm.circus

        promise.then(function (result) {
            vm.Zoo = result.data;
        }, function (result) {
            console.log(result)
        });

        vm.Add = function (zooId, exhibit) { //pass circusId in here maybe if SPencer is retarded
            var copy = angular.copy(exhibit); //act is passing up a number isntead of a name (e.g.,, 4)
            exhibit.name = '';


            var promise = $http.post('/api/zoos/' + zooId + '/exhibits', copy); //SPencer didn't have .id here for his
            promise.then(function (result) {
                vm.Exhibit.push(result.data);
            }, function (result) {
            });
        };

        vm.Remove = function (exhibit) {
            
            var url = '/api/exhibits/{id}'.replace('{id}', exhibit.id);



            var promise = $http.delete(url);
            promise.then(function (result) {

                var index = vm.Exhibit.indexOf(act);
                vm.Exhibit.splice(index, 1);
            });
        };
    }
})();