class RecordView extends Controller('socialApp.controllers')
    constructor: (
        $scope
        $stateParams
        $rootScope
        journalService
        modalService)->

        $scope.$root.title = 'Fortress | Запись в дневнике'

        _this = this
        _this.pageError = false
        _this.it =
            loader: true

        # get single record data
        # ---------------
        journalService.getById(+$stateParams.id).then (res)->
            _this.it = res.data
            if $rootScope.user.id == _this.it.author.id then _this.it.isOwner = true else _this.it.isOwner = false
            _this.it.comments.form = {}
        , (res)->
            _this.pageError = true
        .finally (res)->
            _this.it.loader = false


#            _this.it.comments =
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
#                        created: "19 июня 2015 | 15:08",
#                        commentFor: { id: 2, name: "Вася" }
#                    },
#                    {
#                        id: 2,
#                        text: 'wwwwww wwwwww' ,
#                        author: {id: 7, fullName: 'Вася', avatar: 'avatartest1'},
#                        likes: {
#                            list: [
#                                { id: 1, fullName: 'Владимир Владимирович', avatar: 'avatartest1' },
#                                { id: 2, fullName: 'Владимир Владимирович', avatar: 'avatartest1' },
#                                { id: 3, fullName: 'Владимир Владимирович', avatar: 'avatartest1' }
#                            ],
#                            count: 5,
#                            isLiked: false
#                        },
#                        created: "19 июня 2015 | 15:08"
#                    }
#                ]
#                count: 23

        # edit record
        # ---------------
        _this.edit = ->
            modalService.show
                name: 'journalSubmit'
                data:
                    model: _this.it
                    success: (record)->
                        _this.it = record

        # remove record
        # ---------------
        _this.remove = ->
            modalService.show
                name: 'journalRemove'
                data:
                    id: _this.it.id
                    success: (res)->
                        $state.go 'main.profile', { userId: $rootScope.user.id }

        # social sharing
        # ---------------
        _this.share = ->
            modalService.show
                name: 'socialShare'
                data:
                    text: _this.it.text
                    media: _this.it.media
                    hashtags: _this.it.tags

        #fake model


#        _this.it = {
#            isOwner: true
#            loader: false
#
#            id: 123
#            text: '123123123'
#            author: {
#                id: 12
#                avatar: 'avatar'
#                fullName: 'Павел Козловский'
#            }
#            date: '19 июля 2015 | 15:08'
#            likes: {
#                list: [
#                    { id: 1, fullName: 'Владимир Владимирович', avatar: 'avatartest1' },
#                    { id: 2, fullName: 'Владимир Владимирович', avatar: 'avatartest1' },
#                    { id: 3, fullName: 'Владимир Владимирович', avatar: 'avatartest1' },
#                    { id: 4, fullName: 'Владимир Владимирович', avatar: 'avatartest1' },
#                    { id: 5, fullName: 'Владимир Владимирович', avatar: 'avatartest1' }
#                ]
#                count: 23
#            }
#            media: [
#                { id: 1, type: 'image', img: 'srctest1' },
#                { id: 2, type: 'image', img: 'srctest2' },
#                { id: 3, type: 'video', img: 'srctest3' },
#                { id: 4, type: 'image', img: 'srctest4' }
#            ]
#            tags: ['Питание', 'Программа тренировок']
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
#        }

