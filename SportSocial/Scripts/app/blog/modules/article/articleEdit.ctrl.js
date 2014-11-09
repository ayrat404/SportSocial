'use strict';

angular.module('blog').controller('ArticleEditCtrl',
    ['$scope',
     'articleRqst',
     'utilsSrvc',
function ($scope, articleRqst, utilsSrvc) {
    $scope.er = {
        create: {
            show: false,
            msg: ''
        }
    }

    // создание статьи
    // ---------------
    $scope.createArticle = function (article) {
        articleRqst.createArticle(utilsSrvc.token.add(article))
            .then(function(res) {
                if (!res.data.success) {
                    $scope.er.create.show = true;
                    $scope.er.create.msg = res.data.error;
                }
            });
    }

}]);
