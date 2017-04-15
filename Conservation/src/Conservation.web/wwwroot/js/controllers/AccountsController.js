(function () {
    'use strict';

    var application = angular.module('Application');

    application.controller('AccountsController', AccountsController);

    AccountsController.$inject = ['$http', '$location'];

    function AccountsController($http, $location) {
        var vm = this;

        vm.Account = [];

        vm.Register = function (model) {
            var promise = $http.post('/account/register', model);
            promise.then(function (result) {
                $location.path('/');
            }, function (result) {
                console.log(result);
            });
        };

        vm.Login = function (model) {
            var promise = $http.post('/account/login', model);
            promise.then(function (result) {
                $location.path('/');
            }, function (result) {
                console.log(result);
            });
        };

    }
})();