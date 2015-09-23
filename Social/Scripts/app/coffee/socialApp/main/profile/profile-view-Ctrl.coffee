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
        defaultAvatarUrl)->

        $scope.$root.title = ['Fortress | ', $rootScope.user.fullName].join('')

        _this = this
        _this.unknown = false
        _this.user =
            loaded: false

        recordsFilter =
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
                recordsFilter.page = +recordsFilter.page + 1
                $state.params = recordsFilter
                $state.transitionTo($state.current, $state.params, { notify: false });
                getList(recordsFilter).then (list)->
                    _this.list.push list
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
            recordsFilter.authorId = _this.user.id

#            _this.user.achievements =
#                closed:
#                    count: 10
#                    list: [
#                        {id: 1, cupImage: 'asdasd'}
#                        {id: 2, cupImage: 'asdasd'}
#                        {id: 3, cupImage: 'asdasd'}
#                        {id: 4, cupImage: 'asdasd'}
#                    ]
#                opened:
#                    count: 10
#                    list: [
#                        {id: 1, cupImage: 'asdasd'}
#                        {id: 2, cupImage: 'asdasd'}
#                        {id: 3, cupImage: 'asdasd'}
#                        {id: 4, cupImage: 'asdasd'}
#                    ]

#            avatar =
#                id: 2
#                url: 'asdasd'
#            _this.user.avatar = avatar

            # profile fake model
#            _this.user.media = [
#              { recordId: 1, index: 1, url: 'asdasd1', type: 'image' }
#              { recordId: 2, index: 2, url: 'asdasd2', type: 'image' }
#              { recordId: 3, index: 3, url: 'asdasd3', type: 'video' }
#              { recordId: 4, index: 4, url: 'asdasd4', type: 'image' }
#              { recordId: 5, index: 5, url: 'asdasd5', type: 'image' }
#              { recordId: 6, index: 6, url: 'asdasd6', type: 'video' }
#              { recordId: 7, index: 7, url: 'asdasd7', type: 'video' }
#              { recordId: 8, index: 8, url: 'asdasd8', type: 'image' }
#              { recordId: 9, index: 9, url: 'asdasd9', type: 'image' }
#            ]
        , (res)->
            _this.unknown = true