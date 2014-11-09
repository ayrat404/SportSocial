'use strict';

angular.module('blog').controller('CommentCtrl',
    ['$scope',
     'articleRqst',
     'utilsSrvc',
     '$window',
     '$timeout',
function ($scope, articleRqst, utilsSrvc, $window, $timeout) {

    // типы комментариев (комментарий к статье, ответ на комментарий)
    // type: comment/answer

    var prop = {
        isAnswer    : false,    // если этот ответ к комментарию
        watcher     : null,     // watcher для поля ответа к комментарию
        answerFor   : null      // комментарий на который пишется ответ
    };

    // ошибки
    // ---------------
    $scope.er = {
        server: false,
        create: false
    }

    // лоадеры
    // ---------------
    $scope.ld = {
        creating: false
    }

    // загрузка комментариев, которые не отображаются
    // ---------------
    $scope.loadAll = function () {
        articleRqst.loadComments(utilsSrvc.token.add({ id: $scope.itemId }))
            .then(function(res) {
                if (res.data.length) {
                    // если грузим все комменты и вставляем
                    $scope.comments = res.data;
                    // если грузим оставшиеся комменты
                    //$scope.comments = res.data.comments.concat($scope.comments);
                } else {
                    console.log('load comments : server error');
                }
            }, function() {
                console.log('load comments : server unavalible');
            });
    }

    // пользователь активировал ответ на комментарий
    // ---------------
    $scope.createAnswer = function (c) {
        prop.isAnswer = true;
        prop.answerFor = c;
        $scope.focus = !$scope.focus;
        $scope.text = c.name + ', ';
        prop.watcher = $scope.$watch('text', function (val) {
            if (c.name !== val.substr(0, c.name.length)) {
                prop.isAnswer = false;
                prop.watcher();
            }
        });
    }

    // не в дериктиве, а через селектор id, чтобы снизить нагрузку
    // ---------------
    $scope.scrollToFor = function (id) {
        var $el = angular.element("#comment_" + id);
        angular.element('html, body').animate({
            scrollTop: $el.offset().top - $window.innerHeight / 2
        }, 300);
        $el.addClass('cl__it--light');
        $timeout(function() {
            $el.removeClass('cl__it--light');
        }, 1000);
    }

    // отправка данных для создания комментария или ответа
    // ---------------
    $scope.createComment = function (text) {
        var data = {};
        data.itemId = $scope.itemId;
        data.itemType = $scope.itemType;
        if (prop.isAnswer) {    // если создается ответ на комментарий
            data.commentType = 'answer';
            data.commentForId = prop.answerFor.Id;
            data.text = text.substr(prop.answerFor.name.length + 2, text.length-1);
        } else {                // если создается комментарий
            data.commentType = 'comment';
            data.text = text;
        }
        articleRqst.createComment(utilsSrvc.token.add(data))
            .then(function(res) {
                if (res.data.success) {
                    $scope.comments.push(res.data.comment);
                    $scope.text = '';
                } else {
                    $scope.er.create = true;
                }
            }, function() {
                $scope.er.server = true;
            }).finally(function() {
                $scope.ld.creating = false;
            });
    }

}]);
