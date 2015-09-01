# submit modal
# ---------------
class SupportSubmitModal extends Controller('socialApp.controllers')
    constructor: (
        $scope
        $modalInstance
        supportService
        modalService)->

        $scope.serverValidation = {}

        $scope.submit = ->
            supportService.send($scope.support).then((res)->
                $modalInstance.dismiss()
                modalService.show(name: 'supportSuccess')
            , (res)->
                $scope.serverValidation = res.errors
            )

# success modal
# ---------------
class SupportSuccessModal extends Controller('socialApp.controllers')
    constructor: (
        $scope
        $modalInstance)->

        $scope.close = ->
            $modalInstance.dismiss()

