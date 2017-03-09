(function () {

    var application = angular.module('Application');

    var controller = application.controller('GroupsController', GroupsController);

    GroupsController.$inject = ['$http'];

    function GroupsController($http) {
        var vm = this;

        var count = 0;

        vm.Group = [];
        vm.conservation = []; //this didn't exist

        var promise = $http.get('/api/conservations/' + vm.conservation.id + '/groups'); //this was circusId instead of vm.circus

        promise.then(function (result) {
            vm.Conservation = result.data;
        }, function (result) {
            console.log(result)
        });

        vm.Add = function (conservationId, group) { //pass circusId in here maybe if SPencer is retarded
            var copy = angular.copy(group); //act is passing up a number isntead of a name (e.g.,, 4)
            group.name = '';


            var promise = $http.post('/api/conservations/' + conservationId + '/groups', copy); //SPencer didn't have .id here for his
            promise.then(function (result) {
                vm.Group.push(result.data);
            }, function (result) {
            });
        };

        vm.Remove = function (group) {
            
            var url = '/api/groups/{id}'.replace('{id}', group.id);



            var promise = $http.delete(url);
            promise.then(function (result) {

                var index = vm.Exhibit.indexOf(group);
                vm.Group.splice(index, 1);
            });
        };
    }
})();