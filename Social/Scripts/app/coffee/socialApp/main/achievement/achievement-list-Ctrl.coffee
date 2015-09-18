class AchievementList extends Controller('socialApp.controllers')
    constructor: (
        $q
        $scope
        $state
        achievementService)->

        # ---------- COMMON ----------#
        $scope.$root.title = 'Fortress | Список заявок'

        _this = this
        _this.loader = false # todo true
        _this.pageError = false
        _this.showMoreLoading = false

        _this.filter = # filter object default
            status: 'all'       # fail, credit
            actual: 'opened'    # last
            count: 20           # default count load
            page: 3             # default page

        _this.prop = {} # filter settings object

        for k,v of $state.params
            if v != undefined
                _this.filter[k] = v

        console.log(_this.filter)
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
                achievementService.getList(filter).then (res)->
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
        achievementService.getFilterProp().then (res)->
            _this.prop = res.data
            _this.updateList(true)
        , ->
            _this.pageError = true

        # watch for toggles
        # ---------------
        $scope.$watch ->
            return _this.filter.actual
        , (newVal, oldVal)->
            if newVal != oldVal
                _this.updateList()

#        # fake filter prop
#        _this.prop =
#            types: ['Подтягивания', 'Отжимания']
#
#
#        #fake list
#        _this.showMore = true
#        _this.list = [{
#            id: 1
#            iconUrl: 'iconUrl1'
#            title: 'Подтягивания. 35 Повторений'
#            created: '06 сентября 2015'
#            timeSpent: '6' # в днях
#            voice: {
#                for: 142
#                against: 10
#            }
#            user: {
#                id: 1
#                avatar: 'avatarUrl'
#                fullName: 'Mikki Mouse'
#            }
#            status: 'fail'
#        }
#        {
#            id: 2
#            iconUrl: 'iconUrl1'
#            title: 'Подтягивания. 35 Повторений'
#            created: '06 сентября 2015'
#            timeSpent: '6' # в днях
#            voice: {
#                for: 142
#                against: 10
#            }
#            user: {
#                id: 1
#                avatar: 'avatarUrl'
#                fullName: 'Mikki Mouse'
#            }
#            status: 'credit'
#        }
#        {
#            id: 3
#            iconUrl: 'iconUrl1'
#            title: 'Подтягивания. 35 Повторений'
#            created: '06 сентября 2015'
#            timeSpent: '6' # в днях
#            voice: {
#                for: 142
#                against: 10
#            }
#            user: {
#                id: 1
#                avatar: 'avatarUrl'
#                fullName: 'Mikki Mouse'
#            }
#            status: null
#        }]
