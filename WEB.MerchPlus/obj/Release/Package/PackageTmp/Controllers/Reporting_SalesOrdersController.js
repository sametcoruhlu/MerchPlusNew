(function () {

    //#region View Load

    $(function () {
        console.log("Hello, IIFE loaded for Sales Orders Controller module. Let's get dirty...");
        PrepareDatePickers();
        PrepareGenerateButton();
        $('#btnShipped').click(function (e) {
            e.preventDefault();
            MarkSalesOrderAsShipped();
        });
    });

    function GenerateReport() {

        if ($('#wleStartDate').val() == '' || $('#wleStartDate').datepicker('getDate') == 'Invalid Date') {
            swal('Hata', 'Başlangıç tarihi seçiniz!', 'error');
            return;
        }
        if ($('#wleEndDate').val() == '' || $('#wleEndDate').datepicker('getDate') == 'Invalid Date') {
            swal('Hata', 'Bitiş tarihi seçiniz!', 'error');
            return;
        }

        $('#btnGenerate').ladda('start');

        var token = JSON.parse(sessionStorage.getItem('token'));
        var member = JSON.parse(sessionStorage.getItem('entMember'));
        var currentCustomer = JSON.parse(sessionStorage.getItem('currentCustomer'));
        var eventInstance = event;
        var url = InfrastructureVariables.WebServicePath + "api/ReportingController/SelectSalesOrderByStartDateEndDateCustomerIdGridData";
        var data = {};
        data.CustomerId = currentCustomer.Id;
        data.StartDate = ConvertDateToSqlDate($('#wleStartDate').datepicker('getDate'));
        data.EndDate = ConvertDateToSqlDate($('#wleEndDate').datepicker('getDate'));

        $.ajax({
            type: 'POST',
            url: url,
            data: data,
            beforeSend: function (xhr) {
                xhr.setRequestHeader('Authorization', 'Bearer ' + token.access_token);
            },
            success: function (resultObject) {
                $('#btnGenerate').ladda('stop');
                console.log(JSON.stringify(resultObject, null, 3));
                dataGrid = $("#gridContainer").dxDataGrid({
                    height: '400px',
                    contentReadyAction: function () {
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
                        fileName: "Alınan Siparişler",
                        allowExportSelectedData: false
                    },
                    showRowLines: true,
                    hoverStateEnabled: true,
                    columns: [
                        {
                            caption: "Aksiyonlar",
                            cellTemplate: function (container, options) {
                                $('<button type="button" class="btnView btn btn-default btn-xs" bsm-id="' + options.data.Id + '"><i class="fa fa-eye"> </i></button>').appendTo(container);
                            }
                        },
                        {
                            dataField: "Id",
                            caption: "Sipariş No",
                        },
                        {
                            dataField: "RetailName",
                            caption: "Müşteri",
                        },
                        {
                            dataField: "RetailShopCategoryName",
                            caption: "Format",
                        },
                        {
                            dataField: "RetailShopName",
                            caption: "Mağaza",
                        },
                        {
                            dataField: "Quantity",
                            caption: "Adet",
                            cellTemplate: function (container, options) {
                                $('<span>' + options.data.Quantity + ' koli' + '</span>').appendTo(container);
                            }
                        },
                        {
                            dataField: "CreatedOn",
                            caption: "Sipariş Tarihi",
                            dataType: 'date',
                            format: 'dd.MM.yyyy',
                        },
                        {
                            dataField: "CreatedBy",
                            caption: "Oluşturan",
                        },
                        {
                            dataField: "SalesOrderStatusName",
                            caption: "Durum",
                            cellTemplate: function (container, options) {
                                $((options.data.SalesOrderStatusId == 1 ? '<span class="label label-warning">' + options.data.SalesOrderStatusName + '</span>' : '<span class="label label-success">' + options.data.SalesOrderStatusName + '</span>')).appendTo(container);
                            }
                        },
                        {
                            dataField: "ShippedOn",
                            caption: "Sevk Tarihi",
                            dataType: 'date',
                            format: 'dd.MM.yyyy',
                        },
                        {
                            dataField: "ShippedBy",
                            caption: "Sevkeden",
                        },
                    ],
                    summary: {
                        totalItems: [
                            {
                                column: "Quantity",
                                summaryType: "sum",
                                valueFormat: "number",
                                customizeText: function (data) {
                                    return "Σ: " + data.value;
                                }
                            },
                        ]
                    },
                    onContentReady: function (e) {
                        $('.btnView').off('click');
                        $('.btnView').click(function (e) {
                            e.preventDefault();
                            $('#hdnSalesOrderId').val($(this).attr('bsm-id'));
                            $.when(LoadSalesOrderDetail($('#hdnSalesOrderId').val())).done(function (result) {
                                $('#winEditSalesOrder').modal('show');
                            });
                        });
                    },
                }).dxDataGrid('instance');
            },
            error: function (err) {
                $('#btnGenerate').ladda('stop');
                console.log('An error occured in api/ReportingController/SelectSalesOrderByStartDateEndDateCustomerIdGridData: ' + JSON.stringify(err, null, 3));
                swal('Hoppa...', 'İşleminiz sırasında bir hata oluştu.', 'error');
            }
        });

    }

    function MarkSalesOrderAsShipped() {
        $('#winEditSalesOrder').modal('hide');
        var salesOrderId = $('#hdnSalesOrderId').val();
        var token = JSON.parse(sessionStorage.getItem('token'));
        var member = JSON.parse(sessionStorage.getItem('entMember'));
        var eventInstance = event;
        var url = InfrastructureVariables.WebServicePath + "api/SalesOrderController/MarkSalesOrderAsShipped";
        var data = {};
        data.Id = salesOrderId;
        data.MemberId = member.Id;
        $.ajax({
            type: 'POST',
            url: url,
            data: data,
            beforeSend: function (xhr) {
                xhr.setRequestHeader('Authorization', 'Bearer ' + token.access_token);
            },
            success: function (resultObject) {
                console.log(JSON.stringify(resultObject, null, 3));
                swal('Sevkedildi', 'Sipariş sevkedildi olarak işaretlendi ve listeniz güncellendi.', 'success');
                GenerateReport();
            },
            error: function (err) {
                console.log('An error occured in api/SalesOrderController/MarkSalesOrderAsShipped: ' + JSON.stringify(err, null, 3));
                swal('Hoppa...', 'İşleminiz sırasında bir hata oluştu.', 'error');
            }
        });

    }

    function LoadSalesOrderDetail(salesOrderId) {

        var token = JSON.parse(sessionStorage.getItem('token'));
        var member = JSON.parse(sessionStorage.getItem('entMember'));
        var eventInstance = event;
        var url = InfrastructureVariables.WebServicePath + "api/SalesOrderController/SelectSalesOrderGridDataById";
        var data = {};
        data.Id = salesOrderId;
        return $.ajax({
            type: 'POST',
            url: url,
            data: data,
            beforeSend: function (xhr) {
                xhr.setRequestHeader('Authorization', 'Bearer ' + token.access_token);
            },
            success: function (resultObject) {
                console.log(JSON.stringify(resultObject, null, 3));

                $('#lblSalesOrderId').html(resultObject.SalesOrder[0].Id);
                $('#lblRetailName').html(resultObject.SalesOrder[0].RetailName);
                $('#lblRetailShopCategoryName').html(resultObject.SalesOrder[0].RetailShopCategoryName);
                $('#lblRetailShopName').html(resultObject.SalesOrder[0].RetailShopName);
                $('#lblCreatedBy').html(resultObject.SalesOrder[0].CreatedBy);
                $('#lblTotalQuantity').html(resultObject.SalesOrder[0].Quantity);

                var salesOrderDetailOutput = '';
                $.each(resultObject.SalesOrderDetail, function (index, value) {
                    salesOrderDetailOutput += '<tr>' +
                                              '     <td>' + value.CustomerProductCode + '</td>' +
                                              '     <td>' + value.CustomerProductBarcode + '</td>' +
                                              '     <td>' + value.CustomerProductName + '</td>' +
                                              '     <td>' + value.Quantity + '</td>' +
                                              '</tr>';
                });

                $('#pnlSalesOrderDetail').html(salesOrderDetailOutput);
            },
            error: function (err) {
                console.log('An error occured in api/SalesOrderController/SelectSalesOrderGridDataById: ' + JSON.stringify(err, null, 3));
                swal('Hoppa...', 'İşleminiz sırasında bir hata oluştu.', 'error');
            }
        });

    }

    function PrepareGenerateButton() {
        $('#btnGenerate').ladda();
        $('#btnGenerate').click(function (e) {
            e.preventDefault();
            GenerateReport();
        });
    }

    function PrepareDatePickers() {
        $('#wleStartDate').datepicker({
            format: 'dd.mm.yyyy',
            autoclose: true,
            todayBtn: true,
            todayHighlight: true,
            language: 'tr',
        });

        $('#wleEndDate').datepicker({
            format: 'dd.mm.yyyy',
            endDate: '0d',
            autoclose: true,
            todayBtn: true,
            todayHighlight: true,
            language: 'tr',
        });

        $('#wleStartDate').datepicker('setDate', GetCurrentDate());
        $('#wleEndDate').datepicker('setDate', GetCurrentDate());
    }
}());