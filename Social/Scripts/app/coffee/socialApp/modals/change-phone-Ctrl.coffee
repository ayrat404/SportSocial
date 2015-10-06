# get code for new phone modal
class ChangePhoneGetCodeModal extends Controller('socialApp.controllers')
    constructor: (
        $scope
        $modalInstance
        settingsService
        modalService)->

        $scope.serverValidation = {}
        $scope.submit = ->
            settingsService.sendPhoneForCode(phone: $scope.phone).then (res)->
                modalService.show(name: 'changePhoneSubmitCode', data: { phone: $scope.phone })
            , (res)->
                $scope.serverValidation = res.errors


# submit new phone modal
# ---------------
class ChangePhoneSubmitCodeModal extends Controller('socialApp.controllers')
    constructor: (
        $scope
        $modalInstance
        $modalStack
        $state
        settingsService
        modalService
        modalData)->
        
        $scope.serverValidation = {}
        $scope.model =
            phone: modalData.phone
        $scope.submit = ()->
            settingsService.sendPhoneWithCode($scope.model).then (res)->
                $modalStack.dismissAll()
                $state.reload()
            , (res)->
                $scope.serverValidation = res.errors