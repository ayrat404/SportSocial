class JournalSubmit extends Controller('socialApp.controllers')
    constructor: (
        $scope
        journalSubmitService)->
        journalSubmitService.test()
        console.log('journal submit ctrl')

        # tags
        # -----------------
        $scope.j =
            tags: []
        $scope.getThemes = (search)->
            return null #[search + '123', search + '321']
        $scope.format = ($item, $model, $label)->
            debugger;
