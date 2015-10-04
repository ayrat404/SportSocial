# CoffeeScript
class Landing extends Controller('socialApp.controllers')
    constructor: ($scope, $state, $rootScope, mixpanel)->
        $scope.$root.title = 'Fortress | Добро пожаловать'
        $scope.loading = false

        if $rootScope.user.id
            $state.go 'main.profile', { userId: $rootScope.user.id }

        $scope.o =
            url: '/'
            text: 'Fortress - социальная сеть для спортсменов'
            hashtags: ['sport', 'fortress']
        #socialshare-media="{{::prop.media}}"

        # mixpanel tracking
        # ---------------
        $scope.$on('$viewContentLoaded', ->
            mixpanel.ev.visitPage($scope.$root.title))