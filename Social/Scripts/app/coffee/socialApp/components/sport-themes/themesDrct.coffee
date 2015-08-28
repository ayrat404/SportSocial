class ThemesAutocomplete extends Directive('socialApp.directives')
    constructor: (journalSubmitService)->

        return {
            restrict: 'EA'
            require: 'ngModel'
            replace: true
            scope:
                themes: '=ngModel'
            controller: 'themesAutocompleteController'
            templateUrl: '/Scripts/templates/components/sport-themesTpl.html'
            link: (scope, element, attrs, ngModel)->

        }
