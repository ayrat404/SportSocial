class AchievementSubmit extends Controller('socialApp.controllers')
    constructor: (
        $scope
        $state
        $stateParams
        $rootScope
        $window
        achievementService
        youtubeVideoService)->

        # ---------- COMMON ----------#
        $scope.$root.title = 'Fortress | Заявка на награду'

        prop =
            stepsLength: 3

        defaultModel =
            id: -1
            step: 0
            video: {}

        _this = this


        # prev step
        # ---------------
        this.prevStep = ->
            _this.currentStep--

        # next step
        # ---------------
        this.nextStep = ->
            _this.loader = true
            achievementService.saveTemp(_this.model).then (res)->
                if res.data && res.data.isPublished
                    $state.go 'main.achievementView', id: res.data.id
                else
                    _this.model.id = 1
                    _this.currentStep += 1
            .finally (res)->
                _this.loader = false

        # remove temp achievement
        # ---------------
        this.cancel = ->
            if this.model.id != -1
                achievementService.cancelTemp()
            $state.go 'main.achievementList'


        # ---------- COMMON ----------#


        # ---------- First Step ----------#

        # check first step validation
        # ---------------
        this.checkFirstStep = ->
            result = false
            for i in [0..._this.cards.length]
                if _this.cards[i].focus &&
                  _this.cards[i].selected
                    result = true
                    _this.model.type =
                        id: _this.cards[i].id
                        value: _this.cards[i].selected
                    _this.second.exampleLink = _this.cards[i].videoUrl
                    break
            _this.firstValid = result

        # focus on card
        # ---------------
        this.cardFocus = (card)->
            for i in [0..._this.cards.length]
                if _this.cards[i].id == card.id
                    _this.cards[i].focus = true
                else
                    _this.cards[i].focus = false
            _this.checkFirstStep()

        # ---------- First Step ----------#


        # ---------- Second Step ----------#
        this.second =
            #exampleLink: 'http://www.youtube.com/watch?v=zWc41BbjlZ4'
            isExampleShow: true
            customLink: ''
            hideExample: ->
                if _this.second.isExampleShow
                    _this.second.ePlayer.pauseVideo()
                _this.second.isExampleShow = !_this.second.isExampleShow
            getVideoInfo: ->
                youtubeVideoService.getVideoInfo(link: _this.model.video.remoteUrl, type: 'achievement').then (res)->
                    _this.model.video.id = res.data.id
                    _this.second.isExampleShow = false
                , (res)->
                    _this.model.video.id = null
            removeVideo: ->
                _this.second.customLink = ''
                _this.model.videoId = ''
        # ---------- Second Step ----------#


        # ---------- Third Step ----------#

        # ---------- Third Step ----------#


        # get data: cards & temp achievement
        # ---------------
        _this.loader = true
        achievementService.getTemp().then (res)->
            _this.cards = res.data.cards
            _this.marks = res.data.marks
            if res.data.model != null
                # on model receive
                _this.model = res.data.model
                for i in [0..._this.cards.length]
                    if _this.model.type.id == _this.cards[i].id
                        _this.cards[i].focus = true
                        _this.cards[i].selected = _this.model.type.value
                        _this.second.exampleLink = _this.cards[i].videoUrl
                        break
            else
                # on empty model
                _this.model = defaultModel
            _this.currentStep = _this.model.step
            if _this.currentStep >=1 then _this.second.isExampleShow = false
            _this.checkFirstStep()
        , (res)->
            _this.pageError = true
        .finally ->
            _this.loader = false

        # fake data
#        fake =
#            cards: [
#                { id: 1, img: 'imageUrl1', title: 'Подтягивания', values: [10, 20, 30, 40, 50, 60, 70, 80, 90, 100] },
#                { id: 2, img: 'imageUrl2', title: 'Отжимания', values: [10, 20, 30, 40, 50, 60, 70, 80, 90, 100, 200, 300, 400] }
#            ]
#            model: {
##                id: 1
##                step: 2 # 0, 1, 2
##                type: {
##                    id: 2,
##                    value: 60
##                }
##                video: {
##                    id: 12
##                    remoteUrl: 'http://www.youtube.com/watch?v=zWc41BbjlZ4'
##                }
#            }
#            marks: [
#                {
#                    id: 1
#                    iconUrl: 'iconUrl1'
#                    title: 'Подтягивания. 35 Повторений'
#                    created: '06 сентября 2015'
#                    timeSpent: '6' # в днях
#                    voice: {
#                        for: 142
#                        against: 10
#                    }
#                    user: {
#                        id: 1
#                        avatar: 'avatarUrl'
#                        fullName: 'Mikki Mouse'
#                    }
#                }
#                {
#                    id: 2
#                    iconUrl: 'iconUrl1'
#                    title: 'Подтягивания. 35 Повторений'
#                    created: '06 сентября 2015'
#                    timeSpent: '6' # в днях
#                    voice: {
#                        for: 142
#                        against: 10
#                    }
#                    user: {
#                        id: 1
#                        avatar: 'avatarUrl'
#                        fullName: 'Mikki Mouse'
#                    }
#                }
#                {
#                    id: 3
#                    iconUrl: 'iconUrl1'
#                    title: 'Подтягивания. 35 Повторений'
#                    created: '06 сентября 2015'
#                    timeSpent: '6' # в днях
#                    voice: {
#                        for: 142
#                        against: 10
#                    }
#                    user: {
#                        id: 1
#                        avatar: 'avatarUrl'
#                        fullName: 'Mikki Mouse'
#                    }
#                }
#            ]
#
#        _this.cards = fake.cards
#        _this.marks = fake.marks
#
#        # on model receive
##        _this.model = fake.model
##        for i in [0..._this.cards.length]
##            if _this.model.type.id == _this.cards[i].id
##                _this.cards[i].focus = true
##                _this.cards[i].selected = _this.model.type.value
##                break
#
#        # on empty model
#        _this.model = defaultModel
#
#        # always
#        _this.currentStep = _this.model.step
#        if _this.currentStep >=1 then _this.second.isExampleShow = false

