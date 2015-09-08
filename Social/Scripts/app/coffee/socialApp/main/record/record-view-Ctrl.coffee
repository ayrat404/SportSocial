class RecordView extends Controller('socialApp.controllers')
    constructor: (
        $scope
        $stateParams
        $rootScope
        journalService
        mixpanel)->

        $scope.$root.title = 'Fortress | Запись в дневнике'

        _this = this
        _this.it =
            loader: true

        # get single record data
        # ---------------
        journalService.getById(+$stateParams.id).then((res)->
            _this.it = res.data
            _this.it.loader = false
            if $rootScope.user.id == _this.it.author.id
                _this.it.isOwner = true
            else
                _this.it.isOwner = false
        )

        # edit record
        # ---------------
        _this.edit = ->
            console.log('edit')

        # remove record
        # ---------------
        _this.remove = ->
            console.log('remove')

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
#        }

