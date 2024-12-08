(function () {

    //#region View Load

    $(function () {

        //#region Console writeout

        console.log("Hello, IIFE loaded for TopMenu module. Let's get dirty...");

        LoadCustomerName();

        //#endregion

    });

    //#endregion

    //#region Events

    function LoadCustomerName() {
        var customerEntity = JSON.parse(sessionStorage.getItem('currentCustomer'));
        $('#TopMenu_lblCompanyName').html(customerEntity.Name);
    }

    //#endregion

}());