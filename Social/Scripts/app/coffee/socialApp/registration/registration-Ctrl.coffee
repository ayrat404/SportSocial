class Registration extends Controller('socialApp.controllers')
    constructor: (
        $scope
        $rootScope
        $state
        mixpanel
        registrationService
        userService
        base)->
        $scope.$root.title = 'Fortress | Регистрация'
        $scope.loading = false
        $scope.first = {}                   #first step data model
        $scope.firstStepValidation = {}     # first step validation server object
        $scope.twoStepValidation = {}       # two step validation server object
        $scope.two = {}                     # two step data model
        $scope.step = 1                     # active step

        # datepicker options
        # ---------------
        $scope.datepickerOptions =
            maxDate: Date.now()
            initDate: new Date(1992, 0, 1)

        # send first data
        # ---------------
        $scope.sendImg = ($flow)->
            $flow.upload()

        # send all data after image server upload
        # ---------------
        $scope.sendFirstStepData = (stringResponse)->
            obj = angular.fromJson stringResponse
            if obj.success
                $scope.first.imgId = obj.data.id
                registrationService.registerFirst($scope.first).then((res)->
                    $scope.step = 2
                , (res)->
                    $scope.firstValidation = res.errors
                )

        # send tow step data
        # ---------------
        $scope.sendTwo = ->
            fullData = angular.extend($scope.first, $scope.two)
            registrationService.registerTwo(fullData).then((res)->
                userService.set res.data.id
                $rootScope.user = res.data
                $state.go('main.profile', {userId: res.data.id})
            , (res)->
                $scope.twoStepValidation = res.errors
            )

        # process image
        # ---------------
        $scope.flowObj = {}
        $scope.imgProp =
            vertical: false
            sizeError: false
            style: {}
        $scope.processImage = (files)->
            $scope.imgProp.vertical = false
            $scope.imgProp.sizeError = false
            angular.forEach files, (flow)->
                fileReader = new FileReader()
                image = new Image()
                fileReader.onload = (event)->
                    uri = event.target.result
                    image.src = uri
                    image.onload = ->
                        $scope.$apply ->
                            if image.width < 200 || image.height < 20
                                $scope.flowObj.flow.cancel()
                                $scope.imgProp.sizeError = true
                            else
                                if image.height > image.width
                                    $scope.imgProp.vertical = true
                                    $scope.imgProp.style.width = 186
                                    $scope.imgProp.style.height = Math.round(image.height / (image.width / 186))
                                    $scope.imgProp.style.margin = (98 - $scope.imgProp.style.height / 2) + 'px 0 0 0'
                                else
                                    $scope.imgProp.style.height = 186
                                    $scope.imgProp.style.width = Math.round(image.width / (image.height / 186))
                                    $scope.imgProp.style.margin = '0 0 0 ' + (98 - $scope.imgProp.style.width / 2) + 'px'
                fileReader.readAsDataURL(flow.file)


        # mixpanel tracking
        # ---------------
        $scope.$on('$viewContentLoaded', ->
            mixpanel.ev.visitPage($scope.$root.title))