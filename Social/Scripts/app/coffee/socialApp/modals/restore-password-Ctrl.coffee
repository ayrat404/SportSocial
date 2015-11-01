# submit phone modal
# ---------------
class RestorePasswordSubmitPhoneModal extends Controller('socialApp.controllers')
    constructor: (
        $scope
        $modalInstance
        restorePasswordService
        modalService)->

        $scope.serverValidation = {}
        $scope.submit = ()->
            restorePasswordService.sendPhone(phone: $scope.phone).then((res)->
                $modalInstance.close()
                modalService.show(name: 'restorePasswordSubmitNewData', data: { phone: $scope.phone })
            , (res)->
                $scope.serverValidation = res.errors
            )

# submit new password modal
# ---------------
class RestorePasswordSubmitNewModal extends Controller('socialApp.controllers')
    constructor: (
        $scope
        $modalInstance
        restorePasswordService
        loginService
        modalService
        modalData)->

        $scope.serverValidation = {}
        $scope.restore =
            phone: modalData.phone
        $scope.submit = ()->
            restorePasswordService.sendNewPassword($scope.restore).then((res)->
                $modalInstance.close()
                modalService.show(name: 'loginSubmit')
            , (res)->
                $scope.serverValidation = res.errors
            )

