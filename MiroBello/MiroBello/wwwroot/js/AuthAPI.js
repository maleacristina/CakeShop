function AuthAPI() {

    if (typeof AuthAPI.instance === 'object') {
        return AuthAPI.instance;
    }

    var baseUrl = "";

   

    var doAsyncPost = function (partialUrl, jsonDataToPost) {
        var authorityToken = "";//$.cookie("labman_token");
        var fullUrl = baseUrl + partialUrl;
        return $.ajax({
            url: fullUrl,
            type: "POST",
            headers: {
                "Authority": authorityToken,
                "Content-Type": "application/json"

            },
            data: JSON.stringify(jsonDataToPost),
            dataType: "json"
        });
    };

    
    this.setBaseURL = function (baseUrl) {
        baseUrl = baseUrl;
    };

    this.DoLogin = function (loginInfo, successFn, failFn) {
        var loginUrl = "/api/auth/login";
        doAsyncPost(loginUrl, loginInfo).done(function (response) {

            var token = response;
           // $.cookie("labman_token")= token;
            successFn();

        }).fail(function(response) {
            failFn(response);
        });
    };
    

    AuthAPI.instance = this;
}