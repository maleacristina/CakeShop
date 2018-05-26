function ProductsAPI() {

    if (typeof ProductsAPI.instance === 'object') {
        return ProductsAPI.instance;
    }

    var baseURL = "localhost";
    var clientId = 1;
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
    this.setUserId = function (userId) {
        clientId = useId;
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
        var postURL = "/api/products";
        return doAsyncPost(postURL, product);
    };
    this.deleteProduct = function (productId) {
        var postURL = "/api/products/" + productId;
        return doAsyncDelete(postURL);
    };

    this.updateProduct = function (productData) {
        var putUrl = "/api/products/" + product.id;
        return doAsyncPut(putUrl, productData);
    };

    //This is part of cart
    this.getProductsFromCart = function () {
        var apiURL = "/api/ClientCarts/" + clientId;
        return doAsyncGet(apiURL);
    };

    this.addProductToShoppingCart = function (productId) {
        var postURL = "/api/ClientCarts/" + productId + "?clientId=" + clientId;
        return doAsyncPut(postURL, productId);
    };

    this.deleteProductOnCart = function (productData) {
    var postURL = "/api/ClientCarts/" + productData.productOnCartId;
    return doAsyncDelete(postURL);
    };

    this.updateProductOnCart = function (productData) {
        var putUrl = "/api/ClientCarts/" + productData.productOnCartId;
        return doAsyncPut(putUrl, productData);
    };

    //This is part of bill
    this.getAllBills = function () {
        var allBillsReq = "/api/orders";
        return doAsyncGet(allBillsReq);
    };
    this.addNewBill = function (bill) {
        var postURL = "/api/Orders";
        return doAsyncPost(postURL, bill);
    };
    this.deleteBill = function (billId) {
        var postURL = "/api/orders/" + billId;
        return doAsyncDelete(postURL);
    };

    this.updateBill = function (billData) {
        var putUrl = "/api/orders/" + billData.id;
        return doAsyncPut(putUrl, billData);
    };

    ProductsAPI.instance = this;
}