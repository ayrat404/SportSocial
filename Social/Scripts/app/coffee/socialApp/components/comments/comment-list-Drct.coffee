class commentList extends Directive('socialApp.controllers')
    constructor: (commentsService)->
        return {
            restrict: 'E'
            replace: true
            scope:
                comments: '=ngModel'
                id: '@'
                entityType: '@'
            controller: ($scope)->

                $scope.commentsLimit = 1

                formWatcher = null

                # more comments
                # ---------------
                $scope.showMore = ->
                    $scope.commentsLimit = $scope.comments.list.length

                # answer activate
                # ---------------
                $scope.answer = (commentModel)->
                    $scope.comments.form.isAnswer = true
                    $scope.comments.form.focus = !$scope.comments.form.focus
                    $scope.comments.form.text = commentModel.author.fullName + ', '
                    formWatcher = $scope.$watch 'comments.form.text', (val)->
                        if commentModel.author.fullName != val.substr 0, commentModel.author.fullName.length
                            $scope.comments.form.isAnswer = false
                            formWatcher()


            #controller: 'commentFormController'
            templateUrl: '/template/components/comments/comment-listTpl'
            link: (scope, element, attrs, ngModel)->


        }