class CommentList extends Controller('socialApp.controllers')
    constructor: (
        $scope
        $window
        $timeout)->
            $scope.commentsLimit = 5

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
                $scope.comments.commentModel = commentModel
                $scope.comments.form.text = commentModel.author.fullName + ', '
                formWatcher = $scope.$watch 'comments.form.text', (val)->
                    if commentModel.author.fullName != val.substr 0, commentModel.author.fullName.length
                        $scope.comments.form.isAnswer = false
                        formWatcher()

            # scroll to answer
            # ---------------
            $scope.scrollToAnswer = (id, repeatCall)->
                $el = angular.element('#comment_' + id)
                if $el.length
                    angular.element('html, body').animate(
                        scrollTop: $el.offset().top - $window.innerHeight / 2
                    , 300)
                    $el.addClass 'cl__row--light'
                    $timeout(->
                        $el.removeClass 'cl__row--light'
                    , 1000)
                else if (repeatCall != true)
                    $scope.showMore()
                    $timeout ->
                        $scope.scrollToAnswer(id, true)