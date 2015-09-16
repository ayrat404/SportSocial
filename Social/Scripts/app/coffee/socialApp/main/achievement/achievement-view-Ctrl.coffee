class AchievementView extends Controller('socialApp.controllers')
    constructor: (
        $scope
        $stateParams
        $rootScope
        $window
        achievementService)->

        # ---------- COMMON ----------#
        $scope.$root.title = 'Fortress | Заявка на награду'

        _this = this
        _this.loader = false

        # get single achievement data
        # ---------------
        achievementService.getById(+$stateParams.id).then (res)->
            _this.it = res.data
            _this.it.comments.form = {}
            if $rootScope.user.id == _this.it.author.id
                _this.it.isOwner = true
            else
                _this.it.isOwner = false

        # remove achievement
        # ---------------
        _this.remove = ->
            modalService.show
                name: 'achievementRemove'
                data:
                    id: _this.it.id
                    success: (res)->
                        # todo redirect achievement list page
                        $state.go 'main.profile', { userId: $rootScope.user.id }

        # social sharing
        # ---------------
        _this.share = ->
            modalService.show
                name: 'socialShare'
                data:
                    text: _this.it.text # todo achievement title share
                    media: _this.it.media # todo achievement type image share