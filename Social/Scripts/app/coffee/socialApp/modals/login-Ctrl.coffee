class LoginSubmitModal extends Controller('socialApp.controllers')
    constructor: (
        $scope
        $state
        $modalInstance
        loginService
        modalData)->

        $scope.serverValidation = {}

        # login
        # ---------------
        $scope.submit = ()->
            loginService.logIn($scope.login).then((res)->
                $modalInstance.dismiss()
                modalData.success(res) if typeof modalData.success == 'function'
                # todo after login success
            , (res)->
                $scope.serverValidation = res.errors
            )

        # to registration page
        # ---------------
        $scope.toRegistration = ()->
            $modalInstance.close()
            $state.go 'registration'

        $modalInstance.result.catch ->
            modalData.cancel() if typeof modalData.cancel == 'function'
