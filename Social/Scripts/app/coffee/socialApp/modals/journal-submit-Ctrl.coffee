class JournalModalSubmit extends Controller('socialApp.controllers')
    constructor: (
        $scope
        $modalInstance
        journalService
        modalData)->

        # journal model
        # ---------------
        $scope.j =
            text: ''
            tags: []
            media: []

        if modalData && modalData.model
            angular.copy modalData.model, $scope.j


        # remove item from media array
        # ---------------
        $scope.removeMedia = (item)->
            index = $scope.j.media.indexOf item
            if index != -1
                $scope.j.media.splice(index, 1)

        # submit form
        # ---------------
        $scope.submit = ->
            journalService.save($scope.j).then (res)->
                if modalData && typeof modalData.success == 'function'
                    modalData.success $scope.j
                $modalInstance.close()