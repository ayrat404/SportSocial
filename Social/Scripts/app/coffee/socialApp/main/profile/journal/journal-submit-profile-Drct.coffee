class journalProfileSubmit extends Directive('socialApp.directives')
    constructor: ->
        return {
            restrict: 'AE'
            replace: true
            templateUrl: '/template/journal/submit'
            scope:
                success: '&'
            controller: 'journalProfileSubmitController'
        }