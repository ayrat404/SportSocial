class LikesRow extends Controller('socialApp.controllers')
    constructor: (
        $scope)->

        $scope.like = ->
            console.log('like ' + $scope.type + ' with id=' + $scope.id)

