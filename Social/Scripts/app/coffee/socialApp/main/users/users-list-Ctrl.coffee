class UsersList extends Controller('socialApp.controllers')
    constructor: (
        $q
        $scope
        $state
        userService)->

        # ---------- COMMON ----------#
        $scope.$root.title = 'Fortress | Список атлетов'

        _this = this
        _this.loader = false # todo true
        _this.pageError = false
        _this.showMoreLoading = false

#        _this.filter = # filter object default
#            status: 'all'       # fail, credit
#            actual: 'opened'    # last
#            count: 20           # default count load
#            page: 3             # default page

        #_this.prop = {} # filter settings object

#        for k,v of $state.params
#            if v != undefined
#                _this.filter[k] = v

        # set params in url
        # ---------------
#        setUrl = ->
#            $state.params = _this.filter
#            $state.transitionTo($state.current, $state.params, { notify: false });

        # get list
        # ---------------
        getList = (filter)->
            #setUrl()
            $q (resolve, reject)->
                userService.getList(filter).then (res)->
                    _this.showMore = res.data.isMore
                    resolve res.data.list
                , (res)->
                    _this.list = []
                    _this.showMore = false
                    reject res

        # full update list
        # ---------------
        _this.updateList = (isFirstLoad)->
            _this.loader = true
            filter = angular.extend({}, _this.filter)
            if isFirstLoad && filter.page > 1
                filter.page = 1
                filter.count = _this.filter.page * _this.filter.count
            getList(filter).then (list)->
                _this.list = list
            .finally ->
                _this.loader = false

        # show more
        # ---------------
        _this.loadMore = ->
            if !_this.showMoreLoading
                _this.showMoreLoading = true
                filter = angular.extend({}, _this.filter)
                filter.page = +filter.page + 1
                getList(filter).then (list)->
                    _this.list.push list
                    _this.filter.page = filter.page
                .finally ->
                    _this.showMoreLoading = false

        # first load
        # ---------------
        userService.getFilterProp().then (res)->
            _this.prop = res.data
            _this.updateList(true)
        , ->
            _this.pageError = true

        # watch for toggles
        # ---------------
#        $scope.$watch ->
#            return _this.filter.actual
#        , (newVal, oldVal)->
#            if newVal != oldVal
#                _this.updateList()

#        # fake filter prop
#        _this.prop =
#            types: ['Подтягивания', 'Отжимания']
#
#
        #fake list
#        _this.showMore = true
#        _this.list = [
#            {
#                id: 1
#                fullName: 'Вася Козлов'
#                avatar: 'avatarUrl'
#                age: 23
#                sportTime: 4
#                location: 'Владивосток, Россия'
#                achievementsCount: 10
#                recordsCount: 5
#                subscribers:
#                    count: 19
#                    isSubscribed: false
#                    list: [{id: 1, avatar: 'userAvatar'}]
#            }
#            {
#                id: 2
#                fullName: 'Вася Козлов'
#                avatar: 'avatarUrl'
#                age: 23
#                sportTime: 4
#                location: 'Владивосток, Россия'
#                achievementsCount: 10
#                recordsCount: 5
#                subscribers:
#                    count: 19
#                    isSubscribed: false
#                    list: []
#            }
#        ]
