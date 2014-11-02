var app = angular.module('admin', ['shared', 'ngRoute']);

// SPA admin
// ---------------
app.config(function ($routeProvider) {
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
        // редактирование конференции
        // ---------------
        .when('/conference/item/:id', {
            templateUrl: '/Scripts/templates/admin/conference/conferenceEdit.html',
            controller: 'ConferenceEditCtrl'
        });
});


