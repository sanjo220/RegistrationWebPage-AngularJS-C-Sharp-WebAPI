(function(){
    
    var app = angular.module("app", []);
    
    var RegistrationController =  function($scope,$http) {
        
        $scope.isNotEqual = function(password,repeatPassword) {
            if (password === repeatPassword)
                return false;
            else   
                return true;
        };

        var onSuccessfulSave = function(response) {
            console.log(response);
            $scope.filePath = response.data;
            $scope.errorResult = "";
        };

        var onErrorResponse = function(response) {
            console.log(response);
            if (response.data === null && response.status === -1)
                $scope.errorResult = "Connection to WebAPI is refused."
            else
                $scope.errorResult = response.data.ExceptionMessage;
            $scope.filePath = "";
        };

        $scope.registerUser = function(firstname,lastname,email,username,password) {
            if (email === undefined)
                email = "";
            // ASP.NET Web API accepts this format, not JSON.
            var registrationDetail = "FirstName=" + firstname + 
                "&LastName=" + lastname + 
                "&Email=" + email + 
                "&UserName=" + username + 
                "&Password=" + password;
            
            $http({
                url: "http://localhost:53297/api/Registration/",
                method: "POST",
                data: registrationDetail,
                headers: {"Content-Type": "application/x-www-form-urlencoded"}
            })
            .then(onSuccessfulSave,onErrorResponse);
        };
        $scope.errorResult = "";
        $scope.filePath = "";
    };

    app.controller("RegistrationController", RegistrationController);

}());