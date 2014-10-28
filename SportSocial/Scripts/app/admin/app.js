var app = angular.module('admin', ['shared', 'ngRoute']);
   
app.config(function ($routeProvider) {
    $routeProvider
        .when('/', {
            templateUrl: '/Scripts/templates/admin/articles.html',
            controller: 'ArticlesCtrl'
        })
        .when('/conference', {
            templateUrl: '/Scripts/templates/admin/conference.html',
            controller: 'ConferenceCtrl'
        });
});


