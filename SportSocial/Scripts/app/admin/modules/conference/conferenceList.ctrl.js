'use strict';

angular.module('admin').controller('ConferenceListCtrl', ['$scope', 'utilsSrvc', 'adminRqst', '$routeParams',
function ($scope, utilsSrvc, adminRqst, $routeParams) {
    $scope.param = $routeParams.param;
    // статусы конференций
    // 0 - создана (не началась, не закончилась)
    // 1 - идет (началась, но не закончилась)
    // 2 - закончилась (конференция была проведена)
    // 3 - удалена (конференция была создана и удалена без проведения)

    var confStatuses = {
        'Created'   :   0,
        'Process'   :   1,
        'Finish'    :   2,
        'Remove'    :   3
    };

    // модели
    // ---------------
    $scope.model = {
        conferences: []     // список конференций
    }

    // ошибки
    // ---------------
    $scope.er = {
        server: false   // сервер недоступен
    }

    // лоадеры
    // ---------------
    $scope.ld = {
        conferences: false,  // loader загрузки списка конференций
    }

    // фейковые данные
    // --------------
    //$scope.model = {
    //    conferences:
    //    [
    //        { id: 1, title: 'OPOPOP1', status: 0, url: '#', date: '20.11.2014' },
    //        { id: 2, title: 'OPOPOP2', status: 1, url: '#', date: '20.11.2014' },
    //        { id: 3, title: 'OPOPOP3', status: 2, url: '#', date: '20.11.2014' },
    //        { id: 4, title: 'OPOPOP4', status: 0, url: '#', date: '20.11.2014' },
    //        { id: 5, title: 'OPOPOP5', status: 1, url: '#', date: '20.11.2014' }
    //    ]
    //}

    getConferences();

    // функция для загрузки конференций
    // ---------------
    function getConferences(filter) {
        filter = filter || {};
        $scope.isLoading = true;
        adminRqst.getConferences(filter)
            .then(function (res) {
                if (res.data.length) {
                    $scope.model.conferences = res.data;
                }
            }, function () {
                $scope.er.server = true;
            }).finally(function () {
                $scope.isLoading = false;
            });
    }

    // запрос списка конференций
    // ---------------
    $scope.getConferences = function (filter) {
        getConferences(filter);
    }

    // сменить статус конференции
    // ---------------
    $scope.changeConferenceStatus = function (id, statusString, el) {
        var statusNum = confStatuses[statusString];
        adminRqst.changeConferenceStatus(utilsSrvc.token.add({ id: id, status: statusNum }))
            .then(function (res) {
                if (res.data.success) {
                    el.status = statusNum;

                    // если удаляем конференцию,
                    // то убираем её из массива и из списка
                    // ----------
                    if (el.status == 3) {
                        $scope.forEach(function(v, i, ar) {
                            if (v.id === id) {
                                ar.splice(i, 1);
                                return;
                            }
                        });
                    }
                }
            });
    }
    
}]);
