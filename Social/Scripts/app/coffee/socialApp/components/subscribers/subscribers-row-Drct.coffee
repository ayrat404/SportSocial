class SubscribersInRow extends Directive('socialApp.directives')
    constructor: ($rootScope)->
        return {
            restrict: 'E'
            require: 'ngModel'
            replace: true
            scope:
                subscribers: '=ngModel'
                id: '@'
                opts: '@'
            controller: 'subscribersInRowController'
            templateUrl: '/template/components/subscribers/subscribers-rowTpl'
            link: (scope, element, attrs, ngModel)->
                defaults =
                    rowCount: 4
                    showLink: true
                    imageSize: 50

                # todo override defaults
                scope.o = defaults
                scope.isOwner = +$rootScope.user.id == +scope.id
        }