'use strict';

angular.module('blog').controller('CommentCtrl',
    ['$scope',
     'articleRqst',
     'utilsSrvc',
     '$window',
     '$timeout',
function ($scope, articleRqst, utilsSrvc, $window, $timeout) {

    // типы комментариев
    // type: comment/answer

    var prop = {
        isAnswer    : false,
        watcher     : null,
        answerFor   : null
    };

    // fake
    // ---------------
    //$scope.comments = [
    //    {
    //        id: 1,
    //        avatar: '/Content/images/temp/user.jpg',
    //        name: 'Алексей',
    //        surname: 'Рябов',
    //        date: '10.08.14',
    //        text: 'Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur.'
    //    },
    //    {
    //        id: 2,
    //        avatar: '/Content/images/temp/user.jpg',
    //        name: 'Виктор',
    //        surname: 'Рябов',
    //        date: '11.08.14',
    //        text: 'Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur.'
    //    }
    //];

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
                if (res.data.success) {
                    $scope.comments = res.data.comments.concat($scope.comments);
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
        //$scope.comments.push({
        //    id: 2,
        //    avatar: '/Content/images/temp/user.jpg',
        //    name: 'Херасе',
        //    surname: 'Тимофеевич',
        //    date: '13123',
        //    text: text,
        //    cfor: {
        //        id: prop.answerFor.id,
        //        name: prop.answerFor.name
        //    }
        //});
        var data = {};
        data.text = text;
        if (prop.isAnswer) {    // если создается ответ на комментарий
            data.type = 'answer';
            data.answerId = prop.answerFor.id;
        } else {                // если создается комментарий
            data.type = 'comment';
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
