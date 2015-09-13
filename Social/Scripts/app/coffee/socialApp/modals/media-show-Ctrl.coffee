class MediaModalShow extends Controller('socialApp.controllers')
    constructor: (
        $scope
        $state
        $modalInstance
        $rootScope
        journalService
        modalData)->

        $scope.maxText = 40
        if modalData.media != undefined

            $state.params.media = modalData.media
            $scope.currentIndex = if modalData.index != undefined then +modalData.index else 1

            # get single record data
            # ---------------
            journalService.getById(modalData.media).then((res)->
                $scope.it = res.data
                $scope.it.loader = false
                if $rootScope.user.id == $scope.it.author.id then $scope.it.isOwner = true else $scope.it.isOwner = false
                $scope.itemsCount = $scope.it.media.length
                setByIndex $scope.currentIndex
            , (res)->
                $modalInstance.dismiss()
            )

            #fake model
#            $scope.it = {
#                isOwner: true
#                loader: false
#
#                id: 123
#                text: '1231231asdasddasdasd asd asd as dasasdsdasdasdasd asd asd  asd asdasdas dasdas das das 23'
#                author: {
#                    id: 12
#                    avatar: 'avatar'
#                    fullName: 'Павел Козловский'
#                }
#                date: '19 июля 2015 | 15:08'
#                likes: {
#                    list: [
#                        { id: 1, fullName: 'Владимир Владимирович', avatar: 'avatartest1' },
#                        { id: 2, fullName: 'Владимир Владимирович', avatar: 'avatartest1' },
#                        { id: 3, fullName: 'Владимир Владимирович', avatar: 'avatartest1' },
#                        { id: 4, fullName: 'Владимир Владимирович', avatar: 'avatartest1' },
#                        { id: 5, fullName: 'Владимир Владимирович', avatar: 'avatartest1' }
#                    ]
#                    count: 23
#                }
#                media: [
#                    {
#                        id: 1,
#                        type: 'image',
#                        img: 'srctest1',
#                        likes: {
#                            list: [
#                                { id: 1, fullName: 'Владимир Владимирович', avatar: 'avatartest1' },
#                                { id: 2, fullName: 'Владимир Владимирович', avatar: 'avatartest1' },
#                                { id: 3, fullName: 'Владимир Владимирович', avatar: 'avatartest1' },
#                                { id: 4, fullName: 'Владимир Владимирович', avatar: 'avatartest1' },
#                                { id: 5, fullName: 'Владимир Владимирович', avatar: 'avatartest1' }
#                            ]
#                            count: 23
#                        }
#                    },
#                    {
#                        id: 2,
#                        type: 'image',
#                        img: 'srctest2',
#                        likes: {
#                            list: [
#                                { id: 1, fullName: 'Владимир Владимирович', avatar: 'avatartest1' },
#                                { id: 2, fullName: 'Владимир Владимирович', avatar: 'avatartest1' },
#                                { id: 3, fullName: 'Владимир Владимирович', avatar: 'avatartest1' },
#                                { id: 4, fullName: 'Владимир Владимирович', avatar: 'avatartest1' },
#                                { id: 5, fullName: 'Владимир Владимирович', avatar: 'avatartest1' }
#                            ]
#                            count: 23
#                        }
#                    },
#                    {
#                        id: 3,
#                        type: 'video',
#                        img: 'srctest3',
#                        likes: {
#                            list: [
#                                { id: 1, fullName: 'Владимир Владимирович', avatar: 'avatartest1' },
#                                { id: 2, fullName: 'Владимир Владимирович', avatar: 'avatartest1' },
#                                { id: 3, fullName: 'Владимир Владимирович', avatar: 'avatartest1' },
#                                { id: 4, fullName: 'Владимир Владимирович', avatar: 'avatartest1' },
#                                { id: 5, fullName: 'Владимир Владимирович', avatar: 'avatartest1' }
#                            ]
#                            count: 23
#                        }
#                    },
#                    {
#                        id: 4,
#                        type: 'image',
#                        img: 'srctest4',
#                        likes: {
#                            list: [
#                                { id: 1, fullName: 'Владимир Владимирович', avatar: 'avatartest1' },
#                                { id: 2, fullName: 'Владимир Владимирович', avatar: 'avatartest1' },
#                                { id: 3, fullName: 'Владимир Владимирович', avatar: 'avatartest1' },
#                                { id: 4, fullName: 'Владимир Владимирович', avatar: 'avatartest1' },
#                                { id: 5, fullName: 'Владимир Владимирович', avatar: 'avatartest1' }
#                            ]
#                            count: 23
#                        }
#                    }
#                ]
#                tags: ['Питание', 'Программа тренировок']
#            }



            # set slide by index
            # ---------------
            setByIndex = (index)->
                i = index - 1
                if $scope.it.media[i] != undefined
                    $scope.current = $scope.it.media[i]
                $state.params.index = index
                $state.transitionTo($state.current, $state.params, { notify: false });

            # before media item
            # ---------------
            $scope.before = ->
                if $scope.currentIndex == 1
                    $scope.currentIndex = $scope.itemsCount
                else
                    --$scope.currentIndex
                setByIndex $scope.currentIndex


            # next media item
            # ---------------
            $scope.next = ->
                if $scope.currentIndex == $scope.itemsCount
                    $scope.currentIndex = 1
                else
                    ++$scope.currentIndex
                setByIndex $scope.currentIndex
        else
            console.log 'media id undefined'

        # clear state
        # ---------------
        $modalInstance.result.catch ->
            $state.params.index = null
            $state.params.media = null
            $state.transitionTo($state.current, $state.params, { notify: false });
