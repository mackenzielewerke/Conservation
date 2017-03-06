

(function () {
    'use strict';

    var application = angular.module('Application');

    application.controller('AuthenticationController', AuthenticationController);

    AuthenticationController.$inject = ['$http', '$location'];

    function AuthenticationController($http, $location) {
        var vm = this;

        vm.Authentication = [];

        vm.Register = function (model) {
            var promise = $http.post('/authentication/register', model);
            promise.then(function (result) {
                $location.path('/');
            }, function (result) {
                console.log(result);
            });
        };

        vm.Login = function (model) {
            var promise = $http.post('/authentication/login', model);
            promise.then(function (result) {
                $location.path('/');
            }, function (result) {
                console.log(result);
            });
        };

    }
})();