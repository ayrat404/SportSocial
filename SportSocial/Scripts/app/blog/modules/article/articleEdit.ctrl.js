'use strict';

angular.module('blog').controller('ArticleEditCtrl',
    ['$scope',
     'articleRqst',
     'utilsSrvc',
     '$timeout',
     '$window',
function ($scope, articleRqst, utilsSrvc, $timeout, $window) {

    // service helper
    // ---------------
    var p = {
        'edit': 'editArticle',
        'new': 'createArticle'
    }

    // сообщения после попытки создания/сохранения статьи
    // ---------------
    $scope.msg = {
        show    :   false,  // показать блок с сообщениями
        success :   false,  // сообщение об успешной операции
        error   :   false   // сообщение об ошибке
    }

    // свойста
    // ---------------
    $scope.prop = {
        btnIsDisabled: false
    }

    // редактирование/создание статьи
    // ---------------
    $scope.sendArticleData = function (article) {
        $scope.prop.btnIsDisabled = true;
        articleRqst[p[$scope.type]](utilsSrvc.token.add(article))
            .then(function (res) {
                $scope.msg.show = true;
                if (res.data.success) {
                    $scope.msg.success = true;
                } else {
                    $scope.msg.error = true;
                }
                $timeout(function () {
                    for (var v in $scope.msg) {
                        $scope.msg[v] = false;
                    }
                    $scope.prop.btnIsDisabled = false;
                    $window.location = '/';
                }, 4000);
            });
    }

}]);
