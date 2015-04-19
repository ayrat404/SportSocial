var app = angular.module('admin', ['shared', 'ngRoute']);

// SPA admin
// ---------------
app.config(['$routeProvider', function ($routeProvider) {
    $routeProvider
        // модерация статей
        // ---------------
        .when('/', {
            templateUrl: '/Scripts/templates/admin/articles/articles.html',
            controller: 'ArticlesCtrl'
        })
        // список всех конференций
        // ---------------
        .when('/conference/list', {
            templateUrl: '/Scripts/templates/admin/conference/conferenceList.html',
            controller: 'ConferenceListCtrl'
        })
        // создание новой конференции
        // ---------------
        .when('/conference/new', {
            templateUrl: '/Scripts/templates/admin/conference/conferenceNew.html',
            controller: 'ConferenceNewCtrl'
        })
        // список всех пользователей
        // ---------------
        .when('/users', {
            templateUrl: '/Scripts/templates/admin/users/users-list.html',
            controller: 'UsersCtrl'
        })
        // редактирование конференции
        // ---------------
        .when('/conference/item/:id', {
            templateUrl: '/Scripts/templates/admin/conference/conferenceEdit.html',
            controller: 'ConferenceEditCtrl'
        });
}]);


