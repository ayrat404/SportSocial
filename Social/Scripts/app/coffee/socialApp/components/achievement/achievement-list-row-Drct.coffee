class achievementListRow extends Directive('socialApp.directives')
    constructor: ->
        return {
            restrict: 'E'
            replace: true
            scope:
                achievements: '=ngModel'
            templateUrl: '/template/components/achievements-list-rowTpl'
        }