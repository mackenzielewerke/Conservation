(function () {
    'use strict';

    angular
        .module('Application')
        .controller('AuthenticationController', AuthenticationController);

    AuthenticationController.$inject = ['$http', '$window'];

    function AuthenticationController($http, $window) {
        /* jshint validthis:true */
        var vm = this;

        vm.Register = function (model) {
            var promise = $http.post('/authentication/register', model);
            promise.then(function (result) {
                $window.location.href = '/';

           
            });
        };

        vm.Login = function (model) {
            var promise = $http.post('/authentication/logins', model);
            promise.then(function (result) {
                $window.location.href = '/';
            });
        };
    }
})();
