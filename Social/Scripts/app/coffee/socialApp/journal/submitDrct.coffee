class journalSubmit extends Directive('socialApp.directives')
    constructor: ->
        return {
            restrict: 'AE'
            replace: true
            templateUrl: '/Scripts/templates/journal/submit.html'
            controller: 'journalSubmitController'
            link: (scope, element, JournalCtrl)->
                console.log('journal submit directive')
        }