class LikesInRow extends Directive('socialApp.directives')
    constructor: (
        $rootScope
        likeService)->
        return {
            restrict: 'E'
            require: 'ngModel'
            replace: true
            scope:
                likes: '=ngModel'
                id: '@'
                type: '@'
                opts: '@'
            controller: ($scope)->
                $scope.like = ->
                    if !$scope.loading
                        $scope.loading = true
                        likeService.set(id: $scope.id, entityType: $scope.type, current: $scope.likes.isLiked).then (newStatus)->
                            $scope.likes.isLiked = newStatus
                            if newStatus
                                $scope.likes.count++
                                $scope.likes.list.unshift
                                    id: $rootScope.user.id
                                    fullName: $rootScope.user.fullName
                                    avatar: $rootScope.user.avatar
                            else
                                $scope.likes.count--
                                for l, i in $scope.likes.list
                                    if l.id == $rootScope.user.id
                                        $scope.likes.list.splice i, 1
                                        break
                        .finally (res)->
                            $scope.loading = false
            templateUrl: '/template/components/likes/likes-rowTpl'
            link: (scope, element, attrs, ngModel)->
                defaults =
                    rowCount: 4
                    showLink: true
                    imageSize: 50

                scope.o = angular.extend defaults, eval('(' + scope.opts + ')')
        }