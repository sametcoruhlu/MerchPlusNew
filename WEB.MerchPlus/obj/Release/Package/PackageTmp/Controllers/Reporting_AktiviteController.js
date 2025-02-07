(function () {

    //#region View Load

    $(function () {
        console.log("Hello, IIFE loaded for Reporting Aktivite Controller module. Let's get dirty...");
        PrepareDatePickers();
        PrepareGenerateButton();
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
        var url = InfrastructureVariables.WebServicePath + "api/ReportingController/AktiviteRaporu";
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
                        fileName: "Aktivite Raporu",
                        allowExportSelectedData: false
                    },
                    showRowLines: true,
                    hoverStateEnabled: true,
                    columns: [
                        {
                            dataField: "CustomerProductName",
                            caption: "Ürün",
                            fixed: true,
                        },
                        {
                            dataField: "RetailCategoryName",
                            caption: "Kanal",
                        },
                        {
                            dataField: "CityName",
                            caption: "Şehir",
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
                            dataField: "ActivityTypeName",
                            caption: "Aktivie Türü",
                        },
                        {
                            dataField: "DiscountedPrice",
                            caption: "Aktivite Fiyatı",
                            dataType: 'number',
                        },
                        {
                            dataField: "EffectiveDate",
                            caption: "Kayıt Tarihi",
                            dataType: 'date',
                            format: 'dd.MM.yyyy',
                        },
                        {
                            dataField: "NameSurname",
                            caption: "Merch",
                        }
                    ],
                    onContentReady: function (e) {
                        $("#gridContainer").css('height', '330px');
                        setTimeout(function () {
                            repaintCounter += 1;
                            if (repaintCounter <= 5)
                                dataGrid.repaint();
                        }, 100);
                    }
                }).dxDataGrid('instance');
            },
            error: function (err) {
                console.log('An error occured in api/ReportingController/AktiviteRaporu: ' + JSON.stringify(err, null, 3));
                swal('Hoppa...', 'İşleminiz sırasında bir hata oluştu.', 'error');
            }
        });

    }

    function PrepareGenerateButton() {
        $('#btnGenerate').off('click');
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