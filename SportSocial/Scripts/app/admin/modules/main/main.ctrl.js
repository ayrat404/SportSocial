'use strict';

angular.module('admin').controller('MainAdminCtrl', ['$scope', 'utilsSrvc',
function ($scope, utilsSrvc) {
    console.log(utilsSrvc.token.get().obj);
}]);
