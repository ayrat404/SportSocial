# CoffeeScript
app = angular.module('appSrvc', [])
    .constant 'servicesDefault',
        showNotice: true
        baseServiceUrl: '/api'