var app = angular.module('liveCalendar', []);

app.controller('ShowController', function($scope, $http) {
    $http.get("/Live/GetShows").
    success(function(data) {
        for (var i = 0; i < data.length; i++) {
            $('#shows tbody').append('<tr><td>' + data[i].Artist + '</td><td>' + data[i].Venue + '</td><td>' + data[i].Date + '</td><td>' + data[i].Event + '</td>');
        }
        $scope.count = data.length;
    });
});