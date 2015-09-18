# CoffeeScript
class ProfileView extends Controller('socialApp.controllers')
    constructor: (
        $scope
        $state
        $stateParams
        $rootScope
        mixpanel
        profileService
        modalService)->

        $scope.$root.title = ['Fortress | ', $rootScope.user.fullName].join('')

        _this = this
        _this.unknown = false
        # user info model
        _this.user = {
            loaded: false
        }

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
            _this.user.journals.unshift res.data

        # social share record
        # ---------------
        _this.shareRecord = (obj)->
            modalService.show
                name: 'socialShare'
                data:
                    text: obj.text
                    media: obj.media
                    hashtags: obj.tags

        # remove item from
        # ---------------
        _this.remove = (id)->
            modalService.show
                name: 'journalRemove'
                data:
                    id: id
                    success: (res)->
                        for j, i in _this.user.journals
                            if j.id == id
                                _this.user.journals.splice i, 1
                                break


        # new avatar response
        # ---------------
        _this.avatarResponse = (stringRes)->
            objRes = angular.fromJson stringRes
            if obj.success
                _this.user.avatar = obj.data

        # remove avatar
        # ---------------
        _this.removeAvatar = ->
            profileService.removeAvatar().then (res)->
                _this.user.avatar = null

        # get user profile info
        # ---------------
        profileService.getInfo($stateParams.userId).then((res)->
            _this.user = res.data
            if $rootScope.user.id == +$stateParams.userId
                _this.user.isOwner = true
            else
                _this.user.isOwner = false
            _this.user.id = $stateParams.userId
            _this.user.loaded = true

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
        )