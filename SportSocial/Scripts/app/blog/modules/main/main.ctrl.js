'use strict';

angular.module('blog').controller('MainBlogCtrl', ['$scope',
function ($scope) {

    // переменные
    // ---------------
    $scope.nav = {
        user: {
            isOpen: false
        }
    }

    // функции
    // ---------------
    $scope.fn = {};

    // меню пользователя в header
    // ---------------
    $scope.fn.closeHeaderMenu = function () {
        $scope.nav.user.isOpen = false;
        $scope.$digest();
    }

}]);
