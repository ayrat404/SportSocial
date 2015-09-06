# CoffeeScript
class MainSocial extends Controller('socialApp.controllers')
    constructor: (
        $rootScope
        userService)->


        # set user in $rootScope
        # ---------------
        #$rootScope.user = userService.get()

        #$state.go 'main.profile', userId: 666
        #$state.go 'landing'