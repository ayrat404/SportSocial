# CoffeeScript
app = angular.module('appSrvc', [])
    .constant 'servicesDefault',
        noticeShow: {
            errors: true
        }
        baseServiceUrl: '/api'