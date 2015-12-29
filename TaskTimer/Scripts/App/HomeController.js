angular.module("taskTimer", [])
    .controller("HomeController", function($scope, $interval, $http) {

        $scope.currentEvent = {};
        $scope.ToggleEventButtonCaption = 'START';


        function msToTime(s) {
            var ms = s % 1000;
            s = (s - ms) / 1000;
            var secs = s % 60;
            s = (s - secs) / 60;
            var mins = s % 60;
            var hrs = (s - mins) / 60;

            return hrs + ':' + mins + ':' + secs;
        }

        var stop;

        var createNewEvent = function(date) {
            var newEvent = { Id: 0, ProjectId: 0, StartDate: date };
            return newEvent;
        }

        $scope.ToggleEvent = function() {


            $scope.stopTimer = function() {
                if (angular.isDefined(stop)) {
                    $interval.cancel(stop);
                    $scope.ToggleEventButtonCaption = 'START';
                    stop = undefined;
                }
            }

            if (angular.isDefined(stop)) {
                $scope.stopTimer();

                $scope.currentEvent.FinishDate = new Date();
                $scope.currentEvent.IsRunning = false;

                $http.post('/Home/Events', JSON.stringify($scope.currentEvent)).success(function() {

                    $http.get('/Home/EventHistory')
                        .success(function(historyList) {
                            $scope.historyList = historyList;
                            $scope.currentEvent = createNewEvent(new Date());
                        });
                });

                return;
            };


            if (!($scope.currentEvent.Id > 0)) {

                $scope.currentEvent = createNewEvent(new Date());
                $scope.currentEvent.IsRunning = true;

                $http.post('/Home/Events', JSON.stringify($scope.currentEvent)).success(function(id) {
                    $scope.currentEvent.Id = id;
                });
            }

            $scope.ToggleEventButtonCaption = 'STOP';

            stop = $interval(function() {

                if ($scope.currentEvent.Id > 0) {
                    if ($scope.currentEvent.StartDate.toString().indexOf('/') > -1) {
                        $scope.currentEvent.StartDate = new Date(parseInt($scope.currentEvent.StartDate.substr(6)));
                    }
                }

                $scope.currentEvent.ElapsedTime = msToTime(Date.now() - $scope.currentEvent.StartDate);
                $scope.currentEvent.IsRunning = true;
            }, 100);

            $scope.$on('$destroy', function() {
                $scope.stopTimer();
            });
        };

        $scope.messageFromAction = "";

        $scope.projects = [];
        $scope.historyList = [];

        var projectLoader = function() {
            $http.get('/Home/Projects')
                .success(function(projects) {
                    $scope.projects = projects;
                });
        }

        var historyLoader = function() {
            $http.get('/Home/EventHistory')
                .success(function(historyList) {
                    $scope.historyList = historyList;
                });
        }

        var loadLastNotFinishedEvent = function() {
            $http.get('/Home/CurrenEvent')
                .success(function(currentEvent) {

                    $scope.currentEvent = currentEvent;

                    if ($scope.currentEvent.Id > 0) {
                        $scope.currentEvent.IsRunning = true;
                        $scope.ToggleEvent();
                    } else {
                        $scope.currentEvent = createNewEvent(new Date());
                    }
                });
        };

        loadLastNotFinishedEvent();
        projectLoader();
        historyLoader();
    })
    .filter('jsonDate', [
        '$filter', function($filter) {
            return function(input, format) {
                return (input) ? $filter('date')(parseInt(input.substr(6)), format) : '';
            };
        }
    ]);