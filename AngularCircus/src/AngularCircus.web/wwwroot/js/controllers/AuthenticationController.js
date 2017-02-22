(function () {
    'use strict';

    angular
        .module('Application')
        .controller('AuthenticationController', AuthenticationController);

    AuthenticationController.$inject = ['$http', '$location'];

    function AuthenticationController($http, $location) {
        /* jshint validthis:true */
        var vm = this;

        vm.Register = function (model) {
            var promise = $http.post('/authentication/register', model);
            promise.then(function (result) {
                $location.path('/');
                throw 'test';
            });
        };

        vm.Login = function (model) {
            var promise = $http.post('/authentication/logins', model);
            promise.then(function (result) {
                $location.path('/');
            });
        };
    }
})();
