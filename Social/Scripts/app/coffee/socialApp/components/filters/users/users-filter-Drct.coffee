class usersFilter extends Directive('socialApp.directives')
    constructor: ($timeout)->
        return {
            restrict: 'E'
            scope:
                filter: '='
                callback: '&'
                queryListLimit: '@'
            controller: 'usersFilterController'
            controllerAs: 'uFilter'
            templateUrl: '/template/components/filters/users-filterTpl'
            link: (scope, element, attrs, ctrl)->
        }