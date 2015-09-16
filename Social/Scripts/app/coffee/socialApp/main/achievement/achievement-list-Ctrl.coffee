class AchievementList extends Controller('socialApp.controllers')
    constructor: (
        $scope
        achievementService)->

        # ---------- COMMON ----------#
        $scope.$root.title = 'Fortress | Список заявок'

        _this = this
        _this.loader = true
        _this.pageError = false

        # get single achievement data
        # ---------------
        achievementService.getList().then (res)->
            _this.it = res.data
        , (res)->
            _this.pageError = true
        .finally (res)->
            _this.loader = false


        _this.list = [{
            id: 1
            iconUrl: 'iconUrl1'
            title: 'Подтягивания. 35 Повторений'
            created: '06 сентября 2015'
            timeSpent: '6' # в днях
            voice: {
                for: 142
                against: 10
            }
            user: {
                id: 1
                avatar: 'avatarUrl'
                fullName: 'Mikki Mouse'
            }
        }
        {
            id: 2
            iconUrl: 'iconUrl1'
            title: 'Подтягивания. 35 Повторений'
            created: '06 сентября 2015'
            timeSpent: '6' # в днях
            voice: {
                for: 142
                against: 10
            }
            user: {
                id: 1
                avatar: 'avatarUrl'
                fullName: 'Mikki Mouse'
            }
        }
        {
            id: 3
            iconUrl: 'iconUrl1'
            title: 'Подтягивания. 35 Повторений'
            created: '06 сентября 2015'
            timeSpent: '6' # в днях
            voice: {
                for: 142
                against: 10
            }
            user: {
                id: 1
                avatar: 'avatarUrl'
                fullName: 'Mikki Mouse'
            }
        }]

