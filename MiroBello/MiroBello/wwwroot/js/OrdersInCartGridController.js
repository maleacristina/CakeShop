var ordersInCartGridController = {

    getBillsAPI: function () {
        var retApi = new ProductsAPI();
        var hostName = '';//window.location.protocol + "//" + window.location.host;    
        retApi.setBaseURL(hostName);
        return retApi;
    },
    loadData: function (filter) {
        var deferred = $.Deferred();
        var billsApi = this.getBillsAPI();
        billsApi.getAllBills().done(
            function (response) {
                deferred.resolve(response);
            });
        return deferred.promise();
    },
    insertItem: function (insertingItem) {
        var billsApi = this.getBillsAPI();
        return billsApi.addNewBill(insertingItem);
    },

    updateItem: function (updatingItem) {
        var billsApi = this.getBillsAPI();
        return billsApi.updateBill(updatingItem);
    },

    deleteItem: function (deletingItem) {
        var billsApi = this.getBillsAPI();
        return billsApi.deleteBill(deletingItem);
    }

}