angular.module('TreeApp', [ 'toastr'])
    .controller('TreeCtrl', function ($scope, $http, toastr) {
        $scope.factories = [];

      
            $http.get("/api/factories").then(function (data, status, headers, config) {
                $scope.factories = data.data;
                toastr.success('GotData', 'Success', { closeButton: true });
            }, function (data, status, headers, config) {
                toastr.error('Something went wriong while getting factories', 'Error', { closeButton: true });
            });
        
        $scope.editFactory = function (index) {

        }
       
    
    });