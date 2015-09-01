class GlobalLoader extends Service('shared')
    constructor: ($rootScope)->

        states = []

        add = (stateName)->
            if states.indexOf stateName == -1
                states.push stateName
            $rootScope.loader = true if $rootScope.loader != true


        remove = (stateName)->
            index = states.indexOf stateName
            if index != -1
                states.splice index, 1
                $rootScope.loader = false if !states.length

        return {
            add: add
            remove: remove
        }