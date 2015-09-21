class SubscribersInRow extends Directive('socialApp.directives')
    constructor: (
        $rootScope
        $timeout
        subscribeService)->
        return {
            restrict: 'E'
            require: 'ngModel'
            replace: true
            scope:
                subscribers: '=ngModel'
                id: '@'
                opts: '@'
            controller: ($scope)->
                $scope.subscribe = ->
                    if !$scope.loading
                        $scope.loading = true
                        subscribeService.set(id: $scope.id, current: $scope.subscribers.isSubscribed).then (newStatus)->
                            $scope.subscribers.isSubscribed = newStatus
                            if newStatus
                                $scope.subscribers.count++
                                $scope.subscribers.list.unshift
                                    id: $rootScope.user.id
                                    avatar: $rootScope.user.avatar
                            else
                                $scope.subscribers.count--
                                for l, i in $scope.subscribers.list
                                    if l.id == $rootScope.user.id
                                        $scope.subscribes.list.splice i, 1
                                        break
                        .finally (res)->
                            $timeout ->
                                $scope.loading = false
            templateUrl: '/template/components/subscribers/subscribers-rowTpl'
            link: (scope, element, attrs, ngModel)->
                defaults =
                    rowCount: 4
                    showLink: true
                    imageSize: 50

                # todo override defaults
                scope.o = defaults

                #scope.o = angular.extend defaults, eval('(' + scope.opts + ')')
        }