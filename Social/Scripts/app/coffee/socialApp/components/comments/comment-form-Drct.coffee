class commentForm extends Directive('socialApp.directives')
    constructor: ->
        return {
            restrict: 'E'
            replace: true
            scope:
                comments: '=ngModel'
                id: '@'
                entityType: '@'
            controller: 'commentFormController'
            templateUrl: '/template/components/comments/comment-formTpl'
            link: (scope, element, attrs, ngModel)->
        }