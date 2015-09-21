class Tape extends Controller('socialApp.controllers')
    constructor: (
        $q
        $scope
        $state
        tapeService)->

        # ---------- COMMON ----------#
        $scope.$root.title = 'Fortress | Моя лента'

        _this = this
        _this.loader = false # todo true
        _this.pageError = false
        _this.showMoreLoading = false

        _this.filter =
            count: 20           # default count load
            page: 3             # default page

        for k,v of $state.params
            if v != undefined
                _this.filter[k] = v

        # set params in url
        # ---------------
        setUrl = ->
            $state.params = _this.filter
            $state.transitionTo($state.current, $state.params, { notify: false });

        # get list
        # ---------------
        getList = (filter)->
            setUrl()
            $q (resolve, reject)->
                tapeService.getList(filter).then (res)->
                    _this.showMore = res.data.isMore
                    resolve res.data.list
                , (res)->
                    _this.list = []
                    _this.showMore = false
                    reject res

        # first load
        # ---------------
        do->
            filter = angular.extend({}, _this.filter)
            filter.page = 1
            filter.count = _this.filter.page * _this.filter.count
            getList(filter).then (list)->
                _this.list = list
            , (res)->
                _this.pageError = true
            .finally ->
                _this.loader = false

        # show more
        # ---------------
        _this.loadMore = ->
            if !_this.showMoreLoading
                _this.showMoreLoading = true
                _this.filter.page = +_this.filter.page + 1
                getList(_this.filter).then (list)->
                    _this.list.push list
                .finally ->
                    _this.showMoreLoading = false
