(function () {
    'use strict';

    angular
        .module('VoteOn-Dashboard')
        .controller('PollsByIdController', PollsByIdController);

    PollsByIdController.$inject = ['$scope', 'PollService', 'RoutingService'];

    function PollsByIdController($scope, PollService, RoutingService) {

        $scope.polls = [];
        $scope.loaded = false;

        $scope.navigateToPoll = navigateToPoll;
        $scope.navigateToResults = navigateToResults;
        $scope.navigateToHomePage = navigateToHomePage;

        activate();

        function activate() {
            PollService.getPollsById()
            .then(function (data) {
                $scope.loaded = true;
                $scope.polls = data;
            });
        }

        function navigateToPoll(UUID) {
            RoutingService.navigateToVotePage(UUID);
        }

        function navigateToResults(UUID) {
            RoutingService.navigateToResultsPage(UUID);
        }

        function navigateToHomePage() {
            RoutingService.navigateToHomePage();
        }
    }
})();
