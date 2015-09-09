class LikesRow extends Controller('socialApp.controllers')
    constructor: (
        $scope
        $rootScope
        likeService)->

        # like action
        # ---------------
        $scope.like = ->
            likeService.set(id: $scope.id, type: $scope.type, current: $scope.likes.isLiked).then (res, newStatus)->
                $scope.likes.isLiked = newStatus
                if newStatus
                    $scope.likes.list.unshift
                        id: $rootScope.user.id
                        fullName: $rootScope.user.fullName
                        avatar: $rootScope.user.avatar

