class aboutAchievementModal extends Controller('socialApp.controllers')
    constructor: (
        $state
        $scope
        $modalInstance)->

        # to achievement submit page
        # ---------------
        $scope.goAddAchievement = ->
            $modalInstance.close()
            #$state.go 'achievementSubmit'