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
            loginService.logIn($scope.login).then (res)->
                $modalInstance.close()
                if modalData &&
                  typeof modalData.success == 'function'
                    modalData.success(res)
                else
                    $state.go 'main.profile', userId: res.data.id
            , (res)->
                $scope.serverValidation = res.data.errors

        # to registration page
        # ---------------
        $scope.toRegistration = ()->
            $modalInstance.close()
            $state.go 'registration'

        $modalInstance.result.catch ->
            modalData?.cancel?()
