'use strict';

angular.module('admin').controller('UsersCtrl', ['$scope', 'utilsSrvc', 'adminRqst',
function ($scope, utilsSrvc, adminRqst) {
    // статусы пользователя
    // 0 - заблокирован
    // 1 - удален
    // 2 - доверенный пользователь
    // 3 - обычный пользователь

    var usersStatuses = {
        'Block': 0,
        'Remove': 1,
        'Trust': 2,
        'Simple': 3
    };

    // ошибки
    // ---------------
    $scope.er = {
        server: false   // доступность сервера
    }

    // лоадеры
    // ---------------
    $scope.ld = {
        users: false, // loader загрузки пользователей
        statistic: true // loader загрузки статистики
    }

    // данные 
    // ---------------
    $scope.model = {};

    getUsers();
    getStatistic();

    // функция для загрузки пользователей
    // ---------------
    function getUsers(filter) {
        filter = filter || {};
        $scope.isLoading = true;
        adminRqst.getUsers(utilsSrvc.token.add(filter))
            .then(function (res) {
                if (res.data.length) {
                    $scope.model.users = res.data;
                }
                $scope.er.server = false;
            }, function () {
                $scope.er.server = true;
            }).finally(function () {
                $scope.isLoading = false;
            });
    }

    // загрузка общей статистики пользователей
    // ---------------
    function getStatistic() {
        adminRqst.getUsersStatistic(utilsSrvc.token.add({}))
            .then(function(res) {
                if (res.data.length) {
                    $scope.model.statistic = res.data;
                    $scope.statisticIsLoaded = true;
                }
            }).finally(function() {
            $scope.ld.statistic = false;
        });
    }

    $scope.getUsersStatistic = function(){
        getStatistic();
    }

    // запрос списка пользователей
    // ---------------
    $scope.getUsers = function (filter) {
        getUsers(filter);
    }

    // сменить статус пользователя
    // ---------------
    $scope.changeUserStatus = function (id, statusString, el) {
        var statusNum = usersStatuses[statusString];
        adminRqst.changeUserStatus(utilsSrvc.token.add({ id: id, status: statusNum }))
            .then(function (res) {
                if (res.data.success) {
                    el.status = statusNum;
                }
            });
    }

}]);
