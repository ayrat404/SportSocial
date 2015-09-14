class AchievementSubmit extends Controller('socialApp.controllers')
    constructor: (
        $scope
        $stateParams
        $rootScope)->

        $scope.$root.title = 'Fortress | Заявка на награду'

        prop =
            stepsLength: 3

        _this = this

        _this.steps = {
            current: 0
        }

        # prev step
        # ---------------
        _this.prevStep = ->
            _this.steps.current--

        # next step
        # ---------------
        _this.nextStep = ->
            _this.steps.current++
