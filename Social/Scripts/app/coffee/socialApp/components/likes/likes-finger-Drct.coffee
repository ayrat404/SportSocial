class LikesInFinger extends Directive('socialApp.directives')
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
                entityType: '@'
            controller: ($scope)->
                $scope.like = ->
                    likeService.set(id: $scope.id, entityType: $scope.entityType, current: $scope.likes.isLiked).then (newStatus)->
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
            templateUrl: '/template/components/likes/likes-fingerTpl'
        }