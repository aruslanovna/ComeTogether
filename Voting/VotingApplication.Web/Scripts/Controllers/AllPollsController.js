/// <reference path="../Services/AccountService.js" />
/// <reference path="../Services/PollService.js" />
/// <reference path="../Services/RoutingService.js" />
/// <reference path="../Services/TokenService.js" />
(function () {
    'use strict';

    angular
        .module('GVA.Manage')
        .controller('AllController', AllController);

    AllController.$inject = ['$scope', 'AccountService', 'PollService', 'RoutingService', 'TokenService'];

    function AllController($scope, AccountService, PollService, RoutingService, TokenService) {

        var UNAUTHORISED = 401;
        $scope.getAllPolls = getAllPolls;
       
        $scope.manageUrl = manageUrl;
        $scope.copyPoll = copyPoll;

        $scope.allPolls = {};

        activate();


        function activate() {
            getAllPolls();
        }

        function getAllPolls() {          
                        $scope.allPolls = response.data;

        }

        function goToManagePage(manageId) {
            return RoutingService.navigateToManagePage(manageId);
        }

        function manageUrl(manageId) {
            return RoutingService.getManagePageUrl(manageId);
        }

        function copyPoll(pollId) {
            PollService.copyPoll(pollId)
                .then(saveTokenAndGoToManage);
        }

        function saveTokenAndGoToManage(response) {
            var data = response.data;

            TokenService.setToken(data.NewPollId, data.CreatorBallotToken)
                .then(function () { goToManagePage(data.NewManageId); });

        }
    }

})();
