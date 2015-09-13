class LikesRow extends Controller('socialApp.controllers')
    constructor: (
        $scope
        $rootScope
        likeService)->

        # like action
        # ---------------
        $scope.like = ->
            likeService.set(id: $scope.id, entityType: $scope.type, current: $scope.likes.isLiked).then (res, newStatus)->
                $scope.likes.isLiked = newStatus
                if newStatus
                    $scope.likes.list.unshift
                        id: $rootScope.user.id
                        fullName: $rootScope.user.fullName
                        avatar: $rootScope.user.avatar
                else
                    for l, i in $scope.likes.list
                        if l.id == $rootScope.user.id
                            $scope.likes.list.splice i, 1
                            break

