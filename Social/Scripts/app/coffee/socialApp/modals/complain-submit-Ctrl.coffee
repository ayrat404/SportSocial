class ComplainSubmitModal extends Controller('socialApp.controllers')
    constructor: (
        $scope
        $modalInstance
        complainService
        modalData)->

        $scope.o = modalData

        # send complain
        # ---------------
        $scope.submit = ->
            $scope.o.text = $scope.text
            complainService.submit(modalData).then (res)->
                $modalInstance.close()
                if typeof modalData.success == 'function'
                    modalData.success res

        $scope.close = ->
            $modalInstance.close()