'use strict';

angular.module('admin').controller('ArticlesCtrl', ['$scope', 'utilsSrvc', 'adminRqst',
function ($scope, utilsSrvc, adminRqst) {
    // статусы статьи
    // 0 - на модерации
    // 1 - опубликована
    // 2 - отклонена
    // 3 - статья-пример от Fortress

    var articleStatuses = {
        'Moderate'  :   0,
        'Publish'   :   1,
        'Reject'    :   2,
        'Fortress'  :   3
    };

    // ошибки
    // ---------------
    $scope.er = {
        server: false   // доступность сервера
    }

    // лоадеры
    // ---------------
    $scope.ld = {
        articles: false // loader загрузки статей
    }

    // данные 
    // ---------------
    $scope.model = {};

    getArticles();

    // функция для загрузки статей
    // ---------------
    function getArticles(filter) {
        filter = filter || {};
        $scope.isLoading = true;
        adminRqst.getArticles(utilsSrvc.token.add(filter))
            .then(function (res) {
                if (res.data.length) {
                    $scope.model.articles = res.data;
                }
                $scope.er.server = false;
            }, function () {
                $scope.er.server = true;
            }).finally(function () {
                $scope.isLoading = false;
            });
    }

    // запрос списка статей
    // ---------------
    $scope.getArticles = function (filter) {
        getArticles(filter);
    }

    // сменить статус статьи
    // ---------------
    $scope.changeArticleStatus = function (id, statusString, el) {
        var statusNum = articleStatuses[statusString];
        adminRqst.changeArticleStatus(utilsSrvc.token.add({ id: id, status: statusNum }))
            .then(function (res) {
                if (res.data.success) {
                    el.status = statusNum;
                }
            });
    }

}]);
