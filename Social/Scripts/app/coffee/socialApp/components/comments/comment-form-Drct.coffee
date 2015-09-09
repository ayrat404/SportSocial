class commentForm extends Directive('socialApp.controllers')
    constructor: (commentsService)->
        return {
            restrict: 'E'
            replace: true
            scope:
                comments: '=ngModel'
                id: '@'
                entityType: '@'
            controller: ($scope)->

                if $scope.comments.form == undefined ||
                  $scope.comments.form == null
                    $scope.comments.form = {}

                # close comment form
                # ---------------
                closeForm = ->
                    $scope.open = false
                    takeWatcher()

                # clear comment form
                # ---------------
                clearForm = ->
                    $scope.comments.form.text = ''

                # take watcher form
                # ---------------
                takeWatcher = ->
                    textWatcher = $scope.$watch 'comments.form.text', (oldVal, newVal)->
                        if newVal && newVal.length > 3 && oldVal != newVal
                            $scope.open = true
                            textWatcher()

                takeWatcher()

                # submit comment
                # ---------------
                $scope.submit = ()->
                    commentsService.submit(text: $scope.comments.form.text, entityType: $scope.entityType, id: $scope.id).then (res)->
                        $scope.comments.list.unshift res.data
                        clearForm()
                        closeForm()

                # cancel form
                # ---------------
                $scope.cancel = ->
                    clearForm()
                    closeForm()

            #controller: 'commentFormController'
            templateUrl: '/template/components/comments/comment-formTpl'
            link: (scope, element, attrs, ngModel)->

        }