'use strict';

// Контроллер добавления комментариев
// ---------------
angular.module('blog').controller('CommentsCtrl',
    ['$scope',
     'commentsRqst',
     'utilsSrvc',
     '$window',
     '$timeout',
function ($scope, commentsRqst, utilsSrvc, $window, $timeout) {

    // типы комментариев (новый комментарий, ответ на комментарий)
    // type: comment/answer

    var prop = {
            isAnswer    :   false,  // если этот ответ к комментарию
            watcher     :   null,   // watcher для поля ответа к комментарию
            answerFor   :   null    // комментарий на который пишется ответ
        },
        lightClass = 'cl__it--light';   // класс который подсвечивает комментарий

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
    $scope.loadAll = function (callback) {
        commentsRqst.loadComments({ id: $scope.itemId, itemType: $scope.itemType})
            .then(function(res) {
                if (res.data.length) {
                    // если грузим все комменты и вставляем
                    $scope.comments = res.data;
                    // скрываем кнопку загрузки предыдущих комментариев
                    $scope.more = 0;
                    // если грузим оставшиеся комменты
                    //$scope.comments = res.data.comments.concat($scope.comments);
                    // если был передан callback, вызываем его
                    if (typeof callback == 'function') {
                        callback(res.data);
                    }
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
        $scope.m.text = c.name + ', ';
        prop.watcher = $scope.$watch('m.text', function (val) {
            if (c.name !== val.substr(0, c.name.length)) {
                prop.isAnswer = false;
                prop.watcher();
            }
        });
    }

    // не в дериктиве, а через селектор id, чтобы снизить нагрузку
    // ---------------
    $scope.scrollToFor = function (id, repeatCall) {
        var $el = angular.element("#comment_" + id);
        // если комментарий есть на странице, то скроллим к нему
        // ----------
        if ($el.length) {
            angular.element('html, body').animate({
                scrollTop: $el.offset().top - $window.innerHeight / 2
            }, 300);
            $el.addClass(lightClass);
            $timeout(function () {
                $el.removeClass(lightClass);
            }, 1000);
        // если нет, то подгружаем все комментарии и ищем его там
        // ----------
        } else if (repeatCall !== true) {
            $scope.loadAll(function () {
                // добавляем timeout для того чтобы элементы отрисовались
                // -----
                $timeout(function () {
                    $scope.scrollToFor(id, true);
                });
            });
        }
    }

    // отправка данных для создания комментария или ответа
    // ---------------
    $scope.createComment = function (d) {
        var data = {};
        angular.extend(data, d);
        data.itemId = $scope.itemId;
        data.itemType = $scope.itemType;
        if (prop.isAnswer) {    // если создается ответ на комментарий
            data.commentType = 'answer';
            data.commentForId = prop.answerFor.id;
            data.text = d.text.substr(prop.answerFor.name.length + 2, d.text.length - 1);
        } else {                // если создается комментарий
            data.commentType = 'comment';
            data.text = d.text;
        }
        commentsRqst.createComment(utilsSrvc.token.add(data))
            .then(function (res) {
                if (res.data.success) {
                    $scope.comments.push(res.data.comment);
                    $scope.m.text = '';
                } else {
                    $scope.er.create = true;
                }
            }, function () {
                $scope.er.server = true;
            }).finally(function () {
                $scope.ld.creating = false;
            });
    }

}]);
