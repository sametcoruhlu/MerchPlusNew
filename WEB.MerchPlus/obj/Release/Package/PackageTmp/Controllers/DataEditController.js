var submitButton;
var gridData;
(function () {

    //#region View Load

    $(function () {
        //console.log("Hello, IIFE loaded for Data Edit Controller module. Let's get dirty...");
        PrepareDatePickers();
        PrepareGenerateButton();

        $('#btnBulkUpdate').click(function (e) {
            e.preventDefault();
            $('#winEdit').modal('show');
        });

        $('#winEdit_btnUpdate').click(function (e) {
            e.preventDefault();
            var idsToUpdate = [];
            var filterExpr = $("#gridContainer").dxDataGrid("instance").getCombinedFilter();
            gridData.filter(filterExpr);
            gridData.load().done(function (result) {
                for (i = 0; i < result.length; i++) {
                    console.log(result[i]);
                    idsToUpdate.push(result[i].Id);
                }
                //console.log(result.length);
            });

            //console.log(filterExpr);
            //console.log(idsToUpdate);

            UpdateShelfPriceByIds(idsToUpdate, $('#winEdit_txtShelfPrice').val());
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


        var token = JSON.parse(sessionStorage.getItem('token'));
        var member = JSON.parse(sessionStorage.getItem('entMember'));
        var currentCustomer = JSON.parse(sessionStorage.getItem('currentCustomer'));
        var eventInstance = event;
        var url = InfrastructureVariables.WebServicePath + "api/ReportingController/SelectMemberRouteDetailByCustomerIdStartDateEndDateGridDataForEdit";
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
                submitButton.ladda('start');
            },
            success: function (resultObject) {
                ////console.log(JSON.stringify(resultObject, null, 3));

                gridData = new DevExpress.data.DataSource({
                    store: resultObject.Content,
                    paginate: false
                });

                dataGrid = $("#gridContainer").dxDataGrid({
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
                        pageSize: 7
                    },
                    pager: {
                        showNavigationButtons: true
                    },
                    searchPanel: {
                        visible: true,
                        width: '250px',
                        placeholder: "Ara"
                    },
                    headerFilter: {
                        visible: true
                    },
                    selection: {
                        mode: "single"
                    },
                    "export": {
                        enabled: true,
                        fileName: "Veri Dökümü",
                        allowExportSelectedData: false
                    },
                    showRowLines: true,
                    hoverStateEnabled: true,
                    onContentReady: function (e) {
                        $('.txtFaceCount').off('change');
                        $('.txtFaceCount').change(function (e) {
                            //console.log($(this).attr('bsm-id') + ' - ' + $(this).val());
                            UpdateFaceCountById($(this).attr('bsm-id'), $(this).val());
                        });

                        $('.txtShelfStock').off('change');
                        $('.txtShelfStock').change(function (e) {
                            //console.log($(this).attr('bsm-id') + ' - ' + $(this).val());
                            UpdateShelfStockById($(this).attr('bsm-id'), $(this).val());
                        });

                        $('.txtShelfPrice').off('change');
                        $('.txtShelfPrice').change(function (e) {
                            //console.log($(this).attr('bsm-id') + ' - ' + $(this).val());
                            UpdateShelfPriceById($(this).attr('bsm-id'), $(this).val());
                        });

                    },
                    columns: [
                        {
                            dataField: "NameSurname",
                            caption: "Merch",
                            width: '150px',
                            fixed: true,
                        },
                        {
                            dataField: "EffectiveDate",
                            caption: "Tarih",
                            dataType: 'date',
                            format: 'dd.MM.yyyy',
                            width: '70px',
                            //cellTemplate: function (container, options) {
                            //    $('<span>' + numeral(options.data.EffectiveDate).format() + '</span>').appendTo(container);
                            //}
                        },
                        {
                            dataField: "RetailShopName",
                            caption: "Mağaza",
                        },
                        {
                            dataField: "CustomerProductName",
                            caption: "Ürün",
                        },
                        {
                            dataField: "FaceCount",
                            caption: "Önyüz",
                            width: '100px',
                            cellTemplate: function (container, options) {
                                $('<input type="number" style="text-align: center;" bsm-id="' + options.data.Id + '" class="txtFaceCount form-control" value="' + options.data.FaceCount + '"></input>').appendTo(container);
                            }
                        },
                        {
                            dataField: "ShelfPrice",
                            caption: "Raf Fiyatı",
                            width: '100px',
                            cellTemplate: function (container, options) {
                                $('<input type="number" style="text-align: center;" bsm-id="' + options.data.Id + '" class="txtShelfPrice form-control" value="' + options.data.ShelfPrice + '"></input>').appendTo(container);
                            }
                        },
                        {
                            dataField: "ShelfStock",
                            caption: "Raf Stok",
                            width: '100px',
                            cellTemplate: function (container, options) {
                                $('<input type="number" style="text-align: center;" bsm-id="' + options.data.Id + '" class="txtShelfStock form-control" value="' + options.data.ShelfStock + '"></input>').appendTo(container);
                            }
                        },
                    ],
                }).dxDataGrid('instance');

                submitButton.ladda('stop');

            },
            error: function (err) {
                submitButton.ladda('stop');
                //console.log('An error occured in api/ReportingController/SelectMemberRouteDetailByCustomerIdStartDateEndDateGridDataForEdit: ' + JSON.stringify(err, null, 3));
                swal('Hoppa...', 'İşleminiz sırasında bir hata oluştu.', 'error');
            }
        });

    }

    function UpdateFaceCountById(id, value) {

        var token = JSON.parse(sessionStorage.getItem('token'));
        var member = JSON.parse(sessionStorage.getItem('entMember'));
        var eventInstance = event;
        var url = InfrastructureVariables.WebServicePath + "api/MemberRouteDetailController/UpdateFaceCountById";
        var data = {};
        data.Id = id;
        data.FaceCount = value;
        $.ajax({
            type: 'POST',
            url: url,
            data: data,
            beforeSend: function (xhr) {
                xhr.setRequestHeader('Authorization', 'Bearer ' + token.access_token);
            },
            success: function (resultObject) {
                //console.log(JSON.stringify(resultObject, null, 3));
            },
            error: function (err) {
                //console.log('An error occured in api/MemberRouteDetailController/UpdateFaceCountById: ' + JSON.stringify(err, null, 3));
                swal('Oops...', 'İşleminiz yapılırken bir hata oluştu!', 'error');
            }
        });

    }

    function UpdateShelfStockById(id, value) {

        var token = JSON.parse(sessionStorage.getItem('token'));
        var member = JSON.parse(sessionStorage.getItem('entMember'));
        var eventInstance = event;
        var url = InfrastructureVariables.WebServicePath + "api/MemberRouteDetailController/UpdateShelfStockById";
        var data = {};
        data.Id = id;
        data.ShelfStock = value;
        $.ajax({
            type: 'POST',
            url: url,
            data: data,
            beforeSend: function (xhr) {
                xhr.setRequestHeader('Authorization', 'Bearer ' + token.access_token);
            },
            success: function (resultObject) {
                //console.log(JSON.stringify(resultObject, null, 3));
            },
            error: function (err) {
                //console.log('An error occured in api/MemberRouteDetailController/UpdateShelfStockById: ' + JSON.stringify(err, null, 3));
                swal('Oops...', 'İşleminiz yapılırken bir hata oluştu!', 'error');
            }
        });

    }

    function UpdateShelfPriceById(id, value) {

        var token = JSON.parse(sessionStorage.getItem('token'));
        var member = JSON.parse(sessionStorage.getItem('entMember'));
        var eventInstance = event;
        var url = InfrastructureVariables.WebServicePath + "api/MemberRouteDetailController/UpdateShelfPriceById";
        var data = {};
        data.Id = id;
        data.ShelfPrice = value;
        $.ajax({
            type: 'POST',
            url: url,
            data: data,
            beforeSend: function (xhr) {
                xhr.setRequestHeader('Authorization', 'Bearer ' + token.access_token);
            },
            success: function (resultObject) {
                //console.log(JSON.stringify(resultObject, null, 3));
            },
            error: function (err) {
                //console.log('An error occured in api/MemberRouteDetailController/UpdateShelfPriceById: ' + JSON.stringify(err, null, 3));
                swal('Oops...', 'İşleminiz yapılırken bir hata oluştu!', 'error');
            }
        });

    }

    function UpdateShelfPriceByIds(id, value) {

        var token = JSON.parse(sessionStorage.getItem('token'));
        var member = JSON.parse(sessionStorage.getItem('entMember'));
        var eventInstance = event;
        var url = InfrastructureVariables.WebServicePath + "api/MemberRouteDetailController/UpdateShelfPriceByIds";
        var data = {};
        data.Ids = id;
        data.ShelfPrice = value;
        $.ajax({
            type: 'POST',
            url: url,
            data: data,
            beforeSend: function (xhr) {
                xhr.setRequestHeader('Authorization', 'Bearer ' + token.access_token);
            },
            success: function (resultObject) {
                //console.log(JSON.stringify(resultObject, null, 3));
                $('#winEdit').modal('hide');
                swal('Tamam', 'Güncelleme başarıyla yapıldı', 'success');
            },
            error: function (err) {
                //console.log('An error occured in api/MemberRouteDetailController/UpdateShelfPriceById: ' + JSON.stringify(err, null, 3));
                $('#winEdit').modal('hide');
                swal('Oops...', 'İşleminiz yapılırken bir hata oluştu!', 'error');
            }
        });

    }

    function PrepareGenerateButton() {
        submitButton = $('#btnGenerate').ladda();
        submitButton.click(function (e) {
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