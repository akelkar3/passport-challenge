var app = angular.module('TreeApp', ['toastr', 'ui.bootstrap']);
hub = $.connection.treeHub; // create a proxy to signalr hub on web server
app.controller('TreeCtrl', function ($scope, $http, toastr, $uibModal) {
  

    $scope.getFactories = function () {
        $http.get("/api/factories/getFactories").then(function (data, status, headers, config) {
            $scope.factories = data.data;
         //   toastr.success('GotData', 'Success', { closeButton: true });
        }, function (data, status, headers, config) {
            toastr.error('Something went wrong while getting factories', 'Error', { closeButton: true });
        });
    }
    $scope.getFactories();
    $scope.edit = function (index) {
        $scope.selectedFactory = $scope.factories[index];
        $scope.isEdit = true;
        $scope.open(true);
    }

    $scope.deleteFactory = function (index) {

        var selectedFactory = $scope.factories[index];
        var req = {
            method: 'DELETE',
            url: "/api/factories/deleteFactory/" + selectedFactory.id
        }
        $http(req).then(function (data, status, hears, config) {
            hub.server.sendUpdate();
            toastr.success('Factory deleted successfuly', 'Success', { closeButton: true });
        }, function (data, status, hears, config) {
            toastr.error('Something went wrong while updating factory', data.message, { closeButton: true });

        });
    }
    $scope.generateNode = function (index) {
        var selected = $scope.factories[index];
        var req = {
            method: 'POST',
            url: "/api/factories/generateNodes",
            data: {
                id: selected.id,
                name: selected.name,
                minimum: selected.minimum,
                maximum: selected.maximum,
                numberOfNodes: selected.numberOfNodes
            }
        }
        $http(req).then(function (data, status, hears, config) {
            hub.server.sendUpdate();
            toastr.success('Nodes created successfuly', 'Success', { closeButton: true });
        }, function (data, status, hears, config) {
            toastr.error('Something went wrong while generating nodes ', data.message, { closeButton: true });

        });
    }
  
    $scope.open = function (edit) {
       $scope.isEdit= edit ? true : false;
        var modalInstance = $uibModal.open({
            templateUrl: "app/modalContent.html",
            plain: true,
            controller: "ModalContentCtrl",
            scope: $scope
        });

        modalInstance.result.then(function (response) {

        });


    };

    //Signal R related code
    hub.client.updareFactories = function (items) {

        $scope.getFactories();
        //$scope.changeFactoies(items);
        //$scope.$apply(); // this is outside of angularjs, so need to apply
    }

    $.connection.hub.start(); // connect to signalr hub

});

app.controller('ModalContentCtrl', function ($scope, $http, toastr, $uibModalInstance) {
    if ($scope.isEdit) {
        $scope.nameInput = $scope.selectedFactory.name;
        $scope.minimumInput = $scope.selectedFactory.minimum;
        $scope.maximumInput = $scope.selectedFactory.maximum;
        $scope.numberOfNodesInput = $scope.selectedFactory.numberOfNodes;
    }
    $scope.cancel = function () {
        $uibModalInstance.dismiss();
    }
    console.log($scope.factories);
    $scope.addFactory = function () {
        var req = {
            method: 'POST',
            url: "/api/factories/postFactory",
            data: {
                name: $scope.nameInput,
                minimum: $scope.minimumInput,
                maximum: $scope.maximumInput,
                numberOfNodes: $scope.numberOfNodesInput
            }
        }
        $http(req).then(function (data, status, hears, config) {
            $uibModalInstance.dismiss();
            hub.server.sendUpdate();
            toastr.success('Factory created successfuly', 'Success', { closeButton: true });
        }, function (data, status, hears, config) {
            $uibModalInstance.dismiss();
            toastr.error('Something went wriong while creating factory', data.message, { closeButton: true });

        });
    }
    $scope.editFactory = function () {
        var req = {
            method: 'PUT',
            url: "/api/factories/putFactory/" + $scope.selectedFactory.id,

            data: {
                id: $scope.selectedFactory.id,
                name: $scope.nameInput,
                minimum: $scope.minimumInput,
                maximum: $scope.maximumInput,
                numberOfNodes: $scope.numberOfNodesInput
            }
        }
        $http(req).then(function (data, status, hears, config) {
            hub.server.sendUpdate();
            $uibModalInstance.dismiss();
            toastr.success('Factory updated successfuly', 'Success', { closeButton: true });
        }, function (data, status, hears, config) {
            $uibModalInstance.dismiss();
            toastr.error('Something went wrong while updating factory', data.message, { closeButton: true });

        });
    }
});

