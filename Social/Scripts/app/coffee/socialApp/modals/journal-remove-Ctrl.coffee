class JournalModalRemove extends Controller('socialApp.controllers')
    constructor: (
        $scope
        $modalInstance
        journalService
        modalData)->

        # remove journal item
        # ---------------
        $scope.remove = ->
            journalService.remove(modalData.id).then (res)->
                $modalInstance.close()
                if typeof modalData.success == 'function'
                    modalData.success res

        $scope.close = ->
            $modalInstance.close()