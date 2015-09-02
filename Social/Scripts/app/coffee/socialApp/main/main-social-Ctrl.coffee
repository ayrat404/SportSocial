# CoffeeScript
class MainSocial extends Controller('socialApp.controllers')
    constructor: (
        $state
        $rootScope
        userService)->

        # todo get user data & add in $rootScope ({ id: xxx, avatar: xxx, fullName })
        #$state.go 'main.profile', userId: 666
        #$state.go 'landing'