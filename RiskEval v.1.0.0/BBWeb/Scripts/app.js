

var module = angular.module('myapp', []);

//module.factory('memberDataStoreService', function ($http) {



//    var memberDataStore = {};
//    memberDataStore.doRegistration = function (theData) {
//        var promise = $http({ method: 'POST', url: 'memberservices/register', data: theData });
//        return promise;
//    }
//    return memberDataStore;
//}).controller("MyController", function ($scope, memberDataStoreService) {
//    $scope.person = {};
//    $scope.person.newsletterOptIn = true;
//    $scope.person.channels = [
//        { value: "television", label: "Television" },
//        { value: "radio", label: "Radio" },
//        { value: "social-media", label: "Social Media" },
//        { value: "other", label: "Other" }
//    ];
//    $scope.register = function () {
//        $scope.firstNameInvalid = false;
//        $scope.lastNameInvalid = false;
//        $scope.emailInvalid = false;
//        $scope.researchInvalid = false;
//        $scope.showSuccessMessage = false;
//        $scope.showErrorMessage = false;

//        if (!$scope.registrationForm.firstName.$valid) {
//            $scope.firstNameInvalid = true;
//        }
//        if (!$scope.registrationForm.lastName.$valid) {
//            $scope.lastNameInvalid = true;
//        }
//        if (!$scope.registrationForm.email.$valid) {
//            $scope.emailInvalid = true;
//        }
//        if (!$scope.registrationForm.research.$valid) {
//            $scope.researchInvalid = true;
//        }
//        // If the registration form is valid, use the
//        // memberDataStoreService to submit the form data
//        if ($scope.registrationForm.$valid) {

//            $scope.working = true;
//            var promise = memberDataStoreService.doRegistration($scope.person);
//            promise.success(function (data, status) {

//                $scope.successMessage = "Your transaction identifier is " + data.transactionID;
//                $scope.showSuccessMessage = true;
//            });
//            promise.error(function (data, status) {

//                if (status === 0) {
//                    $scope.errorMessage = "network or http level issue";
//                } else {
//                    $scope.errorMessage = "response HTTP status is " + status;
//                }

//                $scope.showErrorMessage = true;
//            });
//            promise.finally(function () {
//                $scope.working = false;
//            })

//            $scope.doShow = true;
//        }
//    }
//})

module.controller("userController", function ($scope, $http) {   

    $scope.field = "idcard";
    $scope.isAsc = true;
    $scope.page = 1;
    $scope.usersContainer = true;
    $scope.userContainer = false;
    $scope.user = {};

    $http.get('User2/GetUsers', {
        params: { field: $scope.field, isAsc: $scope.isAsc, page: $scope.page }
    })
        .success(function (data) {
            $scope.users = data.Models;
        });

    
    //Display user detail
    $scope.display = function (id) {

        $http.get('User2/GetUser', {
            params: { id: id }
        })
        .success(function (data) {
            
            $scope.user.firstNameTh = data.FirstNameTh;
            $scope.user.lastNameTh = data.LastNameTh;
            $scope.user.firstNameEn = data.FirstNameEn;
            $scope.user.lastNameEn = data.LastNameEn;
            $scope.user.address = data.Address;
            $scope.user.status = data.Status;
            $scope.user.phoneCenter = data.PhoneCenter;
            $scope.user.mobile = data.Mobile;
            $scope.user.phoneTable = data.PhoneDirect;
            $scope.user.email = data.Email;

            console.log($scope.loading);

            $scope.usersContainer = false;
            $scope.userContainer = true;
            //$("#list-users-container").hide();
            //$("#edit-uesr-container").show();

        });       
    }

    //Sort users to label
    $scope.order = function (field) {
        console.log($scope.isAsc);
        $http.get('User2/GetUsers', {
            params: { field: field, isAsc: $scope.isAsc, page: $scope.page }
        })
        .success(function (data) {
            $scope.users = data.Models;
            $scope.isAsc = !$scope.isAsc;
            console.log($scope.isAsc);
        });
    }

    $scope.enable = function (form) {
        console.log(form);
    }
})


var control = {

    get: function (url, param) {
        var result="";
        $.ajax({
            type: "GET",
            contentType: "application/json; charset=utf-8",
            url: url,
            data: param,
            async: false,
            dataType: "json",
            success: function (data) {
                if (data.Success) {
                    console.log(data);
                    //debugger
                    result = data;
                } else {
                    $("#message-fail").text(data.Message);
                    $('#modal-fail').modal('show');
                }
            },            
        });

        return result;
    },
};

