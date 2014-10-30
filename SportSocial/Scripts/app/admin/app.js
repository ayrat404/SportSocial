var app = angular.module('admin', ['shared', 'ngRoute']);
  
// SPA admin
// ---------------
app.config(function ($routeProvider) {
    $routeProvider
        // модерация статей
        // ---------------
        .when('/', {
            templateUrl: '/Scripts/templates/admin/articles.html',
            controller: 'ArticlesCtrl'
        })
        // список всех конференций
        // ---------------
        .when('/conference/', {
            templateUrl: '/Scripts/templates/admin/conferenceList.html',
            controller: 'ConferenceListCtrl'
        })
        // создание новой конференции
        // ---------------
        .when('/conference/new', {
            templateUrl: '/Scripts/templates/admin/conferenceNew.html',
            controller: 'ConferenceNewCtrl'
        });
});


