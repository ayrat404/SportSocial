class SettingsProfile extends Controller('socialApp.controllers')
    constructor: (
        $scope
        $state
        $stateParams
        $rootScope
        settingsService)->

        _this = this

        _this.model = {}
        _this.prop = {}

        _this.pageError = false
        _this.pageLoading = true
        _this.serverValidation = {}

        # datepicker birthday options
        # ---------------
        _this.datepickerOptions =
            maxDate: Date.now()

        # get profile settings
        # ---------------
        settingsService.getProfileSettings().then (res)->
            _this.model = res.data.model
            _this.prop = res.data.prop
        , (res)->
            _this.pageError = true
        .finally (res)->
            _this.pageLoading = false


        # save
        # ---------------
        _this.save = ->
            _this.pageLoading = true
            settingsService.saveProfileSettings(_this.model).then (res)->
                console.log 'success'
            , (res)->
                _this.serverValidation = res.errors
            .finally (res)->
                _this.pageLoading = false
