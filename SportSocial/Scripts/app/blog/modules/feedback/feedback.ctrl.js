'use strict';

angular.module('blog').controller('FeedbackCtrl',
    ['$scope',
     'feedbackRqst',
     'utilsSrvc',
function ($scope, feedbackRqst, utilsSrvc) {

    // сообщения после попытки создания/сохранения статьи
    // ---------------
    $scope.msg = {
        show    :   false,  // показать блок с сообщениями
        success :   false,  // сообщение об успешной операции
        server  :   '',     // сообщение об ошибке
        s404    :   false   // сервер недоступен
    }

    // отправка отзыва
    // ---------------
    $scope.sendFeedback = function (data) {
        for (var v in $scope.msg) {
            $scope.msg[v] = false;
        }
        feedbackRqst.sendFeedback(utilsSrvc.token.add(data))
            .then(function(res) {
                if (res.data.success) {
                    $scope.msg.success = true;
                } else {
                    $scope.msg.server = res.data.errorMessage;
                }
            }, function() {
                $scope.msg.s404 = true;
            }).finally(function() {
                $scope.msg.show = true;
            });
    }

}]);
