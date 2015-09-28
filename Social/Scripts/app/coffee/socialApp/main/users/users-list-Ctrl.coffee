class UsersList extends Controller('socialApp.controllers')
    constructor: (
        $q
        $scope
        $state
        userService)->

        $scope.$root.title = 'Fortress | Список атлетов'

        _this = this
        _this.loader = true
        _this.pageError = false
        _this.showMoreLoading = false

        # get filter params from url
        # ---------------
        _this.filter = {}
        for k,v of $state.params
            if v != undefined
                _this.filter[k] = v

        # set url params
        # ---------------
        setUrl = (params)->
            $state.params = params
            $state.transitionTo $state.current, $state.params, notify: false

        # get list func
        # ---------------
        getList = (filter)->
            setUrl filter
            $q (resolve, reject)->
                userService.getList(filter).then (res)->
                    _this.showMore = res.data.isMore
                    resolve res.data.list
                , (res)->
                    _this.list = []
                    _this.showMore = false
                    reject res

        # update users list
        # ---------------
        _this.updateList = ->
            _this.loader = true
            filter = angular.extend {}, _this.filter
            filter.count = filter.page * filter.count
            filter.page = 1
            getList(filter).then (list)->
                _this.list = list
            .finally ->
                _this.loader = false

        # load more users
        # ---------------
        _this.loadMore = ->
            if !_this.showMoreLoading
                _this.showMoreLoading = true
                filter = angular.extend {}, _this.filter
                filter.page = +filter.page + 1
                getList(filter).then (list)->
                    _this.list.push list
                    _this.filter.page = filter.page
                .finally ->
                    _this.showMoreLoading = false

        # first load
        # ---------------
        _this.updateList()