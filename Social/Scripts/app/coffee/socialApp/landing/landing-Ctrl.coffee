# CoffeeScript
class Landing extends Controller('socialApp.controllers')
    constructor: ($scope, $state, $rootScope, mixpanel)->
        $scope.$root.title = 'Fortress | Добро пожаловать'
        $scope.loading = false

        if $rootScope.user.id
            $state.go 'main.profile', { userId: $rootScope.user.id }

        # mixpanel tracking
        # ---------------
        $scope.$on('$viewContentLoaded', ->
            mixpanel.ev.visitPage($scope.$root.title))