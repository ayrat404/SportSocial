# CoffeeScript
class MainSocial extends Controller('socialApp.controllers')
    constructor: ($state)->
        #$state.go 'main.profile', userId: 666
        #$state.go 'landing'