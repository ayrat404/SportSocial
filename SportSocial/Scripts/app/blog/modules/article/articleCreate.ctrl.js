'use strict';

angular.module('app').controller('ArticleCreateCtrl',
    ['$scope',
     'articleRqst',
     'tokenRqst',
function ($scope, articleRqst, tokenRqst) {

    // ответ от сервера на загрузку изображения
    // ---------------
    $scope.test = function(res) {
        debugger;
    }

    //$scope.$on('flow::fileAdded', function (event, $flow, flowFile) {
    //    debugger;
    //});

    $scope.createArticle = function() {
        
    }

}]);
