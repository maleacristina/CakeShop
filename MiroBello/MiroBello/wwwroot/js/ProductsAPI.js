function ProductsAPI() {

    if (typeof ProductsAPI.instance === 'object') {
        return ProductsAPI.instance;
    }

    var baseURL = "localhost";

    var doAsyncGet = function (partialUrl) {
        var authorityToken = "";//$.cookie("labman_token");
        var fullUrl = baseURL + partialUrl;
        return $.ajax({
            url: fullUrl,
            
            type: "GET",
            headers: {
                "Authority": authorityToken
            },
            dataType: "json"
        });
    };

    var doAsyncPost = function (partialURL, jsonDataToPost) {
        var authorityToken = "";//$.cookie("labman_token");
        var fullUrl = baseURL + partialURL;
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

    var doAsyncDelete = function (partialURL) {
        var authorityToken = "";//$.cookie("labman_token");
        var fullUrl = baseURL + partialURL;
        return $.ajax({
            url: fullUrl,
            type: "DELETE",
            headers: {
                "Authority": authorityToken
            },
            dataType: "json"
        });
    };

    var doAsyncPut = function (partialURL, jsonDataToPut) {
        var authorityToken = "";//$.cookie("labman_token");
        var fullUrl = baseURL + partialURL;
        return $.ajax({
            url: fullUrl,
            type: "PUT",
            headers: {
                "Authority": authorityToken,
                "Content-Type": "application/json"
            },
            data: JSON.stringify(jsonDataToPut),
            dataType: "json"
        });
    };

    this.setBaseURL = function (strBaseURL) {
        baseURL = strBaseURL;
    };

    this.getAllProducts = function () {
        var allProductsReq = "/api/products";
        return doAsyncGet(allProductsReq);
    };
    this.getProduct = function (id) {
        var productReq = "/api/products/" + id;
        return doAsyncGet(productReq);
    };

    this.getAllCategory = function () {
        var apiURL = "/api/category";
        return doAsyncGet(apiURL);
    };

    this.getProductsByCategory = function (categoryId) {
        var apiURL = "/api/categories/" + categoryId + "/products";
        return doAsyncGet(apiURL);
    };

    this.getGradesForStudent = function (studentId) {
        var apiURL = "/api/students/" + studentId + "/grades";
        return doAsyncGet(apiURL);
    };

    this.addNewProduct = function (product) {
        var postURL = "/api/product";
        return doAsyncPost(postURL, product);
    };
    this.deleteProduct = function (productId) {
        var postURL = "/api/product/" + productId;
        return doAsyncDelete(postURL);
    };

    this.updateProduct = function (productData) {
        var putUrl = "/api/product/" + product.id;
        return doAsyncPut(putUrl, productData);
    };


    ProductsAPI.instance = this;
}