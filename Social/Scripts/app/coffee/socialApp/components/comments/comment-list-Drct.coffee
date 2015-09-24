class commentList extends Directive('socialApp.directives')
    constructor: ->
        return {
            restrict: 'E'
            replace: true
            scope:
                comments: '=ngModel'
                id: '@'
                entityType: '@'
            controller: 'commentListController'
            templateUrl: '/template/components/comments/comment-listTpl'
            link: (scope, element, attrs, ngModel)->

        }