class MediaModalShow extends Controller('socialApp.controllers')
    constructor: (
        $scope
        $state
        $modalInstance
        $rootScope
        base
        journalService
        modalService
        modalData)->

        receiveParams = ['media', 'entityType', 'index']

        $scope.maxText = 40

        if modalData.media &&
          modalData.entityType

            for i,v of receiveParams
                $state.params[v] = modalData[v]

            $scope.currentIndex = if modalData.index != undefined then +modalData.index else 1
            $scope.entityType = modalData.entityType

            # get single record data
            # ---------------
            journalService.getById(id: modalData.media).then((res)->
                $scope.it = res.data
                $scope.it.loader = false
                if $rootScope.user.id == $scope.it.author.id then $scope.it.isOwner = true else $scope.it.isOwner = false
                $scope.itemsCount = $scope.it.media.length
                if $scope.currentIndex > $scope.itemsCount
                    $scope.currentIndex = 1
                setByIndex $scope.currentIndex
            , (res)->
                $modalInstance.dismiss()
                base.notice.show
                    text: 'Record with id=' + modalData.media + ' is not defined.'
                    type: 'warning'
            )

            # set slide by index
            # ---------------
            setByIndex = (index)->
                i = index - 1
                if $scope.it.media[i] != undefined
                    $scope.current = $scope.it.media[i]
                $state.params.index = index
                $state.transitionTo($state.current, $state.params, { notify: false });

            # social sharing
            # ---------------
            $scope.socialShare = ->
                modalService.show
                    name: 'socialShare'
                    data:
                        url: $state.href('main.journalIt', {id: $scope.it.id}, {absolute: true})
                        text: $scope.it.text
                        media: $scope.current.url
                        hashtags: $scope.it.tags

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

            # keyup listener
            # ---------------
            doc = angular.element document
            keyListener = (event)->
                if event.which == 37
                    $scope.before()
                    event.preventDefault()
                else if event.which == 39
                    $scope.next()
                    event.preventDefault()

            # ---------------
            doc.on 'keydown', keyListener

            # ---------------
            $scope.$on '$destroy', ->
                doc.off 'keydown', keyListener

        else
            $modalInstance.dismiss()
            console.log 'media id or type undefined'

        # clear state
        # ---------------
        $modalInstance.result.catch ->
            for i,v of receiveParams
                $state.params[v] = null
            $state.transitionTo($state.current, $state.params, { notify: false });
