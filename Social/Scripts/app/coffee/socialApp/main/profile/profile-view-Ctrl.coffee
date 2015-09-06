# CoffeeScript
class ProfileView extends Controller('socialApp.controllers')
    constructor: (
        $scope
        $stateParams
        mixpanel
        profileService)->

        _this = this

        # user info model
        _this.user = {
            loaded: false
        }

        # get user profile info
        # ---------------
        profileService.getInfo($stateParams.userId).then((res)->
            _this.user = res.data
            _this.user.loaded = true
        )

        _this.user =
            avatar: '123123'
            fullName: 'Kaka Lakovich'
            age: 25
            sportTime: 21
            location: 'Заинск, Россия'
            isOwner: true
            loaded: true
            followers:
                list: [
                    {id: 1, fullName: 'Александр Русаков', avatar: '#'}
                    {id: 2, fullName: 'Александр Русаков', avatar: '#'}
                    {id: 3, fullName: 'Александр Русаков', avatar: '#'}
                ]
                count: 3
            subscribe:
                list: [
                    {id: 5, fullName: 'Александр Русаков', avatar: '#'}
                    {id: 6, fullName: 'Александр Русаков', avatar: '#'}
                    {id: 7, fullName: 'Александр Русаков', avatar: '#'}
                    {id: 8, fullName: 'Александр Русаков', avatar: '#'}
                ]
                count: 120