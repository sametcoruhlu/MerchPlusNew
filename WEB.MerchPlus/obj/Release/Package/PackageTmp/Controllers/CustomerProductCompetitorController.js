(function () {

    $(function () {
        console.log("Hello, IIFE loaded for CustomerProductCompetitor module. Let's get dirty...");

        LoadGrid();
        LoadProducts();
        LoadCompetitorProducts();

        $('#btnAddNew').click(function (e) {
            $('#winEdit').modal('show');
        });

        $('#btnSave').click(function (e) {
            $('#winEdit').modal('hide');
            var token = JSON.parse(sessionStorage.getItem('token'));
            var member = JSON.parse(sessionStorage.getItem('entMember'));
            var customer = JSON.parse(sessionStorage.getItem('currentCustomer'));
            var eventInstance = event;
            var url = InfrastructureVariables.WebServicePath + "api/CustomerProductCompetitorController/InsertCustomerProductCompetitor";
            var data = {};
            data.CustomerId = customer.Id;
            data.CustomerProductId = $('#winEdit_wleCustomerProductId').val();
            data.CustomerProductCompetitorId = $('#winEdit_wleCustomerProductCompetitorId').val();
            $.ajax({
                type: 'POST',
                url: url,
                data: data,
                beforeSend: function (xhr) {
                    xhr.setRequestHeader('Authorization', 'Bearer ' + token.access_token);
                },
                success: function (resultObject) {
                    console.log(JSON.stringify(resultObject, null, 3));
                    swal('Kaydedildi', 'Rakip ürün başarıyla kaydedildi', 'success');
                    LoadGrid();
                },
                error: function (err) {
                    console.log('An error occured in api/CustomerProductCompetitorController/InsertCustomerProductCompetitor: ' + JSON.stringify(err, null, 3));
                    swal('Hoppa...', 'İşleminiz sırasında bir hata oluştu.', 'error');
                }
            });

        });
    });

    function LoadProducts() {

        var token = JSON.parse(sessionStorage.getItem('token'));
        var member = JSON.parse(sessionStorage.getItem('entMember'));
        var customer = JSON.parse(sessionStorage.getItem('currentCustomer'));
        var eventInstance = event;
        var url = InfrastructureVariables.WebServicePath + "api/CustomerProductController/SelectCustomerProductByCustomerId";
        var data = {};
        data.CustomerId = customer.Id;
        $.ajax({
            type: 'POST',
            url: url,
            data: data,
            beforeSend: function (xhr) {
                xhr.setRequestHeader('Authorization', 'Bearer ' + token.access_token);
            },
            success: function (resultObject) {
                console.log(JSON.stringify(resultObject, null, 3));
                var output = '<option></option>';
                $.each(resultObject.Content, function (index, value) {
                    output += '<option value="' + value.Id + '">' + value.Name + '</option>';
                });

                $('#winEdit_wleCustomerProductId').html(output);
                $('#winEdit_wleCustomerProductId').select2({
                    width: '100%',
                    theme: 'bootstrap',
                    placeholder: 'Seçiniz...',
                    allowClear: true,
                });

            },
            error: function (err) {
                console.log('An error occured in api/CustomerProductController/SelectCustomerProductByCustomerId: ' + JSON.stringify(err, null, 3));
                swal('Hoppa...', 'İşleminiz sırasında bir hata oluştu.', 'error');
            }
        });

    }
    function LoadCompetitorProducts() {

        var token = JSON.parse(sessionStorage.getItem('token'));
        var member = JSON.parse(sessionStorage.getItem('entMember'));
        var customer = JSON.parse(sessionStorage.getItem('currentCustomer'));
        var eventInstance = event;
        var url = InfrastructureVariables.WebServicePath + "api/CustomerProductCompetitorController/SelectCustomerProductCompetitorByCustomerId";
        var data = {};
        data.CustomerId = customer.Id;
        $.ajax({
            type: 'POST',
            url: url,
            data: data,
            beforeSend: function (xhr) {
                xhr.setRequestHeader('Authorization', 'Bearer ' + token.access_token);
            },
            success: function (resultObject) {
                console.log(JSON.stringify(resultObject, null, 3));
                var output = '<option></option>';
                $.each(resultObject.Content, function (index, value) {
                    output += '<option value="' + value.Id + '">' + value.CustomerProductName + '</option>';
                });

                $('#winEdit_wleCustomerProductCompetitorId').html(output);
                $('#winEdit_wleCustomerProductCompetitorId').select2({
                    width: '100%',
                    theme: 'bootstrap',
                    placeholder: 'Seçiniz...',
                    allowClear: true,
                });

            },
            error: function (err) {
                console.log('An error occured in api/CustomerProductController/SelectCustomerProductByCustomerId: ' + JSON.stringify(err, null, 3));
                swal('Hoppa...', 'İşleminiz sırasında bir hata oluştu.', 'error');
            }
        });

    }

    function LoadGrid() {

        var token = JSON.parse(sessionStorage.getItem('token'));
        var member = JSON.parse(sessionStorage.getItem('entMember'));
        var customer = JSON.parse(sessionStorage.getItem('currentCustomer'));
        var eventInstance = event;
        var url = InfrastructureVariables.WebServicePath + "api/CustomerProductCompetitorController/SelectCustomerProductCompetitorByCustomerIdGridData";
        var data = {};
        data.CustomerId = customer.Id;
        $.ajax({
            type: 'POST',
            url: url,
            data: data,
            beforeSend: function (xhr) {
                xhr.setRequestHeader('Authorization', 'Bearer ' + token.access_token);
            },
            success: function (resultObject) {
                console.log(JSON.stringify(resultObject, null, 3));

                var dataGrid = $("#gridContainer").dxDataGrid({
                    height: '300px',
                    contentReadyAction: function () {
                        $("#gridContainer .dx-scrollable").dxScrollable({ showScrollbar: 'always' });
                    },
                    onContentReady: function (e) {
                        $('.btnDelete').off('click');
                        $('.btnDelete').click(function (e) {
                            console.log($(this).attr('bsm-id'));
                            DeleteCustomerProductCompetitorById($(this).attr('bsm-id'));
                        });
                        $("#gridContainer .dx-scrollable").dxScrollable({ showScrollbar: 'always' });
                    },
                    allowColumnReordering: true,
                    allowColumnResizing: true,
                    columnAutoWidth: true,
                    columnChooser: {
                        enabled: true
                    },
                    columnFixing: {
                        enabled: true
                    },
                    dataSource: resultObject.Content,
                    filterRow: {
                        visible: true,
                        applyFilter: "auto"
                    },
                    paging: {
                        pageSize: 100
                    },
                    scrolling: {
                        mode: "virtual"
                    },
                    searchPanel: {
                        visible: true,
                        width: '250px',
                        placeholder: "Search"
                    },
                    headerFilter: {
                        visible: true
                    },
                    selection: {
                        mode: "single"
                    },
                    "export": {
                        enabled: true,
                        fileName: "Ürün Eşleştirme Listesi",
                        allowExportSelectedData: false
                    },
                    showRowLines: true,
                    hoverStateEnabled: true,
                    columns: [
                        {
                            dataField: "CustomerProductName",
                            caption: "Ürün",
                        },
                        {
                            dataField: "CustomerProductCompetitorName",
                            caption: "Rakip Ürün",
                        },
                        {
                            caption: "Aksiyonlar",
                            cellTemplate: function (container, options) {
                                $('<button type="button" class="btnDelete btn btn-danger btn-xs" bsm-id="' + options.data.Id + '">Sil</button>').appendTo(container);
                            }
                        },

                    ],
                }).dxDataGrid('instance');

                setTimeout(function () {
                    dataGrid.repaint();
                }, 2000);

            },
            error: function (err) {
                console.log('An error occured in api/CustomerProductCompetitorController/SelectCustomerProductCompetitorByCustomerIdGridData: ' + JSON.stringify(err, null, 3));
                swal('Hoppa...', 'İşleminiz sırasında bir hata oluştu.', 'error');
            }
        });

    }
    function DeleteCustomerProductCompetitorById(idToDelete) {

        var token = JSON.parse(sessionStorage.getItem('token'));
        var member = JSON.parse(sessionStorage.getItem('entMember'));
        var eventInstance = event;
        var url = InfrastructureVariables.WebServicePath + "api/CustomerProductCompetitorController/DeleteCustomerProductCompetitorById";
        var data = {};
        data.Id = idToDelete;
        $.ajax({
            type: 'POST',
            url: url,
            data: data,
            beforeSend: function (xhr) {
                xhr.setRequestHeader('Authorization', 'Bearer ' + token.access_token);
            },
            success: function (resultObject) {
                console.log(JSON.stringify(resultObject, null, 3));
                swal('İşlem Tamam', 'Rakip ürün eşleştirmesi başarıyla silindi', 'success');
                LoadGrid();
            },
            error: function (err) {
                console.log('An error occured in api/CustomerProductCompetitorController/DeleteCustomerProductCompetitorById: ' + JSON.stringify(err, null, 3));
                swal('Hoppa...', 'İşleminiz sırasında bir hata oluştu.', 'error');
            }
        });

    }

}());
