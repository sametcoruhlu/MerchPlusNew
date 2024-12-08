var submitButton;
var dataStore = [];
(function () {

    //#region View Load

    $(function () {
        console.log("Hello, IIFE loaded for Reporting Var Yok Controller module. Let's get dirty...");
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

        submitButton.ladda('start');

        var token = JSON.parse(sessionStorage.getItem('token'));
        var member = JSON.parse(sessionStorage.getItem('entMember'));
        var currentCustomer = JSON.parse(sessionStorage.getItem('currentCustomer'));
        var eventInstance = event;
        var url = InfrastructureVariables.WebServicePath + "api/ReportingController/OnyuzRaporuV2";
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
                submitButton.ladda('stop');
                //console.log(JSON.stringify(resultObject));

                $("#gridContainer").dxPivotGrid({
                    allowSortingBySummary: true,
                    allowSorting: true,
                    allowFiltering: true,
                    allowExpandAll: true,
                    height: 440,
                    showBorders: true,
                    fieldChooser: {
                        enabled: true
                    },
                    "export": {
                        enabled: true,
                        fileName: "Onyuz Raporu"
                    },
                    dataSource: {
                        fields: [
                            {
                                caption: "NameSurname",
                                dataField: "NameSurname",
                                area: "row",
                                expanded: false,
                            },
                            {
                                caption: "CustomerProduct",
                                dataField: "CustomerProductName",
                                area: "row",
                                expanded: false,
                            },
                            {
                                caption: "Channel",
                                dataField: "RetailCategoryName",
                            },
                            {
                                caption: "City",
                                dataField: "CityName",
                            },
                            {
                                caption: "Retail",
                                dataField: "RetailName",
                            },
                            {
                                caption: "RetailShop",
                                dataField: "RetailShopName",
                            },
                            {
                                caption: "CustomerBrand",
                                dataField: "CustomerBrandName",
                                area: "column",
                            },
                            {
                                caption: "Onyuz (Avg)",
                                dataField: "FaceCount",
                                summaryType: "avg",
                                area: "data",
                                format: { type: 'fixedPoint', precision: 2 }
                            },
                            {
                                caption: "Onyuz Payı",
                                dataField: "FaceCount",
                                summaryType: "sum",
                                area: "data",
                                summaryDisplayMode: "percentOfRowTotal",
                            }],
                        store: resultObject.Content
                    }
                });

                //setTimeout(function () {
                //    dataGrid.repaint();
                //}, 2000);

            },
            error: function (err) {
                submitButton.ladda('stop');
                console.log('An error occured in api/ReportingController/OnyuzRaporuV2: ' + JSON.stringify(err, null, 3));
                swal('Hoppa...', 'İşleminiz sırasında bir hata oluştu.', 'error');
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