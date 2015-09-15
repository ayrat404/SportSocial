class AchievementSubmit extends Controller('socialApp.controllers')
    constructor: (
        $scope
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
                _this.model.id = res.data.id
                _this.currentStep++
            .finally (res)->
                _this.loader = false

        # remove temp achievement
        # ---------------
        this.cancel = ->
            if this.model.id != -1
                achievementService.cancelTemp()
            $window.history.back()


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
            exampleLink: 'http://www.youtube.com/watch?v=zWc41BbjlZ4'
            isExampleShow: true
            customLink: ''
            isCustomLoaded: false
            hideExample: ->
                if _this.second.isExampleShow
                    _this.second.ePlayer.pauseVideo()
                _this.second.isExampleShow = !_this.second.isExampleShow
            getVideoInfo: (link)->
                youtubeVideoService.getVideoInfo(_this.second.customLink).then (res)->
                    _this.second.isCustomLoaded = true
                    _this.model.videoId = res.data.id
                    _this.second.isExampleShow = false
            removeVideo: ->
                _this.second.customLink = ''
                _this.model.videoId = ''
                _this.second.isCustomLoaded = false
        # ---------- Second Step ----------#


        # ---------- Third Step ----------#

        # ---------- Third Step ----------#


        # get data: cards & temp achievement
        # ---------------
        #        _this.loader = true
        #        achievementService.getCards().then (res)->
        #            _this.cards = res.data
        #            achievementService.getTemp().then (res)->
        #                _this.model = res.data
        #            , (res)->
        #                _this.model = defaultModel
        #            .finally ->
        #                _this.loader = false
        #                _this.currentStep = _this.model.step
        #        , (res)->
        #            _this.pageError = true
        #            _this.loader = false

        # fake data
        _this.cards = [
            { id: 1, img: 'imageUrl1', title: 'Подтягивания', values: [10, 20, 30, 40, 50, 60, 70, 80, 90, 100] },
            { id: 2, img: 'imageUrl2', title: 'Отжимания', values: [10, 20, 30, 40, 50, 60, 70, 80, 90, 100, 200, 300, 400] }
        ]
        _this.model = defaultModel
        _this.currentStep = _this.model.step