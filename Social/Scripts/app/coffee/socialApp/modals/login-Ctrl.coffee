class LoginSubmitModal extends Controller('socialApp.controllers')
    constructor: (
        $scope
        $state
        $modalInstance
        loginService)->

        $scope.serverValidation = {}

        # login
        # ---------------
        $scope.submit = ()->
            loginService.logIn($scope.login).then((res)->
                $modalInstance.dismiss()
                # todo after login success
            , (res)->
                $scope.serverValidation = res.errors
            )

        # to registration page
        # ---------------
        $scope.toRegistration = ()->
            $modalInstance.close()
            $state.go 'registration'
