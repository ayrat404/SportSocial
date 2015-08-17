# CoffeeScript
class Registration extends Controller('socialApp.controllers')
    constructor: ($scope, mixpanel, registrationService)->
        $scope.$root.title = 'Fortress | Регистрация'
        $scope.loading = false
        
        # mixpanel tracking
        # ---------------
        $scope.$on('$viewContentLoaded', ->
            mixpanel.ev.visitPage($scope.$root.title))