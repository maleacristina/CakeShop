var productsInCartGridController =
    {

        getProductsOnCartAPI: function () {
            var retApi = new ProductsAPI();
            var hostName = '';//window.location.protocol + "//" + window.location.host;    
            retApi.setBaseURL(hostName);
            return retApi;
        },
        loadData: function (filter) {
            var deferred = $.Deferred();
            var productsOnCartApi = this.getProductsOnCartAPI();
            productsOnCartApi.getProductsFromCart(filter).done(
                function (response) {
                    deferred.resolve(response);
                });
            return deferred.promise();
        },
        insertItem: function (insertingItem) {
            var productsOnCartApi = this.getProductsOnCartAPI();
            return productsOnCartApi.addNewProduct(insertingItem);
        },

        updateItem: function (updatingItem) {
            var productsOnCartApi = this.getProductsOnCartAPI();
            return productsOnCartApi.updateProduct(updatingItem);
        },

        deleteItem: function (deletingItem) {
            var productsOnCartApi = this.getProductsOnCartAPI();
            return productsOnCartApi.deleteProduct(deletingItem);
        }


    }