class journalProfileSubmit extends Directive('socialApp.directives')
    constructor: ->
        return {
            restrict: 'AE'
            replace: true
            templateUrl: '/template/journal/submit'
            controller: 'journalProfileSubmitController'
            link: (scope, element, JournalCtrl)->
                console.log('journal submit directive')
        }