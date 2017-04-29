var app = angular.module('myApp', []);
app.controller('HomeCtrl', ['$scope', '$http', function ($scope, $http) {

    $http({
        method: 'POST',
        url: 'Home/Risks'
    })
        .then(function (response) {
            console.log(response);
            $scope.risklist = response.data;
        }, function (error) {
            console.log(error);
        });

}]);