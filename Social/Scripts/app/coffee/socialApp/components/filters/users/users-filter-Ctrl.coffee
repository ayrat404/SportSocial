class UsersFilter extends Controller('socialApp.controllers')
    constructor: (
        $scope
        userService
        geoService
        base)->

        _this = this
        _this.loading = true
        _this.error = false

        _this.filter = $scope.filter

        getGeo = (entity, query)->
            geoService[entity](query: query, count: $scope.queryListLimit).then (res)->
                if res.data.length
                    return res.data
                else
                    base.notice.show
                        text: "Location #{query} is not found"
                        type: 'info'
                    return []

        _this.getCountry = (query)->
            getGeo 'getCountry', query

        _this.getCity = (query)->
            getGeo 'getCity', query

        # get filter prop
        # ---------------
        userService.getFilterProp().then (res)->
            _this.prop = res.data
        , ->
            _this.error = true
        .finally ->
            _this.loading = false