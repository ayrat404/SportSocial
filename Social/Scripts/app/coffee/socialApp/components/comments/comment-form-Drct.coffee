class commentForm extends Directive('socialApp.directives')
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

                # close & clear comment form
                # ---------------
                closeForm = ->
                    $scope.open = false
                    $scope.comments.form.text = ''
                    takeWatcher()

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
                    data =
                        entityType: $scope.entityType
                        entityId: $scope.id
                    if $scope.comments.form.isAnswer
                        data.commentType = 'answer'
                        data.commentForId = $scope.comments.commentModel.id
                        data.text = $scope.comments.form.text.substr($scope.comments.commentModel.author.fullName.length + 2, $scope.comments.form.text.length - 1)
                    else
                        data.commentType = 'comment'
                        data.text = $scope.comments.form.text
                    commentsService.submit(data).then (res)->
                        $scope.comments.list.unshift res.data
                        closeForm()

                # cancel form
                # ---------------
                $scope.cancel = ->
                    closeForm()
            templateUrl: '/template/components/comments/comment-formTpl'
            link: (scope, element, attrs, ngModel)->

        }