

(function () {

    var application = angular.module('Application');

    var controller = application.controller('AuthenticationController', AuthenticationController);

    AuthenticationController.$inject = ['$http'];

    function AuthenticationController($http) {
        var vm = this;

        var count = 0;

        vm.Authentication = [];

        var promise = $http.get('api/authentication');

        promise.then(function (result) {
            vm.Authentication = result.data;
        });

        vm.Add = function (authentication) {
            var copy = angular.copy(authentication);
            authentication.email = '';
            
    

            var promise = $http.post('api/authentication', copy);
            promise.then(function (result) {
                vm.Authentication.push(result.data);
            }, function (result) {
            });
        };

        vm.Remove = function (authentication) {

            var url = 'api/authentication/{email}'.replace('{email}', authentication.email);

            var promise = $http.delete(url);
            promise.then(function (result) {


                var index = vm.Authentication.indexOf(authentication);
                vm.Authentication.splice(index, 1);
            });
        };
    }
})();