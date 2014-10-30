'use strict';

angular.module('admin').controller('ConferenceListCtrl', ['$scope', 'utilsSrvc', 'adminRqst',
function ($scope, utilsSrvc, adminRqst) {
    // статусы конференций
    // 0 - создана (не началась, не закончилась)
    // 1 - идет (началась, но не закончилась)
    // 2 - закончилась (конференция была проведена)
    // 3 - удалена (конференция была создана и удалена без проведения)

    var confStatuses = {
        'Created': 0,
        'Process': 1,
        'Finish': 2,
        'Remove': 3
    };

    // данные
    // --------------
    $scope.model = {
        conferences:
        [
            { id: 1, title: 'OPOPOP1', status: 0, url: '#', date: '20.11.2014' },
            { id: 2, title: 'OPOPOP2', status: 1, url: '#', date: '20.11.2014' },
            { id: 3, title: 'OPOPOP3', status: 2, url: '#', date: '20.11.2014' },
            { id: 4, title: 'OPOPOP4', status: 0, url: '#', date: '20.11.2014' },
            { id: 5, title: 'OPOPOP5', status: 1, url: '#', date: '20.11.2014' }
        ]
    }

    // запрос списка конференций
    // ---------------
    $scope.getConferences = function (filter) {
        filter = filter || {};
        $scope.isLoading = true;
        adminRqst.getConferences(utilsSrvc.token.add(filter))
            .then(function (res) {
                if (res.data.success) {
                    $scope.model.conferences.push(res.data.conferences);
                    $scope.isLoading = false;
                }
            }, function () {

            });
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
