class LikesInFinger extends Controller('socialApp.controllers')
    constructor: (
        $scope
        $rootScope
        $timeout
        likeService)->
            # like method
            # ---------------
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

            # todo refactor create method
            # ---------------
            $rootScope.$on 'changeAvatar', (event, newAvatar)->
                for item,index in $scope.likes.list
                    if item.id == $rootScope.user.id
                        $scope.likes.list[index].avatar = newAvatar
                        break