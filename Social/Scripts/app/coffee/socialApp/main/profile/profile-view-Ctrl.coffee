# CoffeeScript
class ProfileView extends Controller('socialApp.controllers')
    constructor: (
        $scope
        $state
        $stateParams
        $rootScope
        mixpanel
        profileService
        modalService
        defaultAvatarUrl
        subscribeService)->

        $scope.$root.title = "Fortress | #{$rootScope.user.fullName}"

        # mixpanel tracking
        # ---------------
        $scope.$on('$viewContentLoaded', ->
            mixpanel.ev.visitPage($scope.$root.title))

        _this = this
        _this.loading = true
        _this.unknown = false
        _this.user =
            loaded: false

        loadProp =
            count: 20           # default count load
            page: 3             # default page

        # complain on user
        # ---------------
        _this.complain = (title)->
            modalService.show
                name: 'complainSubmit'
                data:
                    userId: $rootScope.user.id
                    entityId: _this.user.id
                    type: 'profile'
                    title: title

        # append new record
        # ---------------
        _this.newRecord = (res)->
            _this.user.journal.list.unshift res.data

        # social share record
        # ---------------
        # todo sharing directive
        _this.shareRecord = (obj)->
            modalService.show
                name: 'socialShare'
                data:
                    url: $state.href('main.journalIt', {id: obj.id}, {absolute: true})
                    text: obj.text
                    media: obj.media
                    hashtags: obj.tags

        # remove journal record
        # ---------------
        _this.remove = (id)->
            modalService.show
                name: 'journalRemove'
                data:
                    id: id
                    success: (res)->
                        for j, i in _this.user.journal.list
                            if j.id == id
                                _this.user.journal.list.splice i, 1
                                break

        # edit journal record
        # ---------------
        _this.edit = (model)->
            modalService.show
                name: 'journalSubmit'
                data:
                    model: model
                    success: (record)->
                        for j, i in _this.user.journal.list
                            if j.id == id
                                _this.user.journal.list[i] = record
                                break

        _this.loadMoreRecords = ->
            if !_this.user.journal.loading
                _this.user.journal.loading = true
                loadProp.page += 1
                $state.params = loadProp
                $state.transitionTo($state.current, $state.params, { notify: false });
                getList(loadProp).then (list)->
                    _this.list = _this.list.concat list
                .finally ->
                    _this.user.journal.loading = false


        # new avatar response todo refactor create method
        # ---------------
        _this.avatarResponse = (stringRes)->
            objRes = angular.fromJson stringRes
            if objRes.success
                $rootScope.$emit 'changeAvatar', objRes.data.url
                $rootScope.user.avatar = objRes.data.url
                _this.user.avatar = objRes.data.url

        # remove avatar todo refactor create method
        # ---------------
        _this.removeAvatar = ($flow)->
            profileService.removeAvatar().then (res)->
                $flow.cancel()
                $rootScope.$emit 'changeAvatar', defaultAvatarUrl
                $rootScope.user.avatar = defaultAvatarUrl
                _this.user.avatar = defaultAvatarUrl

        # user subscribe
        # ---------------
        _this.subscribe = ()->
            subscribeService.set(id: _this.user.id, current: _this.user.isSubscribed).then (newStatus)->
                _this.user.isSubscribed = newStatus

        # get user profile info
        # ---------------
        profileService.getInfo(id: $stateParams.userId).then (res)->
            _this.user = res.data
            if $rootScope.user.id == +$stateParams.userId
                _this.user.isOwner = true
            else
                _this.user.isOwner = false
            _this.user.id = $stateParams.userId
            _this.user.loaded = true
            loadProp.authorId = _this.user.id
            #_this.user.subscribe.list = [{id: 1, fullName: 'asdasdasdsada asd as dasassd'}]
        , (res)->
            _this.unknown = true
        .finally (res)->
            _this.loading = false