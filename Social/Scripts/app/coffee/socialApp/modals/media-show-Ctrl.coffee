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

        $scope.maxText = 40
        debugger
        if modalData.media &&
          modalData.entityType

            $state.params.media = modalData.media
            $scope.currentIndex = if modalData.index != undefined then +modalData.index else 1
            $scope.entityType = modalData.entityType

            # get single record data
            # ---------------
            journalService.getById(modalData.media).then((res)->
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

            # before media item
            # ---------------
            $scope.before = ->
                if $scope.currentIndex == 1
                    $scope.currentIndex = $scope.itemsCount
                else
                    --$scope.currentIndex
                setByIndex $scope.currentIndex

            # social sharing
            # ---------------
            $scope.socialShare = ->
                modalService.show
                    name: 'socialShare'
                    data:
                        text: $scope.it.text
                        media: $scope.current.url
                        hashtags: $scope.it.tags

            # next media item
            # ---------------
            $scope.next = ->
                if $scope.currentIndex == $scope.itemsCount
                    $scope.currentIndex = 1
                else
                    ++$scope.currentIndex
                setByIndex $scope.currentIndex
        else
            $modalInstance.dismiss()
            console.log 'media id or type undefined'

        # clear state
        # ---------------
        $modalInstance.result.catch ->
            $state.params.index = null
            $state.params.media = null
            $state.transitionTo($state.current, $state.params, { notify: false });
