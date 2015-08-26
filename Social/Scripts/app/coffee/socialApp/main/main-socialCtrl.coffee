# CoffeeScript
class MainSocial extends Controller('socialApp.controllers')
    constructor: ($state)->
        $state.go 'landing', userId: 666