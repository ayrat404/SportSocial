class SettingsAccount extends Controller('socialApp.controllers')
    constructor: (
        $scope
        $state
        $stateParams
        $rootScope
        settingsService
        modalService)->

        _this = this

        _this.model = {}

        _this.pageError = false
        _this.pageLoading = true
        _this.passwordLoading = false
        _this.passwordValidation = {}


        # get account settings
        # ---------------
        settingsService.getAccountSettings().then (res)->
            _this.model = res.data
        , (res)->
            _this.pageError = true
        .finally (res)->
            _this.pageLoading = false

        # change password
        # ---------------
        _this.changePassword = ->
            _this.passwordLoading = true
            settingsService.changePassword(_this.pass).then (res)->
                for k,v of _this.pass
                    _this.pass[k] = ''
            , (res)->
                _this.passwordValidation = res.errors
            .finally (res)->
                _this.passwordLoading = false