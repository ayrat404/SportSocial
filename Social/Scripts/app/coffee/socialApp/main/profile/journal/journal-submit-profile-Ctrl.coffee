class JournalProfileSubmit extends Controller('socialApp.controllers')
    constructor: (
        $scope
        journalService)->

        # journal model
        # ---------------
        $scope.j =
            text: ''
            themes: []
            media: []

        # take watcher form
        takeWatcher = ->
            textWatcher = $scope.$watch('j.text', (oldVal, newVal)->
                if newVal && newVal.length > 3 && oldVal != newVal
                    $scope.open = true
                    textWatcher()
            )

        # open journal form
        # ---------------
        $scope.open = false
        takeWatcher()

        # close journal form
        $scope.closeForm = ->
            $scope.open = false
            $scope.resetForm()
            takeWatcher()


        # remove item from media array
        # ---------------
        $scope.removeMedia = (item)->
            index = $scope.j.media.indexOf(item)
            if index != -1
                $scope.j.media.splice(index, 1)

        # reset form
        # ---------------
        $scope.resetForm = ->
            $scope.j.text = ''
            $scope.j.themes = []
            $scope.j.media = []

        # submit form
        # ---------------
        $scope.submit = ->
            journalService.submit($scope.j).then((res)->
                $scope.closeForm()
                $scope.success({$res: res})
            , (res)->

            )