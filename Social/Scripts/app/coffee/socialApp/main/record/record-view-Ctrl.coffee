class RecordView extends Controller('socialApp.controllers')
    constructor: (
        $scope
        $stateParams
        $rootScope
        journalService
        mixpanel)->

        _this = this
        _this.it =
            loaded: false

        # get single record data
        # ---------------
        journalService.getById(+$stateParams.id).then((res)->
            _this.it = res.data
            _this.it.loaded = true
        )
