class JournalModalSubmit extends Controller('socialApp.controllers')
    constructor: (
        $scope
        $modalInstance
        journalSubmitService)->

        # journal model
        # ---------------
        $scope.j =
            text: ''
            themes: []
            media: []

        # remove item from media array
        # ---------------
        $scope.removeMedia = (item)->
            index = $scope.j.media.indexOf(item)
            if index != -1
                $scope.j.media.splice(index, 1)

        # submit form
        # ---------------
        $scope.submit = ->
            debugger
            journalSubmitService.submit($scope.j).then((res)->
                $modalInstance.close()
            , (res)->

            )