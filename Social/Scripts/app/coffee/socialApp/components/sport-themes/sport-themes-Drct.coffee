class ThemesAutocomplete extends Directive('socialApp.directives')
    constructor: ->
        return {
            restrict: 'EA'
            require: 'ngModel'
            replace: true
            scope:
                themes: '=ngModel'
            controller: 'themesAutocompleteController'
            templateUrl: '/template/components/sport-themesTpl'
            link: (scope, element, attrs, ngModel)->

        }
