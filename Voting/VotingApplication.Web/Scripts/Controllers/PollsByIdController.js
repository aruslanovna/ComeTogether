/// <reference path="../Services/AccountService.js" />
/// <reference path="../Services/PollService.js" />
/// <reference path="../Services/RoutingService.js" />
/// <reference path="../Services/TokenService.js" />
(function () {
    'use strict';

    angular
        .module('GVA.Manage')
        .controller('PollsByIdController', PollsByIdController);

    PollsByIdController.$inject = ['$scope', 'AccountService', 'PollService', 'RoutingService', 'TokenService'];

    function PollsByIdController($scope, AccountService, PollService, RoutingService, TokenService) {

        var UNAUTHORISED = 401;

        $scope.getPollsById = getPollsById;
        $scope.manageUrl = manageUrl;
        $scope.copyPoll = copyPoll;

        $scope.userPolls = {};

        activate();


        function activate() {
            getPollsById();
        }

        function getPollsById() {
            if (AccountService.account) {
                PollService.getPollsById()
                    .then(function (response) {
                        $scope.userPolls = response.data;
                    })
                    .catch(function (response) {
                        if (response.status === UNAUTHORISED) {
                            AccountService.clearAccount();
                        }
                    });
            }
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
