class AchievementView extends Controller('socialApp.controllers')
    constructor: (
        $scope
        $stateParams
        $rootScope
        $window
        achievementService)->

        # ---------- COMMON ----------#
        $scope.$root.title = 'Fortress | Просмотр заявки на награду'

        _this = this
        _this.loader = true
        _this.pageError = false

        # get single achievement data
        # ---------------
        achievementService.getById(id: +$stateParams.id).then (res)->
            _this.it = res.data
            _this.it.comments.form = {}
            if $rootScope.user.id == _this.it.author.id
                _this.it.isOwner = true
            else
                _this.it.isOwner = false
            calcBars()
        , (res)->
            _this.pageError = true
        .finally (res)->
            _this.loader = false

        # remove achievement
        # ---------------
        _this.remove = ->
            modalService.show
                name: 'achievementRemove'
                data:
                    id: _this.it.id
                    success: ->
                        $state.go 'main.achievementList'

        # social sharing
        # ---------------
        _this.share = ->
            modalService.show
                name: 'socialShare'
                data:
                    url: $state.href('main.achievementView', {id: _this.it.id}, {absolute: true})
                    text: _this.it.title
                    media: _this.it.cupImage

        # calc bar func
        # ---------------
        k = 0
        calcBars = ->
            k = 100 / (_this.it.voice.for + _this.it.voice.against)
            _this.forBarWidth = Math.round(_this.it.voice.for * k) + '%'
            _this.againstBarWidth = Math.round(_this.it.voice.against * k) + '%'

        # voice
        # ---------------
        _this.voice = (action)->
            #if !_this.it.voice.isVoited && _this.it.author.id != $rootScope.user.id
                achievementService.voice(id: _this.it.id, action: action).then (res)->
                    debugger
                    _this.it.voice.for = res.data.for
                    _this.it.voice.against = res.data.against
                    _this.it.voice.isVoited = true
                    calcBars()


#        _this.it =
#            isOwner: true
#
#            id: 12
#            title: 'Подтягивания. 35 Повторений'
#            typeImage: 'typeImageUrl' # картинка кубка с упражнением
#            author: {
#                id: 12
#                avatar: 'avatarImageUrl'
#                fullName: 'Павел Козловский'
#            }
#            created: 'date'
#            timestamp: '12312123' #осталось времени timestamp
#            likes: {
#                list: [
#                    { id: 1, fullName: 'Владимир Владимирович', avatar: 'avatartest1' },
#                    { id: 2, fullName: 'Владимир Владимирович', avatar: 'avatartest1' },
#                    { id: 3, fullName: 'Владимир Владимирович', avatar: 'avatartest1' },
#                    { id: 4, fullName: 'Владимир Владимирович', avatar: 'avatartest1' },
#                    { id: 5, fullName: 'Владимир Владимирович', avatar: 'avatartest1' }
#                ]
#                count: 23
#                isLiked: false
#            }
#            voice:
#                for: 152
#                against: 9
#                isVoited: false
#            comments: {
#                list: [
#                    {
#                        id: 1,
#                        text: 'wwwwww wwwwww' ,
#                        author: {id: 5, fullName: 'Владимир Владимирович', avatar: 'avatartest1'},
#                        likes: {
#                            list: [
#                                { id: 1, fullName: 'Владимир Владимирович', avatar: 'avatartest1' },
#                                { id: 2, fullName: 'Владимир Владимирович', avatar: 'avatartest1' },
#                                { id: 3, fullName: 'Владимир Владимирович', avatar: 'avatartest1' }
#                            ],
#                            count: 5,
#                            isLiked: false
#                        },
#                        date: "19 июня 2015 | 15:08",
#                        commentFor: { id: 2, name: "Вася" }
#                    },
#                    {
#                        id: 2,
#                        text: 'wwwwww wwwwww' ,
#                        author: {id: 5, fullName: 'Вася', avatar: 'avatartest1'},
#                        likes: {
#                            list: [
#                                { id: 1, fullName: 'Владимир Владимирович', avatar: 'avatartest1' },
#                                { id: 2, fullName: 'Владимир Владимирович', avatar: 'avatartest1' },
#                                { id: 3, fullName: 'Владимир Владимирович', avatar: 'avatartest1' }
#                            ],
#                            count: 5,
#                            isLiked: false
#                        },
#                        date: "19 июня 2015 | 15:08"
#                    }
#                ]
#                count: 23
#            }
